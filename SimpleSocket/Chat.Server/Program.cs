// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("Char server stated");
var server = new Server();
var serverThread = new Thread(server.StartServer);
serverThread.Start();

internal class Server
{
    public List<ClientHandler> Clients { get; } = new();

    public void StartServer()
    {
        // Get Host IP Address that is used to establish a connection
        // In this case, we get one IP address of localhost that is IP :

        var host = Dns.GetHostEntry("localhost");
        var ipAddress = host.AddressList[0];
        var localEndPoint = new IPEndPoint(ipAddress, 11000);


        // Create a Socket that will use Tcp protocol
        var listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        // A Socket must be associated with an endpoint using the Bind method
        listener.Bind(localEndPoint);
        listener.Listen(10);
        // Specify how many requests a Socket can listen before it gives Server busy response.
        // We  listen 10 requests at a time
        while (true)
        {
            var socket = listener.Accept();
            var client = new ClientHandler(socket, this);
            Clients.Add(client);
            var clientThread = new Thread(client.StartReceive);
            clientThread.Start();
        }
    }

    public void Broadcast(string message)
    {
        foreach (var client in Clients) client.Send(message);
    }
}

internal class ClientHandler
{
    private readonly Server _server;
    private readonly Socket _socket;

    public ClientHandler(Socket socket, Server server)
    {
        _socket = socket;
        _server = server;
    }

    internal void Send(string message)
    {
        var msg = Encoding.ASCII.GetBytes(message);
        _socket.Send(msg);
    }

    internal void StartReceive()
    {
        while (_socket.Connected)
            try
            {
                // Incoming data from the client.
                var data = "";
                while (true)
                {
                    var bytes = new byte[1024];
                    var bytesRec = _socket.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("<EOF>", StringComparison.OrdinalIgnoreCase) > -1) break;
                }

                Console.WriteLine("Text received : {0}", data);
                _server.Broadcast(data);
            }
            catch (Exception e)
            {
                try
                {
                    _server.Clients.Remove(this);
                    _socket.Shutdown(SocketShutdown.Both);
                    _socket.Close();
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee);
                }

                Console.WriteLine(e);
            }
    }
}