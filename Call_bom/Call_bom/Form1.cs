using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data.Common;

namespace Call_bom
{
    public partial class MainForm : Form
    {
        
        public MainForm()
        {
            InitializeComponent();
            //关闭对文本框的非法线程操作检查
            TextBox.CheckForIllegalCrossThreadCalls = false;
            StartServer();

            ReadTxtFile(writeBomFilePath, 0);
            ReadTxtFile(writeCarFilePath, 1);
            ShowBomInfomationFromDataBase();
           
        }


        string baseInstruction = "AA55000970";   //基础指令
        string crcInstruction = "0000";         //CRC检验指令
        string bomAndRandomInstruction = "0200";    //叫料任务和随机数指令
        string carAndRandomInstruction = "0100";    //叫车任务和随机数指令
        string avgComfirmTaskInstruction = "aa5500046b00"; //服务器确认收到 AGV完成任务的指令

        int callCarFlag = 0;

        string writeCarFilePath = @"..\WaitCar.txt";            //叫车任务存储路径
        string writeBomFilePath = @"..\WaitBom.txt";            //叫料任务存储路径

        int txtFlag = 0;                                        //txt 标记，0代表叫料，1代表叫车


        int stop_send_flag = 0;     //停止发送指令


        

        string sqlSelect = "SELECT * FROM BomInformation WHERE Check_State =''";
        


        // Thread threadWatch = null; //负责监听客户端的线程


        Dictionary<string, Socket> dict = new Dictionary<string, Socket>();      //存储Socket
        Dictionary<string, Thread> dictTread = new Dictionary<string, Thread>(); //存储Thread

        string response = null;    //回复数据

        SortedSet<string> stationSet = new SortedSet<string>();                  //站点集合
        

        private void StartServer()
        {
            //定义一个套接字用于监听客户端发来的信息  包含3个参数(IP4寻址协议,流式连接,TCP协议)
            //socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //服务端发送信息 需要1个IP地址和端口号,从app.config 读取IP
            string strIp = ConfigHelper.GetAppConfig("ServerIP");
            string bomPort = ConfigHelper.GetAppConfig("BomPort");
            string carPort = ConfigHelper.GetAppConfig("CarPort");
            IPAddress ipaddress = IPAddress.Parse(strIp);
            //将IP地址和端口号绑定到网络节点endpoint上 
            //txtTest.AppendText(strIp + ":");
            //txtTest.AppendText(bomPort);
            IPEndPoint endpointBom = new IPEndPoint(IPAddress.Any, int.Parse(bomPort)); //从app.config 读取平板 Port
            IPEndPoint endpointCar = new IPEndPoint(IPAddress.Any, int.Parse(carPort)); //从app.config 读取小车 Port

            //监听平板
            Socket socketBom = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socketBom.Bind(endpointBom);
            socketBom.Listen(100);
            Thread threadBom = new Thread(ListenBom);
            threadBom.IsBackground = true;
            threadBom.Start(socketBom);

            //监听小车
            Socket socketCar = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socketCar.Bind(endpointCar);
            socketCar.Listen(100);
            Thread threadCar = new Thread(ListenCar);
            threadCar.IsBackground = true;
            threadCar.Start(socketCar);

            //ipPoints = new IPEndPoint[2]{endpointBom, endpointCar};

            //scon = new Socket[ipPoints.Length];
            //for(int i = 0; i < ipPoints.Length; i++)
            //{
            //    scon[i] = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //    scon[i].Bind(ipPoints[i]);
            //    scon[i].Listen(100);
            //    Thread thread = new Thread(WatchConnecting);
            //    thread.IsBackground = true;
            //    thread.Start(scon[i]);
            //}
            //ListenPorts lp = new ListenPorts(ipPoints);
            //lp.beginListen();

            //监听绑定的网络节点
            // socketWatch.Bind(endpoint);
            //  socketWatch.Bind(endpointCar);
            //将套接字的监听队列长度限制为
            // socketWatch.Listen(4096);
            //创建一个监听线程 
            // threadWatch = new Thread(WatchConnecting);
            //将窗体线程设置为与后台同步
            // threadWatch.IsBackground = true;
            //启动线程
            //threadWatch.Start();

        }



