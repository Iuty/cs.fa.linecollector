using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace cs.fa.linecollector.ViewModel
{
    public class MainWindowVM:VMBase
    {

        public void setStatus(int status)
        {
            setBackGround(status);
            enabled = true;
            if (status == 2)
            {
                enabled = false;
            }
            RaisePropertyChanged("EnabledStart");
            RaisePropertyChanged("EnabledStop");
        }
        
        private void setBackGround(int status)
        {
            if (status == 0)
            {
                background = Brushes.Gray;
            }
            else if (status == 1)
            {
                background = Brushes.Orange;
            }
            else if (status == 2)
            {
                background = Brushes.Green;

            }
            RaisePropertyChanged("BackGround");
        }

        private bool enabled = true;
        public bool EnabledStart
        {
            get
            {
                return enabled;
            }
        }

        public bool EnabledStop
        {
            get
            {
                return !enabled;
            }
        }

        private Brush background = Brushes.Gray;
        public Brush BackGround
        {
            get
            {
                return background;
            }
        }

        private string info = "启动完毕";
        public string InfoText
        {
            get
            {
                return DateTime.Now.ToString("[HH:MM:ss]  ") + info;
            }
        }

        public void setInfoText(string info)
        {
            this.info = info;
            RaisePropertyChanged("InfoText");
        }

    }
}
