// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.Sockets;

Console.WriteLine("Hello, World!");
var server = new Server();
var serverThread = new Thread(server.StartServer);
serverThread.Start();

internal class Server
{
    public List<ClientHandler> clients;

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
            clients.Add(client);
            var clientThread = new Thread(client.StartClient);
            clientThread.Start();
        }
    }

    public void Broadcast(string message)
    {
        foreach (var client in clients)
        {
                client.Send(message);
        }
    }
}

internal class ClientHandler
{
    private Socket socket;
    private Server server;

    public ClientHandler(Socket socket, Server server)
    {
        this.socket = socket;
        this.server = server;
    }

    internal void Send(string message)
    {
        throw new NotImplementedException();
    }

    internal void StartClient(object? obj)
    {
        while (true)
        {
            string message = "";
            server.Broadcast(message);
        }
    }
}