        //private void btnStartServer_Click(object sender, EventArgs e)
        //{
        //    //定义一个套接字用于监听客户端发来的信息  包含3个参数(IP4寻址协议,流式连接,TCP协议)
        //    socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //    //服务端发送信息 需要1个IP地址和端口号
        //    IPAddress ipaddress = IPAddress.Parse(txtIP.Text.Trim()); //获取文本框输入的IP地址
        //    //将IP地址和端口号绑定到网络节点endpoint上 
        //    IPEndPoint endpoint = new IPEndPoint(ipaddress, int.Parse(txtPort.Text.Trim())); //获取文本框上输入的端口号
        //    //监听绑定的网络节点
        //    socketWatch.Bind(endpoint);
        //    //将套接字的监听队列长度限制为20
        //    socketWatch.Listen(20);
        //    //创建一个监听线程 
        //    threadWatch = new Thread(WatchConnecting);
        //    //将窗体线程设置为与后台同步
        //    threadWatch.IsBackground = true;
        //    //启动线程
        //    threadWatch.Start();
        //    //启动线程后 txtMsg文本框显示相应提示
        //    txtMsg.AppendText("开始监听客户端传来的信息!" + "\r\n");
        //}


        //创建一个负责和客户端通信的套接字 
        //Socket socConnection = null;

        /// <summary>
        /// 监听平板发来的请求
        /// </summary>
        private void ListenBom(object objs)
        {
            Socket scon = objs as Socket;

            while (true)  //持续不断监听客户端发来的请求
            {
                Socket newSocket = scon.Accept();
                //txtMsg.AppendText("客户端连接成功" + "\r\n");
                //创建一个通信线程 
                ParameterizedThreadStart pts = new ParameterizedThreadStart(ServerRecMsgFromBom);
                Thread thr = new Thread(pts);
                thr.IsBackground = true;
                //启动线程
                thr.Start(newSocket);

                dict.Add(newSocket.RemoteEndPoint.ToString(), newSocket);

                //lbOnline.Items.Add(newSocket.RemoteEndPoint.ToString());
                dictTread.Add(newSocket.RemoteEndPoint.ToString(), thr);
            }
        }

