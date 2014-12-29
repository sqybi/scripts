using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace UdpPacketSender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random random;

        public MainWindow()
        {
            InitializeComponent();
            random = new Random();
        }

        private void SendPacketsButton_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(PacketsCountTextBox.Text);

            ResultLable.Content = "Creating socket";

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(IpTextBox.Text), int.Parse(PortTextBox.Text));

            int dataSize = int.Parse(DataSizeTextBox.Text);
            byte[] buffer = new byte[dataSize];

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < count; ++i)
            {
                random.NextBytes(buffer);
                socket.SendTo(buffer, endPoint);
                ResultLable.Content = string.Format("Sending {0}/{1} packet with data size {2}", i + 1, count, dataSize);
                Thread.Sleep(10);
            }

            stopwatch.Stop();
            ResultLable.Content = string.Format("{0} packets sent successfully in {1} seconds", count, stopwatch.Elapsed.TotalSeconds);
        }
    }
}
