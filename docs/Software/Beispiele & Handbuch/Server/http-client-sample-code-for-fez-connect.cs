////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Modified to work with GHI's Wiznet W5100 Ethernet (TCP/IP) libraries.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Net;
using GHIElectronics.NETMF.Net.NetworkInformation;

/// This program demonstrates how to use the .NET Micro Framework HTTP classes 
/// to create a simple HTTP client that retrieves pages from several different 
/// websites, including secure sites.
namespace HttpClientSample
{
    public static class MyHttpClient
    {
        /// <summary>
        /// Retrieves a page from a Web server, using a simple GET request.
        /// </summary>
        public static void Main()
        {
            
            byte[] mac = { 0x00, 0x26, 0x1C, 0x7B, 0x29,0xE8 };
            WIZnet_W5100.Enable(SPI.SPI_module.SPI1, (Cpu.Pin)FEZ_Pin.Digital.Di10, (Cpu.Pin)FEZ_Pin.Digital.Di7, true); // WIZnet interface on FEZ Connect
            Dhcp.EnableDhcp(mac);
            Debug.Print("Network settings:");
            Debug.Print("IP Address: " + new IPAddress(NetworkInterface.IPAddress).ToString());
            Debug.Print("Subnet Mask: " + new IPAddress(NetworkInterface.SubnetMask).ToString());
            Debug.Print("Default Getway: " + new IPAddress(NetworkInterface.GatewayAddress).ToString());
            Debug.Print("DNS Server: " + new IPAddress(NetworkInterface.DnsServer).ToString());
            // Print the HTTP data from each of the following pages.
            PrintHttpData("http://www.ghielectronics.com/downloads/test.txt");
            PrintHttpData("http://www.tinyclr.com/compare/");
        }

        /// <summary>
        /// Prints the HTTP Web page from the given URL and status data while 
        /// receiving the page.
        /// </summary>
        /// <param name="url">The URL of the page to print.</param>
        public static void PrintHttpData(string url)
        {
            // Create an HTTP Web request.
            HttpWebRequest request = 
                HttpWebRequest.Create(url) as HttpWebRequest;

             // Set request.KeepAlive to use a persistent connection. 
            request.KeepAlive = true;

            // Get a response from the server.
            WebResponse resp = null;

            try
            {
                resp = request.GetResponse();
            }
            catch (Exception e)
            {
                Debug.Print("Exception in HttpWebRequest.GetResponse(): " + 
                    e.ToString());
            }

            // Get the network response stream to read the page data.
            if (resp != null)
            {
                Stream respStream = resp.GetResponseStream();
                string page = null;
                byte[] byteData = new byte[2048];
                char[] charData = new char[2048];
                int bytesRead = 0;
                Decoder UTF8decoder = System.Text.Encoding.UTF8.GetDecoder();
                int totalBytes = 0;

                // allow 5 seconds for reading the stream
                respStream.ReadTimeout = 5000; 

                // If we know the content length, read exactly that amount of 
                // data; otherwise, read until there is nothing left to read.
                if (resp.ContentLength != -1)
                {
                    for (int dataRem = (int)resp.ContentLength; dataRem > 0; )
                    {
                        Thread.Sleep(500);
                        bytesRead = 
                            respStream.Read(byteData, 0, byteData.Length);
                        if (bytesRead == 0)
                        {
                            Debug.Print("Error: Received " +
                                (resp.ContentLength - dataRem) + " Out of " +
                                resp.ContentLength);
                            break;
                        }
                        dataRem -= bytesRead;

                        // Convert from bytes to chars, and add to the page 
                        // string.
                        int byteUsed, charUsed;
                        bool completed = false;
                        totalBytes += bytesRead;
                        UTF8decoder.Convert(byteData, 0, bytesRead, charData, 0, 
                            bytesRead, true, out byteUsed, out charUsed, 
                            out completed);
                        page = new String(charData, 0, charUsed);
                        // Display the page results.
                        Debug.Print(page);

                    }

                    page = new String(
                        System.Text.Encoding.UTF8.GetChars(byteData));
                }
                else
                {
                    // Read until the end of the data is reached.
                    while (true)
                    {
                        // If the Read method times out, it throws an exception, 
                        // which is expected for Keep-Alive streams because the 
                        // connection isn't terminated.
                        try
                        {
                            Thread.Sleep(500);
                            bytesRead = 
                                respStream.Read(byteData, 0, byteData.Length);
                        }
                        catch (Exception)
                        {
                            bytesRead = 0;
                        }

                        // Zero bytes indicates the connection has been closed 
                        // by the server.
                        if (bytesRead == 0)
                        {
                            break;
                        }

                        int byteUsed, charUsed;
                        bool completed = false;
                        totalBytes += bytesRead;
                        UTF8decoder.Convert(byteData, 0, bytesRead, charData, 0, 
                            bytesRead, true, out byteUsed, out charUsed, 
                            out completed);
                        page =  new String(charData, 0, charUsed);
                        // Display the page results.
                        Debug.Print(page);
                        
                    }
                }


                // Close the response stream.  For Keep-Alive streams, the 
                // stream will remain open and will be pushed into the unused 
                // stream list.
                resp.Close();
            }
        }
    }
}
