using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using log4net;

namespace cs.fa.linecollector
{
    
    
    public class AppLogs
    {
        
        
        public static ILog getLog(string logname)
        {
            var file = new FileInfo(AppSettings.ConfigPath + @"Log4net.xml");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(file);

            var log = log4net.LogManager.GetLogger("HostInfo");
            return log;
        }
    }
}
