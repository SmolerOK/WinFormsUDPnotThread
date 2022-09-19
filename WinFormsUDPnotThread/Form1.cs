using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows.Forms;

namespace WinFormsUDPnotThread
{
    public partial class Form1 : Form
    {
        private byte[] dataOpenDoor0 = { 0x02, 0x1F, 0x00, 0x03, 0x61, 0x38 };
        private byte[] dataOpenDoor1 = { 0x02, 0x1F, 0x01, 0x03, 0xB9, 0x21 };
        private UdpClient client;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendMessage(dataOpenDoor0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SendMessage(dataOpenDoor1);
        }

        private void SendMessage(byte[] message)
        {
            try
            {
                IPAddress address = IPAddress.Parse(textBox1.Text);

                client = new UdpClient();
                client.Client.ReceiveTimeout = 1000;
                client.Client.SendTimeout = 1000;
                client.Connect(address, 8192);
                client.Send(dataOpenDoor0, dataOpenDoor0.Length);

                IPEndPoint iPEndPoint = new IPEndPoint(address, 8192);

                byte[] receiveByte = client.Receive(ref iPEndPoint);

                string hex = BitConverter.ToString(receiveByte, 0, receiveByte.Length);
                label2.Text = DateTime.Now + " -> " + hex;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
