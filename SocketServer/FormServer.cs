using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace SocketServer
{
    public partial class FormServer : Form
    {
        //定义一个集合，存储客户端信息  
        private readonly Dictionary<string, Socket> clientDict = new Dictionary<string, Socket>();
        // 创建一个负责和客户端通信的套接字
        private readonly Socket connectionSocket;
        private Socket watchSocket;
        private Thread watchThread;

        public FormServer()
        {
            connectionSocket = null;
            InitializeComponent();
            // 关闭文本框的非法线程操作检查
            CheckForIllegalCrossThreadCalls = false;

            cbIp.Items.AddRange(GetIpAddress());
            cbIp.SelectedIndex = 0;
        }


        private void btnServerConn_Click(object sender, EventArgs e)
        {
            try
            {
                btnServerConn.Enabled = false;

                // 定义一个套接字用于监听客户端发来的信息。
                watchSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // 服务器发送信息。需要1个IP地址和端口号
                IPAddress ipAddress = IPAddress.Parse(cbIp.Text.Trim());
                // 将IP地址和端口绑定到网络节点endpoint上
                IPEndPoint endPoint = new IPEndPoint(ipAddress, int.Parse(tbPort.Text.Trim()));
                // 监听绑定的网络节点
                watchSocket.Bind(endPoint);
                watchSocket.Listen(20);
                // 创建一个监听线程
                watchThread = new Thread(WatchConnecting);
                // 将窗体线程设置为与后台同步
                watchThread.IsBackground = true;
                watchThread.Start();
                UpdateChatListBox(MessageType.Default, "开始监听客户端传来的消息");
            }
            catch (Exception ex)
            {
                btnServerConn.Enabled = true;
                UpdateChatListBox(MessageType.Error, ex.Message);
            }
        }

        private void OnlineListDisplay(string info)
        {
            cbClients.Items.Add(info);
            cbClients.SelectedIndex = cbClients.SelectedIndex == -1 ? 0 : cbClients.SelectedIndex;
        }

        /// <summary>
        ///     监听客户端发来的请求
        /// </summary>
        private void WatchConnecting()
        {
            while (true)
            {
                try
                {
                    Socket connection = watchSocket.Accept();
                    IPEndPoint endPoint = connection.RemoteEndPoint as IPEndPoint;
                    if (endPoint == null)
                    {
                        return;
                    }

                    // 获取客户端ip和端口  
                    IPAddress clientIp = endPoint.Address;
                    int clientPort = endPoint.Port;

                    //让客户显示"连接成功的"的信息 
                    string sendMsg = $"连接成功! 本地IP:{clientIp} 端口:{clientPort}";
                    byte[] sendBytes = Encoding.UTF8.GetBytes(sendMsg);
                    connection.Send(sendBytes);
                    UpdateChatListBox(MessageType.Default, sendMsg);

                    // 添加客户端连接情况
                    string dispMsg = $"成功与{connection.RemoteEndPoint}客户端建立连接!";
                    clientDict.Add(connection.RemoteEndPoint.ToString(), connection);
                    OnlineListDisplay(connection.RemoteEndPoint.ToString());
                    UpdateChatListBox(MessageType.Default, dispMsg);

                    //创建一个通信线程
                    ParameterizedThreadStart pts = ServerReceiveMessage;
                    Thread thread = new Thread(pts);
                    thread.IsBackground = true;
                    thread.Start(connection);
                }
                catch (Exception ex)
                {
                    UpdateChatListBox(MessageType.Error, ex.Message);
                    break;
                }
            }
            // ReSharper disable once FunctionNeverReturns
        }

        /// <summary>
        ///     发送信息到客户端的方法
        /// </summary>
        /// <param name="sendMsg"></param>
        private void ServerSendMessage(string sendMsg)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(sendMsg);
            if (cbClients.SelectedIndex == -1)
            {
                MessageBox.Show(@"请选择要发送的客户端！", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                string selectClient = cbClients.Text;
                clientDict[selectClient].Send(bytes);
                UpdateChatListBox(MessageType.Send, sendMsg);
            }
        }

        /// <summary>
        ///     接收客户端发来的信息
        /// </summary>
        /// <param name="clientParaSocket"></param>
        private void ServerReceiveMessage(object clientParaSocket)
        {
            Socket serverSocket = clientParaSocket as Socket;
            if (serverSocket == null)
            {
                return;
            }

            while (true)
            {
                // 创建一个内存缓冲区，大小为1024*1024字节，即1M
                byte[] serverRecMsgBytes = new byte[1024*1024];
                try
                {
                    // 将接收到的信息存入到内存缓冲区， 并返回其字节数组的长度 
                    int length = serverSocket.Receive(serverRecMsgBytes);
                    // 将机器接收到的字节数组转换为人可读懂的字符串
                    string recMsg = Encoding.UTF8.GetString(serverRecMsgBytes, 0, length);
                    UpdateChatListBox(MessageType.Receive, recMsg);
                }
                catch (Exception)
                {
                    UpdateChatListBox(MessageType.Error, $"客户端{serverSocket.RemoteEndPoint}已经中断连接");

                    cbClients.Items.Remove(serverSocket.RemoteEndPoint.ToString());
                    serverSocket.Close();

                    break;
                }
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            ServerSendMessage(tbSendMsg.Text.Trim());
        }

        private void tbSendMsg_KeyDown(object sender, KeyEventArgs e)
        {
            // 如果按下enter键则发送消息
            if (e.KeyCode == Keys.Enter)
            {
                ServerSendMessage(tbSendMsg.Text.Trim());
            }
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

        private void FormServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(@"是否退出？", @"操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Dispose();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private static string[] GetIpAddress()
        {
            string hostName = Dns.GetHostName(); //本机名
            IPAddress[] addressList = Dns.GetHostAddresses(hostName); //会返回所有地址，包括IPv4和IPv6   
            List<string> ipList = new List<string>();
            Regex regex = new Regex(@"\d+\.\d+\.\d+\.\d+");
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