using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
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
using Zo.Xapp.SerialPorts.Datas;
using Zo.Xapp.Text;
using Zo.Xapp.Wpf.Common;

namespace Zo.Xapp.Wpf.Views
{
    /// <summary>
    /// SeralPortDebugView.xaml 的交互逻辑
    /// </summary>
    public partial class SerialPortDebugView : UserControl
    {
        #region 成员变量

        private SerialPortDataProcessor serialPortDataProcessor;
        private SerialPort serialPort;
        private bool serialPortConnecting = false;

        private DataShowType dataShowType = DataShowType.Hex;

        #endregion

        #region 构造函数

        public SerialPortDebugView()
        {
            InitializeComponent();
            InitializePortCombox();
            InitializeBaudRateCombox();
            InitializeParityCombox();
            InitializeDataBitsCombox();
            InitializeStopBitsCombox();

            InitializeMenuTop();
        }

        #endregion

        #region 串口参数初始化

        /// <summary>
        /// 初始化串口
        /// </summary>
        private void InitializePortCombox()
        {
            List<string> ports = new List<string>();
            for (int i = 1; i < 41; i++)
            {
                ports.Add("COM" + Convert.ToString(i));
            }
            this.cbPorts.ItemsSource = ports;
            this.cbPorts.SelectedIndex = 0;
        }

        /// <summary>
        /// 初始化化波特率
        /// </summary>
        private void InitializeBaudRateCombox()
        {
            List<int> baudRates = new List<int>()
            {
               300, 600, 1200, 2400, 4800, 9600,
               19200, 38400, 43000, 56000, 57600,115200
            };

            this.cbBaudRate.ItemsSource = baudRates;
            this.cbBaudRate.SelectedIndex = 5;
        }

        /// <summary>
        /// 初始化校验位
        /// </summary>
        private void InitializeParityCombox()
        {
            string[] paritiesString = Enum.GetNames(typeof(Parity));
            this.cbParity.ItemsSource = paritiesString;
            this.cbParity.SelectedIndex = 1;
        }

        /// <summary>
        /// 初始化数据位
        /// </summary>
        private void InitializeDataBitsCombox()
        {
            List<int> dataBits = new List<int>()
            {
                8, 7 , 6
            };

            this.cbDataBits.ItemsSource = dataBits;
            this.cbDataBits.SelectedIndex = 0;
        }

        /// <summary>
        /// 初始化停止位
        /// </summary>
        private void InitializeStopBitsCombox()
        {
            List<string> stopBits = new List<string>()
            {
                "1", "2"
            };

            this.cbStopBit.ItemsSource = stopBits;
            this.cbStopBit.SelectedIndex = 0;
        }

        #endregion

        #region 顶部菜单

        private void InitializeMenuTop()
        {
            MenuItem dataShowTypeMenuItem = new MenuItem();
            dataShowTypeMenuItem.Header = "显示模式";

            string[] dataShowTypes = Enum.GetNames(typeof(DataShowType));
            foreach (var item in dataShowTypes)
            {
                DataShowType dataShowType = (DataShowType)Enum.Parse(typeof(DataShowType), item);

                MenuItem menuSubItem = new MenuItem() { Header = item, Tag = dataShowType };
                menuSubItem.Click += new RoutedEventHandler(MenuSubItem_Click);

                dataShowTypeMenuItem.Items.Add(menuSubItem);

            }

            this.menuTop.Items.Add(dataShowTypeMenuItem);

        }

        private void MenuSubItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem selectedFunNum = sender as MenuItem;
            this.dataShowType = (DataShowType)selectedFunNum.Tag;
        }

        //private void DisplayFunctionNumView(FunctionNumType functionNumType)
        //{
        //    this.stLeftDown.Children.Clear();
        //    currentFunctionNumView = FunctionNumViewManager.GetFunctionNumView(functionNumType);
        //    if (null != currentFunctionNumView)
        //    {
        //        this.stLeftDown.Children.Add((UserControl)currentFunctionNumView);
        //    }
        //}

        #endregion

        #region 设置串口连接状态对应的界面状态

        private void SetConnectionStatus()
        {
            if (this.serialPortConnecting)
            {
                this.imgConnectionStatus.Source = new BitmapImage(
                    new Uri("pack://application:,,,/Zo.Xapp.Wpf.Ui;component/Resources/Images/connect.png"));
                this.imgConnectionStatus.ToolTip = "串口" + this.serialPort.PortName + "已经打开";

                this.btOpenSerialPort.Content = "关闭串口";
                this.cbPorts.IsEnabled = false;
                this.cbBaudRate.IsEnabled = false;
                this.cbParity.IsEnabled = false;
                this.cbDataBits.IsEnabled = false;
                this.cbStopBit.IsEnabled = false;
            }
            else
            {
                this.imgConnectionStatus.Source = new BitmapImage(
                    new Uri("pack://application:,,,/Zo.Xapp.Wpf.Ui;component/Resources/Images/unconnect.png"));
                this.imgConnectionStatus.ToolTip = "串口" + this.serialPort.PortName + "已经关闭";

                this.btOpenSerialPort.Content = "打开串口";
                this.cbPorts.IsEnabled = true;
                this.cbBaudRate.IsEnabled = true;
                this.cbParity.IsEnabled = true;
                this.cbDataBits.IsEnabled = true;
                this.cbStopBit.IsEnabled = true;
            }
        }

