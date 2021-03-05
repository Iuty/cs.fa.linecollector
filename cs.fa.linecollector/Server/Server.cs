using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLCDriver;
using log4net;

namespace cs.fa.linecollector
{
    public class Server
    {
        private static object inslock = new object();
        private ILog log;
        private Server()
        {
            log = AppLogs.getLog("HostInfo");

        }

        private static Server instance;
        public static Server Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Server();

                }
                return instance;
            }
        }

        public delegate void deleStatusChanged(int status);
        public event deleStatusChanged OnStatusChanged;

        private void setStatus(int status)
        {
            this.status = status;
            
            if (OnStatusChanged != null)
            {
                OnStatusChanged(status);
            }
        }

        private int status = 0;
        public int Status
        {
            get
            {
                return status;
            }
        }

        public void start()
        {
            //
            setStatus(2);
            sendMessage("启动服务成功！");
        }

        public void stop()
        {
            //
            setStatus(1);
            sendMessage("停止服务成功！");
        }

        private void loadConfig()
        {
            
        }

        public delegate void deleSendInfo(string info);
        public event deleSendInfo OnSendInfo;

        private void sendMessage(string msg)
        {
            if (OnSendInfo != null)
            {
                OnSendInfo(msg);
            }
        }

        
    }
}
