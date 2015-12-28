using System;
using Microsoft.SPOT;
using ElzeKool.net;
using System.Text;
using System.Threading;
using System.Resources;
using System.IO;

namespace Haussteuerung
{
	public class RequestHandler : IWebRequestHandler
	{
		//private static Int32 RequestCount = 0;

		/// <summary>
		/// Function called when HTTP gets in
		/// </summary>
		/// <param name="Method">HTTP Method (GET or POST)</param>
		/// <param name="URL">URL requested</param>
		/// <param name="RequestHeaders">Request headers</param>
		/// <param name="GET">GET Values</param>
		/// <param name="POST">POST Values</param>
		/// <param name="Status">Status string to return (eg. HTTP/1.0 200 Ok)</param>
		/// <param name="ResponseHeaders">Response headers to return</param>
		/// <param name="ResponseBody">Response body</param>
		/// <returns></returns>
		public void HandleRequest(
		   String Method,
		   String URL,
		   String POSTBody,
		   HTTPHeader[] RequestHeaders,
		   RequestParameter[] GET,
		   RequestParameter[] POST,
		   ref String Status,
		   ref HTTPHeader[] ResponseHeaders,
		   ref byte[] ResponseBody)
		{
			// Send background
			if ((URL == "/background.jpg") & (Method == "GET"))
			{
				Status = "HTTP/1.0 200 OK";
				HTTPHeader.Set(ref ResponseHeaders, "Content-Type", "image/jpg");
				HTTPHeader.Set(ref ResponseHeaders, "Cache-Control", "no-cache");
				ResponseBody = Resources.GetBytes(Resources.BinaryResources.background);
			}


			//// Load prototype
			//if (URL == "/prototype.js")
			//{
			//    Status = "HTTP/1.0 200 OK";
			//    HTTPHeader.Set(ref ResponseHeaders, "Content-Type", "text/javascript");
			//    ResponseBody = Resources.GetBytes(Resources.BinaryResources.prototype);
			//}

			// Main page with form and post result
			//if (URL == "/")
			//{
			//    if (Method == "GET")
			//    {
			//        // Set Status Ok
			//        Status = "HTTP/1.0 200 OK";

			//        // Set correct content type
			//        HTTPHeader.Set(ref ResponseHeaders, "Content-Type", "text/html");

			//        // Get Form
			//        ResponseBody = Resources.GetBytes(Resources.BinaryResources.posttest);
			//    }

			//    if (Method == "POST")
			//    {
			//        // Set Status Ok
			//        Status = "HTTP/1.0 200 OK";

			//        // Set correct content type
			//        HTTPHeader.Set(ref ResponseHeaders, "Content-Type", "text/plain");

			//        // Output request headers
			//        String ResponseBodyTxt = "";
			//        ResponseBodyTxt = "Request header: \r\n";
			//        ResponseBodyTxt += HTTPHeader.OutputResponseHeader(RequestHeaders);
			//        ResponseBodyTxt += "\r\n";

			//        // Output post values
			//        ResponseBodyTxt += "Value 1: \r\n";
			//        ResponseBodyTxt += RequestParameter.Get(ref POST, "value1", true) + "\r\n";
			//        ResponseBodyTxt += "Value 2: \r\n";
			//        ResponseBodyTxt += RequestParameter.Get(ref POST, "value2", true) + "\r\n";

			//        // Convert to byte[] array
			//        ResponseBody = Encoding.UTF8.GetBytes(ResponseBodyTxt);
			//    }
			//}
		}
	}
}
