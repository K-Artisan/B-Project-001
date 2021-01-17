﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zo.Xapp.SerialPorts.Datas
{
    public class ReceiveDataEventArgs : EventArgs
    {
        public List<byte> ReceiveData { get; set; }

        public ReceiveDataEventArgs(List<byte> receiveData)
        {
            this.ReceiveData = receiveData;
        }

    }
}
