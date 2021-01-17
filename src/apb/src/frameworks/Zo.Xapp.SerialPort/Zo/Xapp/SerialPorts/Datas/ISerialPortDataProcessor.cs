using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zo.Xapp.SerialPorts.Datas;

namespace Zo.Xapp.SerialPorts.Datas
{
    public interface ISerialPortDataProcessor
    {
        /// <summary>
        /// 串口对象
        /// </summary>
        public SerialPort SerialPort { get; set; }

        /// <summary>
        /// 请求帧
        /// </summary>
        List<byte> RequestData { get; set; }

        /// <summary>
        /// 返回帧
        /// </summary>
        List<byte> ReceiveData { get; set; }

        /// <summary>
        /// 请求帧发生改变事件
        /// </summary>
        event EventHandler<RequstDataEventArgs> OnRequestDataChanged;

        /// <summary>
        /// 返回帧发生改变事件
        /// </summary>
        event EventHandler<ReceiveDataEventArgs> OnReceiveDataChanged;

        /// <summary>
        /// 发送数据
        /// </summary>
        bool SendDatas(byte[] data);
        bool SendDatas(List<byte> data);
        bool SendDatas(List<byte[]> datas);
        bool SendDatas(List<List<byte>> datas);

    }
}
