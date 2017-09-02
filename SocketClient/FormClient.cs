using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace SocketClient
{
    public partial class FormClient : Form
    {
        //创建客户端套接字
        private Socket clientSocket;

        //创建负责监听服务端请求的线程    
        private Thread clientThread;

        public FormClient()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;

            cbIp.Items.AddRange(GetIpAddress());
            cbIp.SelectedIndex = 0;
        }

        private void btnBeginListen_Click(object sender, EventArgs e)
        {
            btnBeginListen.Enabled = false;
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddress = IPAddress.Parse(cbIp.Text.Trim());
            IPEndPoint endPoint = new IPEndPoint(ipAddress, int.Parse(tbPort.Text.Trim()));

            try
            {
                clientSocket.Connect(endPoint);
            }
            catch (Exception)
            {
                UpdateChatListBox(MessageType.Error, "连接失败");
                btnBeginListen.Enabled = true;
                return;
            }

            clientThread = new Thread(ReceiveMessage);
            clientThread.IsBackground = true;
            clientThread.Start();
        }

        private void ReceiveMessage()
        {
            int x = 0;
            while (true)
                try
                {
                    byte[] recMsgBytes = new byte[1024*1024];
                    int length = clientSocket.Receive(recMsgBytes);
                    string recMsg = Encoding.UTF8.GetString(recMsgBytes, 0, length);

                    if (x == 1)
                    {
                        UpdateChatListBox(MessageType.Receive, $"服务器:{recMsg}");
                    }
                    else
                    {
                        UpdateChatListBox(MessageType.Receive, recMsg);
                        x = 1;
                    }
                }
                catch (Exception)
                {
                    UpdateChatListBox(MessageType.Error, "远程服务器已经中断连接");
                    btnBeginListen.Enabled = true;
                    break;
                }
            // ReSharper disable once FunctionNeverReturns
        }

        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            ClientSendMessage(tbSendMsg.Text.Trim());
        }

        private void ClientSendMessage(string msg)
        {
            byte[] sendBytes = Encoding.UTF8.GetBytes(msg);
            clientSocket.Send(sendBytes);
            UpdateChatListBox(MessageType.Send, msg);
        }

        private void tbSendMsg_KeyDown(object sender, KeyEventArgs e)
        {
            //当光标位于文本框时 如果用户按下了键盘上的Enter键   
            if (e.KeyCode == Keys.Enter)
                ClientSendMessage(tbSendMsg.Text.Trim());
        }

        private void UpdateChatListBox(MessageType mt, string msg)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (mt)
            {
                case MessageType.Receive:
                    msg = $"--> {DateTime.Now} {msg}";
                    break;
                case MessageType.Send:
                    msg = $"<-- {DateTime.Now} {msg}";
                    break;
                case MessageType.Error:
                    msg = $"<!> {DateTime.Now} {msg}";
                    break;
                case MessageType.Default:
                    msg = $"### {DateTime.Now} {msg}";
                    break;
            }
            lbMsg.Items.Insert(lbMsg.Items.Count, msg);
            if (lbMsg.Items.Count > 100)
            {
                lbMsg.Items.RemoveAt(0);
            }
            lbMsg.SelectedIndex = lbMsg.Items.Count - 1;
            lbMsg.TopIndex = lbMsg.Items.Count - 1;
        }

        private void FormClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(@"是否退出？", @"操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                Dispose();
            else
                e.Cancel = true;
        }

        private void FormClient_Load(object sender, EventArgs e)
        {
        }

        private static string[] GetIpAddress()
        {
            string hostName = Dns.GetHostName(); //本机名
            //System.Net.IPAddress[] addressList = Dns.GetHostByName(hostName).AddressList;//会警告GetHostByName()已过期，我运行时且只返回了一个IPv4的地址   
            IPAddress[] addressList = Dns.GetHostAddresses(hostName); //会返回所有地址，包括IPv4和IPv6   
            List<string> ipList = new List<string>();
            Regex regex = new Regex(@"(192|10)\.\d+\.\d+\.\d+");
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (IPAddress ip in addressList)
            {
                bool isIPv4 = regex.IsMatch(ip.ToString());
                if (isIPv4)
                {
                    ipList.Add(ip.ToString());
                }
            }
            return ipList.ToArray();
        }

        private enum MessageType
        {
            Receive,
            Send,
            Error,
            Default
        }
    }
}