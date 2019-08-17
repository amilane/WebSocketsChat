using System;

namespace WebSocketsWpf.Model
{
  [Serializable]
  public class PathToConnect
  {
    public string uri;
    public PathToConnect()
    {
    }

    public PathToConnect(string uri)
    {
      this.uri = uri;
    }
  }
}
