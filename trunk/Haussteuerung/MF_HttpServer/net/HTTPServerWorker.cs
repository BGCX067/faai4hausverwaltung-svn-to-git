/**********************************************************************************
 * Multi Threaded Webserver implementation
 *  
 * A fully threaded webserver implementation. 
 * 
 * Wait's for connections in it's own thread spawning new threads for recieved connections
 * Automaticly frees the finished worker threads.
 * 
 * (C)opyright Elze Kool, 2008, http://www.microframework.nl
 * UrlDecode function (C)opyright Jan Kucera http://informatix.miloush.net/microframework/
 * 
 * This code is provided AS-IS. I take no responsibility for any damage that occours when using this class
 * or any part of it. You may use this code freely non-commericaly and commercialy as long as you keep
 * in above copyright notice.
 * 
 **********************************************************************************/

// Base
using System;
using Microsoft.SPOT;

// Collections, used for ArrayList
using System.Collections;

// Text used for decoding and encoding UTF8
using System.Text;

// Threading, Used for Threads
using System.Threading;

// Networking
//using System.Net;
using GHIElectronics.NETMF.Net;
//using System.Net.Sockets;
using GHIElectronics.NETMF.Net.Sockets;
//using Microsoft.SPOT.Net.NetworkInformation;
using GHIElectronics.NETMF.Net.NetworkInformation;
//using Socket = System.Net.Sockets.Socket;
using Socket = GHIElectronics.NETMF.Net.Sockets.Socket;

namespace ElzeKool.net
{
    /// <summary>
    /// HTTP Server Worker
    /// This class does the heavy work it's created by HTTPServer 
    /// to process the request. Closes the connection.
    /// 
    /// First create worker with socket and handler then call DoWork to start working
    /// </summary>
    class HTTPServerWorker
    {
        /// <summary>
        /// Accepted Socket is stored here
        /// </summary>
        private Socket _AcceptSocket = null;

        /// <summary>
        /// Function that handles request
        /// </summary>
        private IWebRequestHandler _RequestHandler = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="AcceptSocket">Socket returned by ListenSocket.Accept</param>
        public HTTPServerWorker(ref Socket AcceptSocket, IWebRequestHandler RequestHandler)
        {
            _AcceptSocket = AcceptSocket;
            _RequestHandler = RequestHandler;
        }

