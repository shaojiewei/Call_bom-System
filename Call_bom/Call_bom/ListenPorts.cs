using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Call_bom
{
   public class ListenPorts
    {
       Socket[] scon;
       IPEndPoint[] ipPoints;
       internal ListenPorts(IPEndPoint[] ipPoints)
       {
           this.ipPoints = ipPoints;
           scon = new Socket[ipPoints.Length];

       }

       public void beginListen()
       {
           for(int i = 0; i < ipPoints.Length;i++)
           {
               scon[i] = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
               scon[i].Bind(ipPoints[i]);
               scon[i].Listen(100);

               Thread thread = new Thread(threadListen);
               thread.IsBackground = true;
               thread.Start(scon[i]);
               
           }
       }

       public void threadListen(object objs)
       {
           //创建一个
           Socket scon = objs as Socket;
           byte[] data = new byte[1024];

           while(true)
           {
               Socket newSocket = scon.Accept();
               //创建一个通信线程 
              // ParameterizedThreadStart pts = new ParameterizedThreadStart(ServerRecMsg);
              // Thread thr = new Thread(pts);
           }

              
       }
    }
}
