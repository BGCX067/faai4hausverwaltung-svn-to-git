using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Net;
using GHIElectronics.NETMF.Net.Sockets;
using GHIElectronics.NETMF.Net.NetworkInformation;
using System.Text;
using Socket = GHIElectronics.NETMF.Net.Sockets.Socket;

/// <summary>
/// This is a simple web server.  Given a request, it returns an HTML 
/// document.  The same document is returned for all requests and no parsing of 
/// the request is done.
/// </summary>
public static class MySocketServer
{
    public static void Main()
    {
        const Int32 c_port = 80;
        byte[] ip = { 192, 168, 0, 200 };
        byte[] subnet = { 255, 255, 255, 0 };
        byte[] gateway = { 192, 168, 0, 1 };
        byte[] mac = { 0x00, 0x26, 0x1C, 0x7B, 0x29,0xE8 };
        WIZnet_W5100.Enable(SPI.SPI_module.SPI1, (Cpu.Pin)FEZ_Pin.Digital.Di10,(Cpu.Pin)FEZ_Pin.Digital.Di7,true);
        NetworkInterface.EnableStaticIP(ip, subnet, gateway, mac);
        NetworkInterface.EnableStaticDns(new byte[] { 192, 168, 0, 1 });
        Socket server = new Socket(AddressFamily.InterNetwork,
        SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, c_port);
        server.Bind(localEndPoint);
        server.Listen(1);

        while (true)
        {
            // Wait for a client to connect.
            Socket clientSocket = server.Accept();

            // Process the client request.  true means asynchronous.
            new ProcessClientRequest(clientSocket, true);
        }
    }

    /// <summary>
    /// Processes a client request.
    /// </summary>
    internal sealed class ProcessClientRequest
    {
        private Socket m_clientSocket;

        /// <summary>
        /// The constructor calls another method to handle the request, but can 
        /// optionally do so in a new thread.
        /// </summary>
        /// <param name="clientSocket"></param>
        /// <param name="asynchronously"></param>
        public ProcessClientRequest(Socket clientSocket, Boolean asynchronously)
        {
            m_clientSocket = clientSocket;

            if (asynchronously)
                // Spawn a new thread to handle the request.
                new Thread(ProcessRequest).Start();
            else ProcessRequest();
        }

        /// <summary>
        /// Processes the request.
        /// </summary>
        private void ProcessRequest()
        {
            const Int32 c_microsecondsPerSecond = 1000000;

            // 'using' ensures that the client's socket gets closed.
            using (m_clientSocket)
            {
                // Wait for the client request to start to arrive.
                Byte[] buffer = new Byte[1024];
                if (m_clientSocket.Poll(5 * c_microsecondsPerSecond,
                    SelectMode.SelectRead))
                {
                    // If 0 bytes in buffer, then the connection has been closed, 
                    // reset, or terminated.
                    if (m_clientSocket.Available == 0)
                        return;

                    // Read the first chunk of the request (we don't actually do 
                    // anything with it).
                    Int32 bytesRead = m_clientSocket.Receive(buffer,
                        m_clientSocket.Available, SocketFlags.None);

                    // Return a static HTML document to the client.
                    String s =
                        "HTTP/1.1 200 OK\r\nContent-Type: text/html; charset=utf-8\r\n\r\n<html><head><title>.NET Micro Framework Web Server on USBizi Chipset </title></head>" +
                        "<body><bold><a href=\"http://www.tinyclr.com/\">Learn more about the .NET Micro Framework with FEZ by clicking here</a></bold></body></html>";
                    byte[] buf = Encoding.UTF8.GetBytes(s);

                    int offset = 0;
                    int ret = 0;
                    int len = buf.Length;
                    while (len > 0)
                    {
                        ret = m_clientSocket.Send(buf, offset, len, SocketFlags.None);
                        len -= ret;
                        offset += ret;
                    }

                    m_clientSocket.Close();
                }
            }
        }
    }
}
