using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WebSocketsWpf.ViewModel
{
  public class BaseViewModel : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName]string properyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyName));
    }
  }
}