        /// <summary>
        /// 监听小车发来的请求
        /// </summary>
        private void ListenCar(object objs)
        {
            Socket scon = objs as Socket;

            while (true)  //持续不断监听客户端发来的请求
            {
                Socket newSocket = scon.Accept();
                lblLinkStatu.Text = "连接状态：已连接";
                lblWorkStatu.Text = "工作状态：空闲";
                lblLinkStatu.BackColor = Color.LightGreen;
                lblWorkStatu.BackColor = Color.LightGreen;
                //txtMsg.AppendText("客户端连接成功" + "\r\n");
                //创建一个通信线程 
                ParameterizedThreadStart pts = new ParameterizedThreadStart(ServerRecMsgFromCar);
                Thread thr = new Thread(pts);
                thr.IsBackground = true;
                //启动线程
                thr.Start(newSocket);

                //dict.Add(newSocket.RemoteEndPoint.ToString(), newSocket);

                //lbOnline.Items.Add(newSocket.RemoteEndPoint.ToString());
                //dictTread.Add(newSocket.RemoteEndPoint.ToString(), thr);
            }
        }
        /// <summary>
        /// 接收平板发来的信息 
        /// </summary>
        /// <param name="socketClientPara">客户端套接字对象</param>
        private void ServerRecMsgFromBom(object socketClientPara)
        {
            Socket socketServer = socketClientPara as Socket;
            while (true)
            {
                if (IsConnected(socketServer))
                {


                    //创建一个内存缓冲区 其大小为1024*1024字节  即1M
                    byte[] arrServerRecMsg = new byte[1024 * 1024];
                    //将接收到的信息存入到内存缓冲区,并返回其字节数组的长度
                    try
                    {
                        int length = socketServer.Receive(arrServerRecMsg);
                        //将机器接受到的字节数组转换为人可以读懂的字符串
                        string strRecMsg = Encoding.GetEncoding("gb2312").GetString(arrServerRecMsg, 0, length);
                        //去除字符串中的空格，回车，换行符，制表符
                        string strDataReceive = strRecMsg.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
                        if (length > 4096)
                        {
                            response = "AA55";
                        }
                        else
                            response = "AA00";
                        //将发送的字符串信息附加到文本框txtMsg上  
                        //txtMsg.AppendText("天之涯:" + GetCurrentTime() + "\r\n" + strSRecMsg + "\r\n");
                        //txtTest.AppendText(strDataReceive);
                        ServerSendMsg(response, socketServer);

                        //为数据添加接收时间
                        DataProcess(strRecMsg);


                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {

                    dict.Remove(socketServer.RemoteEndPoint.ToString());
                    dictTread.Remove(socketServer.RemoteEndPoint.ToString());
                    //lbOnline.Items.Remove(socketServer.RemoteEndPoint.ToString());
                    socketServer.Shutdown(SocketShutdown.Both);
                    socketServer.Close();
                    break;
                }

            }
        }


        /// <summary>
        /// 接收小车发来的信息 
        /// </summary>
        /// <param name="socketClientPara">客户端套接字对象</param>
        private void ServerRecMsgFromCar(object socketClientPara)
        {
            Socket socketServer = socketClientPara as Socket;
            while (true)
            {
                if (IsConnected(socketServer))
                {


                    //创建一个内存缓冲区 其大小为1024*1024字节  即1M
                    byte[] arrServerRecMsg = new byte[1024 * 1024];
                    //将接收到的信息存入到内存缓冲区,并返回其字节数组的长度
                    try
                    {
                        int length = socketServer.Receive(arrServerRecMsg);

                        //将机器接受到的字节数组转换为人可以读懂的字符串

                        //string strRecMsg = Encoding.UTF8.GetString(arrServerRecMsg, 0, length);


                        string receiveCar = null;
                        for (int i = 0; i < length; i++)
                            receiveCar += arrServerRecMsg[i].ToString("X2");//转成16进制字符串显示


                        //去除字符串中的空格，回车，换行符，制表符
                        string strDataReceive = receiveCar.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
                        string CarStatus = strDataReceive.Substring(10, 2) + strDataReceive.Substring(20, 4);

                        if (CarStatus == "000100")  //小车空闲
                        {
                            if (stop_send_flag == 0)
                            {
                                if (lstCallBom.Items.Count > 0)  //如果有叫料任务，没有叫车任务
                                {
                                    string[] sendStationArray = lstCallBom.Items[0].ToString().Split(' ');
                                    lstCallBom.Items[0] = sendStationArray[0] + " " + "号站点正在配送";
                                    lblWorkStatu.Text = "工作状态：繁忙";
                                    lblWorkStatu.BackColor = Color.Transparent;
                                    string sendStationToAGV = GroupBomInstruction(sendStationArray[0]);    //组合叫料指令
                                    ServerSendMsgToAGV(sendStationToAGV, socketServer);                    //发送数据


                                }
                                else if (lstCallBom.Items.Count == 0 && lstCallCar.Items.Count > 0) //如果有叫车任务，没有叫料任务
                                {
                                    string[] sendStationArray = lstCallCar.Items[0].ToString().Split(' ');
                                    lstCallCar.Items[0] = sendStationArray[0] + " " + "号站点车正在前往";
                                    lblWorkStatu.Text = "工作状态：繁忙";
                                    lblWorkStatu.BackColor = Color.Transparent;
                                    string sendStationToAGV = GroupCarInstruction(sendStationArray[0]);    //组合叫车指令
                                    ServerSendMsgToAGV(sendStationToAGV, socketServer);

                                    callCarFlag = 1;                                          //叫车标记，正在执行叫车任务
                                }
                            }
                        }
                        else if (CarStatus == "000108")  //判断AGV是否完成任务
                        {
                            ServerSendMsgToAGV(avgComfirmTaskInstruction, socketServer);     

                            if (callCarFlag == 0 && lstCallBom.Items.Count == 1)
                            {
                                lstCallBom.Items.RemoveAt(0);

                                ClearTxtFile(writeBomFilePath, 0);

                            }
                            else if (callCarFlag == 0 && lstCallBom.Items.Count > 1)
                            {
                                lstCallBom.Items.RemoveAt(0);

                                ClearTxtFile(writeBomFilePath, 0);
                                WriteTxtFile(writeBomFilePath, 0);
                            }
                            else if (lstCallCar.Items.Count == 0)
                            {
                                lstCallCar.Items.RemoveAt(0);

                                ClearTxtFile(writeCarFilePath, 1);

                                callCarFlag = 0;
                            }
                            else if (lstCallCar.Items.Count > 1)
                            {
                                lstCallCar.Items.RemoveAt(0);

                                ClearTxtFile(writeCarFilePath, 1);
                                WriteTxtFile(writeCarFilePath, 1);

                                callCarFlag = 0;
                            }
                        }

                        //if (length > 4096)
                        //{
                        //    response = "AA55";
                        //}
                        //else
                        //    response = "AA00";
                        //将发送的字符串信息附加到文本框txtMsg上  
                        //txtMsg.AppendText("天之涯:" + GetCurrentTime() + "\r\n" + strSRecMsg + "\r\n");
                        //txtTest.AppendText(strDataReceive);

                        ////为数据添加接收时间
                        //DataProcess(receiveCar);


                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    lblLinkStatu.Text = "连接状态：未连接";
                    lblWorkStatu.Text = "工作状态：";
                    lblLinkStatu.BackColor = Color.Transparent;
                    lblWorkStatu.BackColor = Color.Transparent;
                    dict.Remove(socketServer.RemoteEndPoint.ToString());
                    dictTread.Remove(socketServer.RemoteEndPoint.ToString());
                    //lbOnline.Items.Remove(socketServer.RemoteEndPoint.ToString());
                    socketServer.Shutdown(SocketShutdown.Both);
                    socketServer.Close();
                    break;
                }

            }
        }

        /// <summary>
        /// 组合叫料指令
        /// </summary>
        /// <param name="bomStation"></param>
        /// <returns></returns>
        public string GroupBomInstruction(string bomStation)
        {
            string callBomInstruction = null;          //单站点
            string callMutiBomInstruction = null;      //多站点
            string callAllBomInsruction = null;


            if (!bomStation.Contains(','))
            {

                bomStation = int.Parse(bomStation).ToString("X");   //将站点转成16进制

                if (bomStation.Length == 1)
                {
                    //int station =  int.Parse(bomStation);
                    //station = int.Parse(station.ToString("X"));
                    callBomInstruction = "0000000" + bomStation;
                }
                else if (bomStation.Length == 2)
                {
                    callBomInstruction = "000000" + bomStation;
                }
                else if (bomStation.Length == 3)
                {
                    callBomInstruction = "00000" + bomStation;
                }
                callAllBomInsruction = baseInstruction + callBomInstruction + bomAndRandomInstruction + crcInstruction;
            }
            else
            {
                string[] stationArray = bomStation.Split(',');
                for (int i = 0; i < stationArray.Length; i++)
                {
                    if (stationArray[i].Length == 1)
                    {
                        //int station =  int.Parse(bomStation);
                        //station = int.Parse(station.ToString("X"));
                        callBomInstruction = "0000000" + int.Parse(stationArray[i]).ToString("X");
                    }
                    else if (stationArray[i].Length == 2)
                    {
                        callBomInstruction = "000000" + int.Parse(stationArray[i]).ToString("X");
                    }
                    else if (stationArray[i].Length == 3)
                    {
                        callBomInstruction = "00000" + int.Parse(stationArray[i]).ToString("X");
                    }
                    callMutiBomInstruction += callBomInstruction + "0A";

                }

                int mutiStationCount = stationArray.Length * 5 + 6;                 //计算多站点个数
                string mutiStationHex = mutiStationCount.ToString("X2");            //转成16进制
                callAllBomInsruction = "AA5500" + mutiStationHex + "73" + "0200" + callMutiBomInstruction + "00" + crcInstruction;   //拼接成指令

            }
            return callAllBomInsruction;
        }

        /// <summary>
        /// 组合叫车任务
        /// </summary>
        /// <param name="bomStation"></param>
        /// <returns></returns>
        public string GroupCarInstruction(string bomStation)
        {
            string callCarInstruction = null;          //单站点

            string callCarInsruction = null;


            bomStation = int.Parse(bomStation).ToString("X");   //将站点转成16进制

            if (bomStation.Length == 1)
            {
                //int station =  int.Parse(bomStation);
                //station = int.Parse(station.ToString("X"));
                callCarInstruction = "0000000" + bomStation;
            }
            else if (bomStation.Length == 2)
            {
                callCarInstruction = "000000" + bomStation;
            }
            else if (bomStation.Length == 3)
            {
                callCarInstruction = "00000" + bomStation;
            }
            callCarInsruction = baseInstruction + callCarInstruction + carAndRandomInstruction;


            return callCarInsruction;
        }

        public bool IsConnected(Socket socket)
        {
            try
            {
                return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
            }
            catch (SocketException) { return false; }
        }

        private void DataProcess(string strMsg)
        {
            string strData = strMsg;
            DateTime timeNow = DateTime.Now;
            strData = strMsg + "@" + timeNow + "+" + "55";

            ShowBom(strData);
            WriteToDatabase(strData);
        }

        /// <summary>
        /// 显示收到的物料信息
        /// </summary>
        /// <param name="strData"></param>
        private void ShowBom(string strData)
        {
            string data = strData.Substring(0, strData.Length - 3);
            string[] allBomlist = data.Split('@');
            string strAllBom = allBomlist[0];
            string nowTime = allBomlist[1];
            string[] strRecieveMsg = strAllBom.Split('+');
            string station = null;
            if (strRecieveMsg.Length < 2)
                return;

            if (strRecieveMsg[1] == "66")
            {
                station = strRecieveMsg[2] + " " + "号站点等待配车";
                lstCallCar.Items.Add(station);


                // 清空叫车txt文件
                ClearTxtFile(writeCarFilePath, 1);

                //写入叫车txt文件
                WriteTxtFile(writeCarFilePath, 1);

            }
            else 
            {
                if(strData.IndexOf("#加急") > 0)     //如果指令有加急
                {
                    DateTime dt = DateTime.Now;
                    string timeReceive = dt.ToString("HH:mm:ss");
                    string[] allBom = strAllBom.Split('&');
                    int count = allBom.Length;
                    for (int i = 0; i < count; i++)
                    {
                        string[] split_Bom = allBom[i].Split('+');
                        dgvShowBom.Rows.Add("0",timeReceive, split_Bom[1], split_Bom[2], split_Bom[3], split_Bom[4], split_Bom[5], split_Bom[6], false);
                        dgvShowBom.Rows[dgvShowBom.Rows.Count -1].DefaultCellStyle.ForeColor = Color.Red;

                    }
                }

                else if(strData.IndexOf("#加急") < 0)
                {
                    DateTime dt = DateTime.Now;
                    string timeReceive = dt.ToString("HH:mm:ss");
                    string[] allBom = strAllBom.Split('&');
                    int count = allBom.Length;
                    for (int i = 0; i < count; i++)
                    {
                        string[] split_Bom = allBom[i].Split('+');
                        dgvShowBom.Rows.Add("0",timeReceive, split_Bom[1], split_Bom[2], split_Bom[3], split_Bom[4], split_Bom[5], split_Bom[6], false);
                    }
                }


            }



        }

        /// <summary>
        /// 写入数据库
        /// </summary>
        /// <param name="strData"></param>
        public void WriteToDatabase(string strData)
        {
            string data = strData.Substring(0, strData.Length - 3);
            string[] allBomlist = data.Split('@');
            string strAllBom = allBomlist[0];
            string nowTime = allBomlist[1];
            string[] strRecieveMsg = strAllBom.Split('+');

            string[] allBom = strAllBom.Split('&');
            int count = allBom.Length;

            DateTime dt = DateTime.Now;
            string time = dt.ToString("HH:mm:ss");

            SQLiteHelper db = new SQLiteHelper(@"C:/Users/admin/Desktop/Call_Bom/Call_bom/Call_bom/bin/Debug/BomDB.sqlite");
            string sql = "INSERT INTO BomInformation(Call_Date, Call_time, Station, Work_Station, Bom_Number, Bom_Describe, Storage_Location, Quantity, Check_State, Check_Time) values(@Call_Date,@Call_Time,@Station,@Work_Station,@Bom_Number,@Bom_Describe,@Storage_Location,@Quantity,@Check_State,@Check_Time)";

            for(int i = 0; i < count; i++)
            {
                string[] split_Bom = allBom[i].Split('+');

                SQLiteParameter[] parameters = {
                                                new SQLiteParameter("@Call_Date"),
                                                new SQLiteParameter("@Call_Time"),
                                                new SQLiteParameter("@Station"),
                                                new SQLiteParameter("@Work_Station"),
                                                new SQLiteParameter("@Bom_Number"),
                                                new SQLiteParameter("@Bom_Describe"),
                                                new SQLiteParameter("@Storage_Location"),
                                                new SQLiteParameter("@Quantity"),
                                                new SQLiteParameter("@Check_State"),
                                                new SQLiteParameter("@Check_Time")
                                               };
                parameters[0].Value = dt.Date;
                parameters[1].Value = time;
                parameters[2].Value = split_Bom[1];
                parameters[3].Value = split_Bom[2];
                parameters[4].Value = split_Bom[3];
                parameters[5].Value = split_Bom[4];
                parameters[6].Value = split_Bom[5];
                parameters[7].Value = split_Bom[6];
                parameters[8].Value = "";
                parameters[9].Value = "";

                db.ExecuteNonQuery(sql, parameters);
            }
        }


        public void ShowBomInfomationFromDataBase()
        {
            
            string connectionString = "Data Source=" + @"C:/Users/admin/Desktop/Call_Bom/Call_bom/Call_bom/bin/Debug/BomDB.sqlite" + ";Version=3;";

            string sql = "SELECT * FROM BomInformation where Check_State =''";


            SelectDataTable(sql, connectionString);
        }


        public void SelectDataTable(string sql, string connectionString)
        {

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {

                    using (SQLiteDataReader adapter = command.ExecuteReader())
                    {
                        while (adapter.Read())
                        {
                            dgvShowBom.Rows.Add(adapter["Id"],adapter["Call_Time"], adapter["Station"], adapter["Work_Station"], adapter["Bom_Number"], adapter["Bom_Describe"], adapter["Storage_Location"], adapter["Quantity"], false);
                        }
                    }
                    //DataTable data = new DataTable();
                    //adapter.Fill(data);

                    //return data;
                }

                connection.Close();
            }

        }


        public void UpdateDataTable(string sql, string connectionString)
        {


            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = sql;

                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }


        }

        /// <summary>
        /// 获取当前系统时间的方法
        /// </summary>
        /// <returns>当前时间</returns>
        private DateTime GetCurrentTime()
        {
            DateTime currentTime = new DateTime();
            currentTime = DateTime.Now;
            return currentTime;
        }

        //发送信息到客户端
        //private void btnSend_Click(object sender, EventArgs e)
        //{
        //    //调用 ServerSendMsg方法  发送信息到客户端
        //    ServerSendMsg(txtSendMsg.Text.Trim());
        //}


        /// <summary>
        /// 发送信息到客户端的方法
        /// </summary>
        /// <param name="sendMsg">发送的字符串信息</param>
        //private void ServerSendMsg(string sendMsg)
        //{
        //    //将输入的字符串转换成 机器可以识别的字节数组
        //    byte[] arrSendMsg = Encoding.UTF8.GetBytes(sendMsg);
        //    //向客户端发送字节数组信息
        //    socConnection.Send(arrSendMsg);
        //    //将发送的字符串信息附加到文本框txtMsg上
        //    txtMsg.AppendText("So-flash:" + GetCurrentTime() + "\r\n" + sendMsg + "\r\n");
        //}


        /// <summary>
        /// 发送信息到客户端的方法
        /// </summary>
        /// <param name="sendMsg">发送的字符串信息</param>
        private void ServerSendMsg(string sendMsg, Socket socConnection)
        {

            //将输入的字符串转换成 机器可以识别的字节数组
            byte[] arrSendMsg = Encoding.UTF8.GetBytes(sendMsg);
            //向客户端发送字节数组信息
            socConnection.Send(arrSendMsg);
        }

        /// <summary>
        /// 发送信息到客户端的方法
        /// </summary>
        /// <param name="sendMsg">发送的字符串信息</param>
        private void ServerSendMsgToAGV(string sendMsg, Socket socConnection)
        {

            //将输入的字符串转换成 机器可以识别的字节数组
            byte[] arrSendMsg = strToToHexByte(sendMsg);
            //向客户端发送字节数组信息
            socConnection.Send(arrSendMsg);
        }


        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        private static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 实时显示当前时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrCurrentTime_Tick(object sender, EventArgs e)
        {

            DateTime dt = DateTime.Now;
            lblTime.Text = dt.ToString("yyyy年MM月dd日       HH:mm:ss");
        }

        private void dgvShowBom_CellClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (lblLinkStatu.Text.Trim() == "连接状态：未连接")
            {
                MessageBox.Show("小车未连接");
            }
            int count = dgvShowBom.Rows.Count;
            for (int i = dgvShowBom.Rows.Count - 1; i >= 0; i--)
            {
                bool isChecked = (bool)dgvShowBom.Rows[i].Cells["dgvPicking"].Value;
                if (isChecked)
                {
                    stationSet.Add(dgvShowBom.Rows[i].Cells["dgvStation"].Value.ToString());

                    string connectionString = "Data Source=" + @"C:/Users/admin/Desktop/Call_Bom/Call_bom/Call_bom/bin/Debug/BomDB.sqlite" + ";Version=3;";
                    string sqlUpdate = string.Format("UPDATE BomInformation SET Check_State = 'true',Check_Time = '{0}' WHERE Id ='{1}';",dgvShowBom.Rows[i].Cells["dgvPickingTime"].Value.ToString(),dgvShowBom.Rows[i].Cells["ID"].Value.ToString());
                    UpdateDataTable(sqlUpdate,connectionString);
                    dgvShowBom.Rows.Remove(dgvShowBom.Rows[i]);

                }

            }
            string station_sum = null;

            foreach (var station in stationSet)
            {
                station_sum += station + ",";
            }
            if (station_sum != null)
            {
                station_sum = station_sum.Substring(0, station_sum.Length - 1);
                station_sum += " " + "号站点等待配送";
                lstCallBom.Items.Add(station_sum);
                stationSet.Clear();
            }

            // 清空叫料txt文件
            ClearTxtFile(writeBomFilePath, 0);

            //写入叫料txt文件
            WriteTxtFile(writeBomFilePath, 0);
        }


