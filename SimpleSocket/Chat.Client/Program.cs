// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("Char client stated");
var client = new ChatClient();
client.StartClient();

return 0;

public class ChatClient
{
    private readonly Thread _thread;
    private readonly Socket _sender;

    public ChatClient()
    {
        // Connect to a Remote server
        // Get Host IP Address that is used to establish a connection
        // In this case, we get one IP address of localhost that is IP : 127.0.0.1
        // If a host has multiple addresses, you will get a list of addresses
        var host = Dns.GetHostEntry("localhost");
        var ipAddress = host.AddressList[0];
        var remoteEP = new IPEndPoint(ipAddress, 11000);

        // Create a TCP/IP  socket.
        _sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        // Connect to Remote EndPoint
        try
        {
            _sender.Connect(remoteEP);

            Console.WriteLine("Socket connected to {0}",
                _sender.RemoteEndPoint);

            _thread = new Thread(GetMessages);
            _thread.Start();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void StartClient()
    {
        try
        {
            // Connect the socket to the remote endpoint. Catch any errors.
            byte[] msg;
            try
            {
                // Encode the data string into a byte array.
                msg = "This is a test<EOF>"u8.ToArray();
                // Send the data through the socket.
                _sender.Send(msg);

                string? message;
                do
                {
                    message = Console.ReadLine();
                    // Encode the data string into a byte array.
                    msg = Encoding.ASCII.GetBytes(message + "<EOF>");
                    // Send the data through the socket.
                    _sender.Send(msg);
                } while (message != null && !message.Contains("STOP"));
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane);
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public void GetMessages()
    {
        // Data buffer for incoming data.
        var bytes = new byte[1024];

        try
        {
            while (_sender.Connected)
            {
                // Receive the response from the remote device.
                var bytesRec = _sender.Receive(bytes);
                var message = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                Console.WriteLine(message);
            }

            // Release the socket.
            _sender.Close();
        }
        catch (ArgumentNullException ane)
        {
            Console.WriteLine("ArgumentNullException : {0}", ane);
        }
        catch (SocketException se)
        {
            Console.WriteLine("SocketException : {0}", se);
        }
        catch (Exception e)
        {
            Console.WriteLine("Unexpected exception : {0}", e);
        }
    }
}