        #endregion

        #region 开、关串口

        private void BtnOpenSerialPort_Click(object sender, RoutedEventArgs e)
        {
            if (!this.serialPortConnecting)
            {
                OpenSerialPort();
            }
            else
            {
                CloseSerialPort();
            }

        }

        private void OpenSerialPort()
        {
            Initialize();

            try
            {
                if (!this.serialPort.IsOpen)
                {
                    this.serialPortConnecting = true;
                    SetConnectionStatus();
                    this.serialPort.Open();
                }
                else
                {
                    MessageBox.Show("串口" + this.serialPort.PortName + "已经打开");
                }
            }
            catch (Exception)
            {
                this.serialPortConnecting = false;
                SetConnectionStatus();
                MessageBox.Show("串口" + this.serialPort.PortName + "打开失败.\n该串口可能不存在或已经被占用");
            }
        }

        private void Initialize()
        {
            this.serialPort = new SerialPort();
            this.serialPort.PortName = this.cbPorts.Text as string;//this.serialPort.PortName = this.cbPorts.SelectedItem as string;
            this.serialPort.BaudRate = (int)this.cbBaudRate.SelectedItem;
            this.serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), (string)this.cbParity.SelectedItem, false);
            this.serialPort.DataBits = (int)this.cbDataBits.SelectedItem;
            this.serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), (string)this.cbStopBit.SelectedItem);

            this.serialPortDataProcessor = new SerialPortDataProcessor(serialPort);
            this.serialPortDataProcessor.OnRequestDataChanged += new EventHandler<RequstDataEventArgs>(SerialPort_OnRequestDataChanged);
            this.serialPortDataProcessor.OnReceiveDataChanged += new EventHandler<ReceiveDataEventArgs>(SerialPort_OnReceiveDataChanged);

        }

        private void CloseSerialPort()
        {
            try
            {
                if (this.serialPort.IsOpen)
                {
                    this.serialPortConnecting = false;
                    SetConnectionStatus();
                    this.serialPort.Close();
                }
                else
                {
                    MessageBox.Show("串口" + this.serialPort.PortName + "已经关闭");
                }
            }
            catch (Exception)
            {
                this.serialPortConnecting = true;
                SetConnectionStatus();
                MessageBox.Show("串口" + this.serialPort.PortName + "关闭失败");
            }
        }

        #endregion

        #region 发送请求帧

        private void BtnSendRequestion_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckSerialPortCanUse())
            {
                MessageBox.Show("串口还没打开，请先打开串口");
                return;
            }

            var sendDatas = tbSendData.Text;
            if (String.IsNullOrWhiteSpace(sendDatas))
            {
                MessageBox.Show("请输入要发送数据");
                return;
            }
            else
            {
                this.serialPortDataProcessor.SendDatas(sendDatas.GetBytes());
            }

        }

        /// <summary>
        /// 检查串口的端口是否可用
        /// </summary>
        /// <returns></returns>
        private bool CheckSerialPortCanUse()
        {
            bool canUse = true;

            if (this.serialPort == null)
            {
                return false;
            }
            if (this.serialPort != null)
            {
                if (!this.serialPort.IsOpen)
                {
                    return false;
                }
            }

            return canUse;
        }

        #endregion

        #region 接收请求帧

        private void SerialPort_OnRequestDataChanged(object sender, RequstDataEventArgs e)
        {
            List<List<byte>> requstData = e.RequstData;
            string recevideDataHexString = null;

            foreach (List<byte> item in requstData)
            {
                if (this.dataShowType == DataShowType.Hex)
                {
                    recevideDataHexString += StringHelper.ConverByteArrayToHexString(item.ToArray());
                }
                else if (this.dataShowType == DataShowType.Text)
                {
                    recevideDataHexString += StringHelper.ConverByteArrayToASCIIString(item.ToArray());
                }
                else
                {
                    recevideDataHexString += StringHelper.ConverByteArrayToHexString(item.ToArray());
                }
            }

            this.Dispatcher.Invoke(new Action(() =>
            {
                this.tbRequestData.Text += recevideDataHexString + "\n";
            }));
        }

        #endregion

        #region 接收响应帧

        private void SerialPort_OnReceiveDataChanged(object sender, ReceiveDataEventArgs e)
        {
            byte[] recevideData = e.ReceiveData.ToArray();
            string recevideDataHexString = StringHelper.ConverByteArrayToHexString(recevideData);

            this.Dispatcher.Invoke(new Action(() =>
            {
                this.tbrecevideData.Text += recevideDataHexString + "\n";
            }));
        }

        #endregion

        #region 清空显示区

        private void BtnClearRequestData_Click(object sender, RoutedEventArgs e)
        {
            this.tbRequestData.Text = null;
        }

        private void BtnClearRecevideData_Click(object sender, RoutedEventArgs e)
        {
            this.tbrecevideData.Text = null;
        }

        #endregion

    }
}
