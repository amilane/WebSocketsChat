using System;
using System.Collections.ObjectModel;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebSocketsWpf.ViewModel
{
  public class MainViewModel : BaseViewModel
  {
    public ClientWebSocket Client { get; set; }
    public ObservableCollection<string> Messages { get; set; }
    public string Message { get; set; }

    // "ws://localhost:5000/ws"
    public string Uri { get; set; }

    private bool connected;
    public bool Connected
    {
      get { return connected;}
      set
      {
        connected = value;
        OnPropertyChanged();
      }
    }

    private string login;
    public string Login {
      get { return login; }
      set {
        if (!String.IsNullOrWhiteSpace(value))
        {
          login = value;
          Connected = true;
        }
        else
        {
          login = String.Empty;
          Connected = false;
        }
      }
    }

    public MainViewModel()
    {
      Client = new ClientWebSocket();

      Messages = new ObservableCollection<string>();
      Messages.Add("Welcome to OpenChat! Enter any login to start chatting");
    }

    public async Task<bool> ConnectionAsync()
    {
      try
      {
        await Client.ConnectAsync(new Uri(Uri), CancellationToken.None);
        await UpdatingAsync();
        return true;
      }
      catch (Exception e)
      {
        return false;
      }
    }

    public async Task SendAsync()
    {
      var msg = new ArraySegment<byte>(Encoding.UTF8.GetBytes($"{Login}: {Message}"));
      await Client.SendAsync(msg, WebSocketMessageType.Text, true,
        CancellationToken.None);
    }

    public async Task UpdatingAsync()
    {
      var buffer = new byte[1024 * 4];
      while (true)
      {
        var result = await Client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
        Messages.Add(message);
        await Task.Delay(100);
      }
    }

  }
}
