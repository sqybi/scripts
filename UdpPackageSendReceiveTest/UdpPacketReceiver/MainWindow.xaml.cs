using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UdpPacketReceiver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private class Counter
        {
            private Label label;
            private int count = 0;
            private const string template = "{0} packets received";

            public Counter(Label label)
            {
                this.label = label;
            }

            public void Increase()
            {
                this.count++;
                RefreshLabel();
            }

            public void Clear()
            {
                this.count = 0;
                RefreshLabel();
            }

            private void RefreshLabel()
            {
                if (this.label != null)
                {
                    this.label.Dispatcher.Invoke(new Action(() => this.label.Content = string.Format(template, this.count)));
                }
            }
        }

        private readonly Counter counter;
        private Thread receiveThread = null;
        private Socket socket;

        public MainWindow()
        {
            InitializeComponent();
            counter = new Counter(PacketsCountLabel);
            counter.Clear();
        }

        private void ResetCountButton_Click(object sender, RoutedEventArgs e)
        {
            counter.Clear();
        }

        private void StartReceiveButton_Click(object sender, RoutedEventArgs e)
        {
            if (receiveThread == null)
            {
                receiveThread = new Thread(ReceiveData)
                {
                    IsBackground = true
                };
                receiveThread.Start(int.Parse(PortTextBox.Text));
                StartReceiveButton.Content = "Stop";
            }
            else
            {
                socket.Close();
                receiveThread.Abort();
                receiveThread = null;
                StartReceiveButton.Content = "Start";
            }
        }

        private void ReceiveData(object port)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, (int)port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(endPoint);

            EndPoint senderEndPoint = new IPEndPoint(IPAddress.Any, 0);

            byte[] buffer = new byte[1024 * 1024];
            while (true)
            {
                try
                {
                    socket.ReceiveFrom(buffer, ref senderEndPoint);
                }
                catch (Exception)
                {
                    break;
                }
                counter.Increase();
            }
        }
    }
}