        # region  叫料、叫车 Txt 文件处理
        /// <summary>
        /// 清空叫料/叫车 txt文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="flag"></param>
        public void ClearTxtFile(string filePath, int flag)
        {
            if (flag == 0)
            {

                FileStream stream = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                stream.Seek(0, SeekOrigin.Begin);
                stream.SetLength(0);
                stream.Close();

            }
            else if (flag == 1)
            {

                FileStream stream = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                stream.Seek(0, SeekOrigin.Begin);
                stream.SetLength(0);
                stream.Close();

            }
        }

        /// <summary>
        /// 写入叫料/叫车 txt文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="flag"></param>
        public void WriteTxtFile(string filePath, int flag)
        {
            if (flag == 0)
            {
                int count = lstCallBom.Items.Count;
                StreamWriter swWriteFile = new StreamWriter(filePath, true);
                for (int i = 0; i < count; i++)
                {
                    swWriteFile.WriteLine(lstCallBom.Items[i].ToString());
                }
                swWriteFile.Close();
            }
            else if (flag == 1)
            {
                int count = lstCallCar.Items.Count;
                StreamWriter swWriteFile = new StreamWriter(filePath, true);
                for (int i = 0; i < count; i++)
                {
                    swWriteFile.WriteLine(lstCallCar.Items[i].ToString());
                }
                swWriteFile.Close();
            }
        }