        /// <summary>
        /// Close Connection
        /// </summary>
        private void Close()
        {
            try
            {
                _AcceptSocket.Close();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Abort connection with 500 error
        /// </summary>
        private void Abort()
        {
            // Send 500 Bad request
            String Response = "";
            Response += "HTTP/1.0 500 Bad request\r\n";
            Response += "Content-Type: text/plain\r\n";
            Response += "Connection: close\r\n";
            Response += "\r\n";
            Response += "This server doesn't support this request.";

            // Send Response
            if (_AcceptSocket.Poll(500,SelectMode.SelectWrite))
            {
                try
                {
                    _AcceptSocket.Send(Encoding.UTF8.GetBytes(Response));
                }
                catch { 
                    // Debug.Print("Failed error sending response.. Client closed?"); 
                }
            }

            // Close Socket
            Close();
        }

        /// <summary>
        /// Processwork processes a recieved request sending back the data
        /// to the socket. 
        /// </summary>
        /// <param name="Request">Request to handle</param>
        public bool ProcessRequest(String Request)
        {

            // All IRequestHandler Parameters
            String RequestMethod = "";
            String RequestURI = "";
            String PostBody = "";
            HTTPHeader[] RequestHeaders = null;
            RequestParameter[] RequestGET = null;
            RequestParameter[] RequestPOST = null;
            String ResponseStatus = "HTTP/1.0 404 Not Found";
            bool CloseConnection = false;
            HTTPHeader[] ResponseHeaders = new HTTPHeader[] 
            {
                new HTTPHeader("Connection","keep-alive"),
                new HTTPHeader("Server","MF_HttpServer by Elze Kool")
            };

            byte[] ResponseBody = Encoding.UTF8.GetBytes("Requested object not available or not handled");

            try
            {
                // If there is no newline in request abort right away..
                if (Request.IndexOf("\r\n") == -1)
                {
                    Abort();
                    return true;
                }

                // The first line should Explode into 3 parts:
                // GET / HTTP/1.1
                String[] RequestStart = Request.Substring(0, Request.IndexOf("\r\n")).Split(' ');
                if (RequestStart.Length != 3)
                {
                    Abort();
                    return true;
                }

                // We only parse valid 1.0 and 1.1 HTTP requests
                if ((RequestStart[2] != "HTTP/1.0") & (RequestStart[2] != "HTTP/1.1"))
                {
                    Abort();
                    return true;
                }

                // We only parse GET and POST request
                if ((RequestStart[0] != "GET") & (RequestStart[0] != "POST"))
                {
                    Abort();
                    return true;
                }

                // Ok, we now got a valid HTTP GET or POST request, store URI and Method
                RequestMethod = RequestStart[0];
                RequestURI = RequestStart[1];
                RequestHeaders = HTTPHeader.ParseRequestHeader(Request);

                // Check if we have GET parameters 
                // Be a little gentile here, it should start with ?... but
                // sometimes it starts with &.. 
                if (RequestURI.IndexOf('?') != -1)
                {
                    // Build RequestGET array
                    RequestGET = RequestParameter.CreateFromRequest(RequestURI.Substring(RequestURI.IndexOf('?') + 1));

                    // Remove Header from URI
                    RequestURI = RequestURI.Substring(0, RequestURI.IndexOf('?'));
                }
                else
                {
                    // The gentile part
                    if (RequestURI.IndexOf('&') != -1)
                    {
                        // Build RequestGET array
                        RequestGET = RequestParameter.CreateFromRequest(RequestURI.Substring(RequestURI.IndexOf('&') + 1));

                        // Remove Header from URI
                        RequestURI = RequestURI.Substring(0, RequestURI.IndexOf('&'));
                    }
                }

                // Store POST request body
                if (Request.IndexOf("\r\n\r\n") != -1)
                {
                    if (Request.IndexOf("\r\n\r\n") + 4 < Request.Length)
                    {
                        PostBody = Request.Substring(Request.IndexOf("\r\n\r\n") + 4);
                    }
                }

                // Check if we have POST parameters
                // - Method must be POST
                // - Form must be url encoded (Content-Type: application/x-www-form-urlencoded)
                // - There must be a request body
                if (
                    (RequestMethod == "POST") &
                    (HTTPHeader.Get(ref RequestHeaders, "Content-Type","").IndexOf("application/x-www-form-urlencoded") != -1) &
                    (Request.IndexOf("\r\n\r\n") != -1))
                {
                    if (Request.IndexOf("\r\n\r\n") + 4 < Request.Length)
                    {
                        // Build RequestPOST array
                        RequestPOST = RequestParameter.CreateFromRequest(Request.Substring(Request.IndexOf("\r\n\r\n") + 4));
                    }
                }

                // Check if the client wishes to close the connection
                if (HTTPHeader.Get(ref RequestHeaders, "Connection", "").ToLower() == "close")
                {
                    HTTPHeader.Set(ref ResponseHeaders, "Connection", "close");
                    CloseConnection = true;
                }

                // Now let the requesthandler handle the request
                if (_RequestHandler != null)
                {
                    _RequestHandler.HandleRequest(
                        RequestMethod,
                        RequestURI,
                        PostBody,
                        RequestHeaders,
                        RequestGET,
                        RequestPOST,
                        ref ResponseStatus,
                        ref ResponseHeaders,
                        ref ResponseBody);
                }

                // Send Response
                String sResponseHeader = "";

                // Add Status
                sResponseHeader += ResponseStatus + "\r\n";

                // Add Content-Length to response header. 
                if (HTTPHeader.Get(ref ResponseHeaders, "Content-Length") == null)
                {
                    HTTPHeader.Set(ref ResponseHeaders, "Content-Length", ResponseBody.Length.ToString());
                }

                // Add Headers
                sResponseHeader += HTTPHeader.OutputResponseHeader(ResponseHeaders);

                // End of Headers, Start of body
                sResponseHeader += "\r\n";

                // Create byte[] array from response header
                byte[] ResponseHeader = Encoding.UTF8.GetBytes(sResponseHeader);

                // Create response
                byte[] Response = new byte[ResponseHeader.Length + ResponseBody.Length];

                // Copy header to response
                Array.Copy(ResponseHeader, Response, ResponseHeader.Length);

                // Copy body to response
                Array.Copy(ResponseBody, 0, Response, ResponseHeader.Length, ResponseBody.Length);

                // Send Response
                if (_AcceptSocket.Poll(100, SelectMode.SelectWrite))
                {
                    try
                    {
                        _AcceptSocket.Send(Response);
                    }
                    catch
                    {
                        // Prevent that the server shuts down due to errors
                        CloseConnection = true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Output serious errors but don't let it shutdown the server
                Debug.Print("HTTPServerWorkerProcessor - Serious error");
                Debug.Print("Message: " + ex.Message);
                Debug.Print("Stacktrace: \r\n" + ex.StackTrace);
                CloseConnection = true;
            }

            return CloseConnection;

        }


        /// <summary>
        /// This function is called to parse request
        /// </summary>
        public void DoWork()
        {
            try
            {
                while (true)
                {
                    // Request is stored here
                    String Request = "";

                    // Recieve buffer
                    byte[] buffer = new byte[256];

                    // Wait max 5 seconds for request to come in
                    DateTime timeoutAt = DateTime.Now.AddSeconds(5);
                    Int32 _Available = _AcceptSocket.Available;
                    while (_Available == 0 && DateTime.Now < timeoutAt)
                    {
                        try
                        {
                            _Available = _AcceptSocket.Available;
                        }
                        catch
                        {
                            break;
                        }

                        System.Threading.Thread.Sleep(100);
                    }

                    try
                    {
                        while (_AcceptSocket.Poll(500, SelectMode.SelectRead))
                        {
                            // If 0 bytes in buffer, then connection is closed, or we have timed out
                            if (_AcceptSocket.Available == 0)
                            {
                                break;
                            }

                            // Zero all bytes in the re-usable buffer
                            Array.Clear(buffer, 0, buffer.Length);

                            // Read a buffer-sized text chunk
                            Int32 bytesRead = _AcceptSocket.Receive(buffer, (_AcceptSocket.Available > buffer.Length) ? buffer.Length : _AcceptSocket.Available, SocketFlags.None);

                            // Append the chunk to the string
                            Request += new String(Encoding.UTF8.GetChars(buffer));
                        }
                    }
                    catch
                    {
                        break;
                    }
                    // Exit if no valid request is recieved
                    if (Request == "") break;

                    // If there is no newline in request abort right away..
                    if (Request.IndexOf("\r\n") == -1)
                    {
                        Abort();
                        return;
                    }

                    // The first line should Explode into 3 parts:
                    // GET / HTTP/1.1
                    String[] RequestStart = Request.Substring(0, Request.IndexOf("\r\n")).Split(' ');
                    if (RequestStart.Length != 3)
                    {
                        Abort();
                        return;
                    }

                    // We only parse valid 1.0 and 1.1 HTTP requests
                    if ((RequestStart[2] != "HTTP/1.0") & (RequestStart[2] != "HTTP/1.1"))
                    {
                        Abort();
                        return;
                    }

                    // We only parse GET and POST request
                    if ((RequestStart[0] != "GET") & (RequestStart[0] != "POST"))
                    {
                        Abort();
                        return;
                    }

                    // Process request and close connection if requested
                    if (ProcessRequest(Request) == true)
                        break;
                }
            }
            catch (Exception ex)
            {
               // Output serious errors but don't let it shutdown the server
               Debug.Print("HTTPServerWorker - Serious error");
               Debug.Print("Message: " + ex.Message);
               Debug.Print("Stacktrace: \r\n" + ex.StackTrace);
            }

            Close();
        }

    }
}
