using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketsChat
{
  public class WebSocketsMessageHandler : SocketHandler
  {
    public WebSocketsMessageHandler(ConnectionManager connections) : base(connections)
    {
    }

    public override async Task OnConnected(WebSocket socket)
    {
      await base.OnConnected(socket);
      var socketId = Connections.GetId(socket);
      await SendMessageToAll("new user just joined the chat");
    }

    public override async Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
    {
      var socketId = Connections.GetId(socket);
      var message = $"{Encoding.UTF8.GetString(buffer, 0, result.Count)}";
      await SendMessageToAll(message);
    }
  }
}
