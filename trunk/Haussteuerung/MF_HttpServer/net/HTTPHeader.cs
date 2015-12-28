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

// Used for ArrayList
using System.Collections;

namespace ElzeKool.net
{
    /// <summary>
    /// Class used for passing response and request headers from and to the
    /// request handler.
    /// 
    /// Use the static HTTPHeader.Get and HTTPHeader.Set functions to modify the
    /// request header arrays
    /// </summary>
    public class HTTPHeader
    {
        /// <summary>
        /// Header field name
        /// It's better to use the static HTTPHeader.Get and HTTPHeader.Set functions to modify the
        /// request header arrays
        /// </summary>
        public String Field = "";

        /// <summary>
        /// Header field name
        /// It's better to use the static HTTPHeader.Get and HTTPHeader.Set functions to modify the
        /// request header arrays
        /// </summary>
        public String Value = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Field">Field</param>
        /// <param name="Value">Value</param>
        public HTTPHeader(String Field, String Value)
        {
            // Test Field Value, Value CAN be null
            if (Field == null) throw new NullReferenceException("Field can't be null");
            
            this.Field = Field;
            this.Value = Value;
        }

        #region Static functions

        /// <summary>
        /// Explode String Subject with Seperator
        /// </summary>
        /// <param name="Seperator"></param>
        /// <param name="Subject"></param>
        /// <returns></returns>
        private static String[] Explode(String Seperator, String Subject)
        {
            // Test input parameters
            if (Seperator == null) throw new NullReferenceException("Seperator can't be null");
            if (Subject == null) throw new NullReferenceException("Subject can't be null");

            ArrayList Strings = new ArrayList();
            Int32 StrPos = 0;
            Int32 _StrPos = 0;

            while (true)
            {
                _StrPos = Subject.IndexOf(Seperator, StrPos);
                if (_StrPos == -1)
                {
                    if (!((StrPos + Seperator.Length) >= Subject.Length))
                        Strings.Add(Subject.Substring(StrPos));
                    break;
                }
                Strings.Add(Subject.Substring(StrPos, _StrPos - StrPos));
                StrPos = _StrPos + Seperator.Length;
            }

            Int32 RetListPos = 0;
            String[] RetList = new String[Strings.Count];
            foreach (String s in Strings) { RetList[RetListPos] = s; RetListPos++; }

            return RetList;
        }

        /// <summary>
        /// Get Value from header array
        /// </summary>
        /// <param name="Headers">Header array</param>
        /// <param name="Field">Field to get</param>
        /// <returns>Value, null if not found</returns>
        public static String Get(ref HTTPHeader[] Headers, String Field)   { return Get(ref Headers, Field, null);  }
        public static String Get(ref HTTPHeader[] Headers, String Field, String Default)
        {
            // Test input parameters
            if (Headers == null) return Default;
            if (Field == null) throw new NullReferenceException("Field can't be null");

            String retString = Default;
            for (int x = 0; x < Headers.Length; x++) { if (Headers[x].Field.ToLower() == Field.ToLower()) retString = Headers[x].Value; }
            return retString;
        }

        /// <summary>
        /// Set value in header array
        /// </summary>
        /// <param name="Headers">Header array</param>
        /// <param name="Field">Field to set</param>
        /// <param name="Value">Value to set</param>
        public static void Set(ref HTTPHeader[] Headers, String Field, String Value)
        {
            // Test Field Value, Value CAN be null
            if (Field == null) throw new NullReferenceException("Field can't be null");

            // If headers are empty just create a new headers array
            if (Headers == null)
            {
                Headers = new HTTPHeader[] { new HTTPHeader(Field,Value) };
            }
            else
            {
                // Check for field in header
                int HeaderIndex = -1;
                for (int x = 0; x < Headers.Length; x++) { if (Headers[x].Field == Field) HeaderIndex = x; }

                // Check if field already in header
                if (HeaderIndex != -1)
                {
                    // Just update the correct field
                    Headers[HeaderIndex].Value = Value;
                }
                else
                {
                    // Create new array one element bigger
                    HTTPHeader[] nHeaders = new HTTPHeader[Headers.Length + 1];
                    Array.Copy(Headers, nHeaders, Headers.Length);

                    // Insert new header element
                    nHeaders[nHeaders.Length - 1] = new HTTPHeader(Field, Value);

                    // Update header to the new array
                    Headers = nHeaders;
                }
            }
        }
        
        /// <summary>
        /// Output Response headers to string
        /// </summary>
        /// <param name="ResponseHeaders">Response headers to return</param>
        /// <returns>String that can be output to client</returns>
        public static String OutputResponseHeader(HTTPHeader[] ResponseHeaders)
        {
            // Test input parameters
            if (ResponseHeaders == null) throw new NullReferenceException("ResponseHeaders can't be null");

            // This will store the response
            String ResponseString = "";

            // Go to array
            foreach (HTTPHeader h in ResponseHeaders)
            {
                // Skip empty elements
                if ((h.Field != "") & (h.Value != null))
                {
                    ResponseString += h.Field + ": " + h.Value + "\r\n";
                }
            }

            // Return the string
            return ResponseString;
        }

        /// <summary>
        /// Parse request header into HTTPHeader array
        /// </summary>
        /// <param name="Request">Request from client</param>
        /// <returns>HTTPHeader array</returns>
        public static HTTPHeader[] ParseRequestHeader(String Request)
        {
            // Dont parse request body (In case of POST)
            if (Request.IndexOf("\r\n\r\n") != -1) Request = Request.Substring(0, Request.IndexOf("\r\n\r\n"));

            // Explode header
            String[] ExplodedHeader = Explode("\r\n", Request);

            // First line is request method and uri 
            if (ExplodedHeader.Length <= 1) return null;
            HTTPHeader[] _HTTPHeaders = new HTTPHeader[ExplodedHeader.Length - 1];

            // Add header parts
            for (int x = 0; x < _HTTPHeaders.Length; x++)
            {
                String Field = ExplodedHeader[x + 1].Substring(0, ExplodedHeader[x + 1].IndexOf(':'));
                String Value = ExplodedHeader[x + 1].Substring(Field.Length + 2);
                _HTTPHeaders[x] = new HTTPHeader(Field, Value);
            }

            // Return headers
            return _HTTPHeaders;

        }

        #endregion
    }
}
