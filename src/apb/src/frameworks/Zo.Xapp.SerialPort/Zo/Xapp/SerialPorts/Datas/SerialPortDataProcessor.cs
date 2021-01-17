using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zo.Xapp.SerialPorts.Datas
{
    /// <summary>
    /// 串口数据处理器，主要职责是：
    /// 1.发送数据；
    /// 2.接收数据
    /// </summary>
    public class SerialPortDataProcessor : ISerialPortDataProcessor
    {
        #region 字段和属性

        private readonly object _objReadOrWriteLock = new object();   //读、写寄存器锁

        public SerialPort SerialPort { get; set; }
 
        #endregion

        #region 构造函数

        public SerialPortDataProcessor(SerialPort serialPort)
        {
            this.SerialPort = serialPort;
            this.SerialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
        }

        #endregion

        #region 串口

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                byte[] recevideData = new byte[this.SerialPort.BytesToRead];
                this.SerialPort.Read(recevideData, 0, recevideData.Length);
                this.ReceiveData = recevideData.ToList();
                RaiseReceiveDataChangedEvent(this.ReceiveData);
            }
            catch (Exception)
            {
                //this.ReceiveData = null;
            }
        }

        #region 数据事件

        private void RaiseRequstDataChangedEvent(List<List<byte>> requesData)
        {
            if (null != requesData)
            {
                if (null != OnRequestDataChanged)
                {
                    RequstDataEventArgs requstDataEventArgs = new RequstDataEventArgs(requesData);
                    foreach (EventHandler<RequstDataEventArgs> hanlder in OnRequestDataChanged.GetInvocationList())
                    {
                        hanlder(this, requstDataEventArgs);
                    }
                }
            }

        }

        private void RaiseReceiveDataChangedEvent(List<byte> receiveData)
        {
            if (null != receiveData)
            {
                if (null != OnRequestDataChanged)
                {
                    ReceiveDataEventArgs requstDataEventArgs = new ReceiveDataEventArgs(receiveData);
                    foreach (EventHandler<ReceiveDataEventArgs> hanlder in OnReceiveDataChanged.GetInvocationList())
                    {
                        hanlder(this, requstDataEventArgs);
                    }
                }
            }
        }

        #endregion

        public bool OpenSerialPort()
        {
            bool succeed = true;

            try
            {
                if (!this.SerialPort.IsOpen)
                {
                    this.SerialPort.Open();
                }
            }
            catch (Exception)
            {
                succeed = false;
            }

            return succeed;
        }

        public bool CloseSerialPort()
        {
            bool succeed = true;

            try
            {
                if (!this.SerialPort.IsOpen)
                {
                    this.SerialPort.Close();
                }
            }
            catch (Exception)
            {
                succeed = false;
            }

            return succeed;
        }

        private void WriteSerialPort(byte[] toWriteData)
        {
            lock (_objReadOrWriteLock)
            {
                this.SerialPort.Write(toWriteData, 0, toWriteData.Length);
            }
        }

        #endregion

        #region  ISerialPortDataProcessor 成员

        public List<byte> RequestData { get; set; } = new List<byte>();
        public List<byte> ReceiveData { get; set; } = new List<byte>();

        public delegate void RequstDataChangedEventHandler(object obj, RequstDataEventArgs requstData);
        public event EventHandler<RequstDataEventArgs> OnRequestDataChanged = delegate { };

        public delegate void ReceiveDataEventChangedHandler(object obj, ReceiveDataEventArgs receiveData);
        public event EventHandler<ReceiveDataEventArgs> OnReceiveDataChanged = delegate { };


        public bool SendDatas(byte[] data)
        {
            return SendDatas(data.ToList());
        }

        public bool SendDatas(List<byte> data)
        {
            return SendDatas(new List<List<byte>> { data });
        }

        public bool SendDatas(List<byte[]> datas)
        {
            List<List<byte>> sendDatas = new List<List<byte>>();
            foreach (var data in datas)
            {
                sendDatas.Add(data.ToList());
            }

            return SendDatas(sendDatas);
        }

        public bool SendDatas(List<List<byte>> datas)
        {
            bool succeed = true;

            if (datas == null || !datas.Any())
            {
                return true;
            }

            try
            {
                foreach (var data in datas)
                {
                    WriteSerialPort(data.ToArray());
                }

                RaiseRequstDataChangedEvent(datas);
            }
            catch (Exception)
            {
                succeed = false;
            }
            return succeed;
        }


        #endregion

    }
}