        /// <summary>
        /// 读出 叫料/叫车 txt文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="flag"></param>
        public void ReadTxtFile(string filePath, int flag)
        {
            //从头到尾以流的方式读出文本文件
            //该方法会一行一行读出文本
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
            {
                if(flag == 0)
                {
                    string str;
                    while ((str = sr.ReadLine()) != null)
                    {
                        lstCallBom.Items.Add(str);
                    }
                }
                else if(flag == 1)
                {
                    string str;
                    while ((str = sr.ReadLine()) != null)
                    {
                        lstCallCar.Items.Add(str);
                    }
                }
            }
        }
        #endregion


        private void dgvShowBom_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 8 && e.RowIndex != -1)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)this.dgvShowBom.Rows[e.RowIndex].Cells["dgvPicking"];
                bool flat = Convert.ToBoolean(checkCell.Value);
                //dgvShowBom.EndEdit();
                //this.dgvShowBom.RefreshEdit();
                //this.dgvShowBom.NotifyCurrentCellDirty(true);
                if ((bool)checkCell.Value == true)
                {

                    this.dgvShowBom.Rows[e.RowIndex].Cells["dgvPickingTime"].Value = "";
                    checkCell.Value = false;
                }
                else
                {

                    DateTime dt = DateTime.Now;
                    string timeNow = dt.ToString("HH:mm:ss");
                    this.dgvShowBom.Rows[e.RowIndex].Cells["dgvPickingTime"].Value = timeNow;
                    checkCell.Value = true;

                }

                //dgvShowBom.EndEdit();

            }
        }

        /// <summary>
        /// 正在配送时，文字变成红色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstCallBom_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                e.DrawBackground();
                Brush mybsh = Brushes.Black;
                // 判断是什么类型的标签  
                if (lstCallBom.Items[e.Index].ToString().IndexOf("正在配送") != -1)
                {
                    mybsh = Brushes.Red;
                }
                else if (lstCallBom.Items[e.Index].ToString().IndexOf("你坏") != -1)
                {
                    mybsh = Brushes.Red;
                }
                // 焦点框  
                e.DrawFocusRectangle();

                //居中对齐
                StringFormat strFmt = new System.Drawing.StringFormat();
                strFmt.Alignment = StringAlignment.Center; //文本垂直居中
                strFmt.LineAlignment = StringAlignment.Center; //文本水平居中

                //文本   
                e.Graphics.DrawString(lstCallBom.Items[e.Index].ToString(), e.Font, mybsh, e.Bounds, strFmt);
            }
        }

        private void lstCallCar_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                e.DrawBackground();
                Brush mybsh = Brushes.Black;
                // 判断是什么类型的标签  
                if (lstCallCar.Items[e.Index].ToString().IndexOf("正在前往") != -1)
                {
                    mybsh = Brushes.Red;
                }
                else if (lstCallCar.Items[e.Index].ToString().IndexOf("你坏") != -1)
                {
                    mybsh = Brushes.Red;
                }
                // 焦点框  
                e.DrawFocusRectangle();

                //居中对齐
                StringFormat strFmt = new System.Drawing.StringFormat();
                strFmt.Alignment = StringAlignment.Center; //文本垂直居中
                strFmt.LineAlignment = StringAlignment.Center; //文本水平居中

                //文本   
                e.Graphics.DrawString(lstCallCar.Items[e.Index].ToString(), e.Font, mybsh, e.Bounds, strFmt);
            }
        }



        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportDate ed = new ExportDate();
            ed.ShowDialog();
        }

        private void rightMouseDelete_Click_1(object sender, EventArgs e)
        {
            ListBox listbox = cmsDelete.SourceControl as ListBox;//获取contextMenuStrip的关联控件
            if (listbox.Name == "lstCallBom")
            {
                int i = listbox.SelectedIndex;
                if(i == -1)
                {
                    return;
                }
                listbox.Items.Remove(listbox.Items[i]);
                ClearTxtFile(writeBomFilePath, 0);
                WriteTxtFile(writeBomFilePath, 0);
            }
            else if (listbox.Name == "lstCallCar")
            {
                int i = listbox.SelectedIndex;
                if(i == -1)
                {
                    return;
                }
                listbox.Items.Remove(listbox.Items[i]);
                ClearTxtFile(writeCarFilePath, 1);
                WriteTxtFile(writeCarFilePath, 1);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("确认要关闭系统吗？", "退出系统", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                  
                System.Environment.Exit(0);

            }
            else
            {
                e.Cancel = true;
            }
        }















    }
}
