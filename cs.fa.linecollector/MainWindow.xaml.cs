using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Forms = System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using cs.fa.linecollector.ViewModel;

using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using System.Reflection;

using log4net;

namespace cs.fa.linecollector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool pressalt = false;
        private System.Windows.Forms.NotifyIcon notifyIcon;

        private Server server;
        private ILog log;

        public MainWindow()
        {
            this.DataContext = this;
            log = AppLogs.getLog("HostInfo");
            InitializeComponent();
            
            initNotifyIcon();
            initServer();

            this.Visibility = Visibility.Hidden;
            Messenger.Default.Register<string>(this, "MinSizeWindow", MinSizeWindow);
            Messenger.Default.Register<string>(this, "ExitWindow", QuitWindow);
            Messenger.Default.Register<string>(this, "ReturnMessageBox", ShowReturnMessageBox);
        }

        public MainWindowVM ViewModel
        {
            get
            {
                return this.DataContext as MainWindowVM;
            }
        }

        private void setStatus(int status)
        {

            ViewModel.setStatus(status);
            
        }

        
        private void MinSizeWindow(string min)
        {
            //this.WindowState = WindowState.Minimized;
            this.Visibility = Visibility.Hidden;
        }

        private void showWindow()
        {
            //this.WindowState = WindowState.Normal;
            this.Visibility = Visibility.Visible;
        }

        public void mouseShowWindow(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            showWindow();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (server.Status == 2)
            {
                MessageBox.Show("不能关闭运行中的服务,请停止后再试.");
                e.Cancel = true;

            }

        } 

        private void QuitWindow(string quit)
        {
            /*
            var LineControlVM = (Application.Current.Resources["Locator"] as ViewModelLocator).LineControlVM;
            if (LineControlVM != null && LineControlVM.UserLevel < 1)
            {
                return;
            }
             * */
            Application.Current.Shutdown();
        }

        private void initNotifyIcon()
        {
            this.notifyIcon = new System.Windows.Forms.NotifyIcon();
            this.notifyIcon.BalloonTipText = "LC 服务正在运行..."; //设置程序启动时显示的文本
            this.notifyIcon.Text = "LC 服务";//最小化到托盘时，鼠标点击时显示的文本
            this.notifyIcon.Icon = new System.Drawing.Icon(
                System.Windows.Application.GetResourceStream(new Uri("/cs.fa.linecollector;component/favicon.ico", UriKind.Relative)).Stream
                );
            this.notifyIcon.Visible = true;

            ////右键菜单--打开菜单项
            //System.Windows.Forms.MenuItem open = new System.Windows.Forms.MenuItem("打开");
            //open.Click += new EventHandler(ShowWindow);
            ////右键菜单--退出菜单项
            //System.Windows.Forms.MenuItem exit = new System.Windows.Forms.MenuItem("Exit");
            //exit.Click += new EventHandler(CloseWindow);
            //关联托盘控件
            //System.Windows.Forms.MenuItem[] childen = new System.Windows.Forms.MenuItem[] { open, exit };
            //notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(childen);

            notifyIcon.MouseDoubleClick += mouseShowWindow;
            this.notifyIcon.ShowBalloonTip(1000);


        }

        private void ShowReturnMessageBox(string msg)
        {
            //MessageBox.Show(msg,"Return Message:");
            //LineController.View.MessageBox mb = new LineController.View.MessageBox();
            //mb.Title.Text = "EIS Return Message";
            //mb.Info.Text = msg;
            //mb.Show();
        }

        private void OnPreKeyDown(object sender, KeyEventArgs e)
        {
            //屏掉alt+f4
            if ((e.SystemKey == Key.LeftAlt) || (e.SystemKey == Key.RightAlt))
            {
                pressalt = true;
            }

            else if (e.SystemKey == Key.F4 && pressalt)
            {
                e.Handled = true;
            }


        }

        

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (((e.SystemKey == Key.LeftAlt) || (e.SystemKey == Key.RightAlt)))
            {
                pressalt = false;
            }
        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            //TaskManage.Instance.stop();
        }

        private void initServer()
        {
            try
            {
                server = Server.Instance;
                server.OnStatusChanged += setStatus;
                server.OnSendInfo += ViewModel.setInfoText;
                
            }
            catch(Exception ex)
            {
                log.ErrorFormat(ex.Message);
            }
            log.Info("初始化服务完成...");

        }

        private void start()
        {
            
            server.start();
            
        }

        private void stop()
        {
            server.stop();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "d:\\LineCollector\\Config\\");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            start();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            stop();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            MinSizeWindow("");
        }

        
    }
}
