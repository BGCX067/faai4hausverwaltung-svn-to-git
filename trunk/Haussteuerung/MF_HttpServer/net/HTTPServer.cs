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
    /// Multi Threaded Webserver implementation
    /// Wait's for connections in it's own thread spawning new threads for recieved connections
    /// Automaticly frees the finished worker threads.
    /// 
    /// Normaly the thread limit's the number of request workers to 5 to change this
    /// update the MaxWorkers constant
    /// 
    /// To debug the creation and stopping of threads uncomment a line in the
    /// DebugWorkers function
    /// </summary>
    public class HTTPServer : IDisposable
    {
        
        #region Private Properties

        /// <summary>
        /// Define request handler
        /// </summary>
        private IWebRequestHandler RequestHandler = null;

        /// <summary>
        /// Socket Listening for connections
        /// </summary>
        private Socket ListenSocket = null;

        /// <summary>
        /// Holds the worker threads
        /// </summary>
        private ArrayList WorkerThreads = new ArrayList();

        /// <summary>
        /// This thread waits for connections and accept them
        /// </summary>
        private Thread AcceptConnection = null;

        /// <summary>
        /// Used for Worker Thread manager
        /// </summary>
        private Thread ManageWorkers = null;

        /// <summary>
        /// Limit the number of worker Threads
        /// </summary>
        private const byte MaxWorkers = 15;

        #endregion

        /// <summary>
        /// Return the number of active worker Threads
        /// </summary>
        public Int32 WorkerCount
        {
            get
            {
                Int32 _value = 0;
                lock (WorkerThreads)
                {
                    _value = WorkerThreads.Count;
                }
                return _value;
            }
        }


        #region Constructor and Destructor

        /// <summary>
        /// Multi Threaded Webserver implementation
        /// Constructor
        /// </summary>
        /// <param name="Port">Port to listen for requests</param>
        /// <param name="RequestHandler">Request handler implementation</param>
        public HTTPServer(Int32 Port, IWebRequestHandler RequestHandler)
        {
            // Test for RequestHandler
            if (RequestHandler == null) throw new NullReferenceException("RequestHandler can't be null");

            // Store request handler
            this.RequestHandler = RequestHandler;

            // First setup Worker Manager
            ManageWorkers = new Thread(new ThreadStart(ManageWorkersThread));
            ManageWorkers.Start();

            // Create listening socket
            ListenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, Port);
            ListenSocket.Bind(localEndPoint);
            ListenSocket.Listen(Int32.MaxValue);

            // Start thread that monitors Socket
            AcceptConnection = new Thread(new ThreadStart(AcceptConnectionThread));
            AcceptConnection.Start();
        }

        /// <summary>
        /// Dispose the server
        /// Stops all workers, closses listening socket
        /// </summary>
        public void Dispose()
        {
            // Never give an Exeption in a Dispose method.. so we try
            try
            {
                // Stop listening
                ListenSocket.Close();

                // Stop Connection Accept Thread
                AcceptConnection.Abort();

                // Stop Worker Manager
                ManageWorkers.Abort();

                // Stop all Worker threads
                lock (WorkerThreads)
                {
                    foreach (Thread t in WorkerThreads)
                    {
                        t.Abort();
                    }

                }
            }
            catch
            {
                // Nothing
            }

        }

        #endregion

        #region Thread Based function (Workers and Management Thread)

        /// <summary>
        /// Monitor the listening socket for connections and spawn new workers
        /// </summary>
        private void AcceptConnectionThread()
        {
            while (Thread.CurrentThread.ThreadState != ThreadState.AbortRequested)
            {
                // Wait for a client to connect
                Socket AcceptSocket = ListenSocket.Accept();

                // Start new worker
                StartNewWorker(ref AcceptSocket);
            }
        }

        /// <summary>
        /// Spawn a new worker. If maximum of active workers is reached wait for a worker
        /// to complete
        /// </summary>
        /// <param name="AcceptSocket">Accepted socket</param>
        public void StartNewWorker(ref Socket AcceptSocket)
        {
            // Here the number of workers is stored
            int NumWorkers;

            // Wait until Worker Slot comes available
            while (true)
            {
                // Fetch Number of workers
                lock (WorkerThreads)
                {
                    NumWorkers = WorkerThreads.Count;
                }

                // Check if we are below limit
                if (NumWorkers < MaxWorkers) break;

                // If not wait some time
                Thread.Sleep(100);
            }

            // Create new worker
            HTTPServerWorker Worker = new HTTPServerWorker(ref AcceptSocket,RequestHandler);
            Thread WorkerThread = new Thread(new ThreadStart(Worker.DoWork));
            WorkerThread.Start();

            // Add to thread pool
            lock (WorkerThread)
            {
                WorkerThreads.Add(WorkerThread);
                DebugWorkers("A new Worker is added to the Worker Pool.");
                DebugWorkers("Worker Pool now contains " + WorkerThreads.Count + " workers.");
            }

        }

        /// <summary>
        /// All Worker related messages are send here, use it to debug workers
        /// </summary>
        /// <param name="DbgText"></param>
        private void DebugWorkers(String DbgText)
        {
            // Uncomment the next line to debug workers
            //Debug.Print(DbgText);
        }

        /// <summary>
        /// Thread that monitors workers, removing the workers that are finished
        /// </summary>
        private void ManageWorkersThread()
        {
            // Endless loop
            while (Thread.CurrentThread.ThreadState != ThreadState.AbortRequested)
            {
                // Give some resource space
                Thread.Sleep(250);

                // Lock Thread Pool
                lock (WorkerThreads)
                {
                    if (WorkerThreads.Count != 0)
                    {
                        // Check if Thread is completed.. if finished remove from worker list
                        for (int w = (WorkerThreads.Count - 1); w >= 0; w--)
                        {
                            // If Thread is stopped
                            if (((Thread)WorkerThreads[w]).ThreadState == ThreadState.Stopped)
                            {
                                // Remove worker from list
                                WorkerThreads.RemoveAt(w);
                                DebugWorkers("Worker " + w.ToString() + " is stopped. Removing from Worker Pool.");
                                DebugWorkers("Worker Pool now contains " + WorkerThreads.Count + " workers.");
                            }
                        }
                    }
                }
            }

        }

        #endregion


    }
}
