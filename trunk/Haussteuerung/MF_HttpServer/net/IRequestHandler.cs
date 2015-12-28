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

namespace ElzeKool.net
{
    /// <summary>
    /// Interface class for a Request handler
    /// </summary>
    public interface IWebRequestHandler
    {
        /// <summary>
        /// Function called when HTTP gets in
        /// </summary>
        /// <param name="Method">HTTP Method (GET or POST)</param>
        /// <param name="URL">URL requested</param>
        /// <param name="POSTBody">POST body</param>
        /// <param name="RequestHeaders">Request headers</param>
        /// <param name="GET">GET Values</param>
        /// <param name="POST">POST Values</param>
        /// <param name="Status">Status string to return (eg. HTTP/1.0 200 Ok)</param>
        /// <param name="ResponseHeaders">Response headers to return</param>
        /// <param name="ResponseBody">Response body (Return String or byte[] array)</param>
        /// <returns></returns>
        void HandleRequest(
            String Method,
            String URL,
            String POSTBody,
            HTTPHeader[] RequestHeaders,
            RequestParameter[] GET,
            RequestParameter[] POST,
            ref String Status,
            ref HTTPHeader[] ResponseHeaders,
            ref byte[] ResponseBody);
        
    }
}
