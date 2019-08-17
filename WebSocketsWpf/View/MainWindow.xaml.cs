using System;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using WebSocketsWpf.Model;
using WebSocketsWpf.ViewModel;

namespace WebSocketsWpf.View
{

  public partial class MainWindow : Window
  {
    public MainViewModel _mainViewModel { get; set; }
    public MainWindow()
    {
      InitializeComponent();


      _mainViewModel = new MainViewModel();
      DataContext = _mainViewModel;

      _mainViewModel.Uri = deserializePath();

    }

    private async void BtnSendMessages_Click(object sender, RoutedEventArgs e)
    {
      await _mainViewModel.SendAsync();
      messageBox.Text = String.Empty;
    }

    private void Connect_Click(object sender, RoutedEventArgs e)
    {
      _mainViewModel.ConnectionAsync();

      btnConnect.IsEnabled = false;
      loginBox.IsEnabled = false;
      btnSendMessages.IsEnabled = true;
      btnConnect.Content = "Connected";
      status.Text = $"connection established @{DateTime.UtcNow:F}";
    }

    private void BtnSaveUri_Click(object sender, RoutedEventArgs e)
    {
      var pathToConnect = new PathToConnect(uriBox.Text);

      XmlSerializer serializer = new XmlSerializer(typeof(PathToConnect));

      using (FileStream fs = new FileStream("pathToConnect.xml", FileMode.OpenOrCreate))
      {
        serializer.Serialize(fs, pathToConnect);
      }
    }

    private string deserializePath()
    {
      XmlSerializer serializer = new XmlSerializer(typeof(PathToConnect));
      FileStream fs = new FileStream("pathToConnect.xml", FileMode.OpenOrCreate);
      TextReader reader = new StreamReader(fs);
      PathToConnect pathToConnect;
      
      try
      {
        pathToConnect = (PathToConnect)serializer.Deserialize(reader);
        return pathToConnect.uri;
      }
      catch (Exception e)
      {
        return "ws://localhost:5000/ws";
      }

    }
  }
}
