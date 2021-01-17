using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zo.Xapp.SerialPorts.Datas
{
    public class RequstDataEventArgs : EventArgs
    {
        public List<List<byte>> RequstData { get; set; }

        public RequstDataEventArgs(List<List<byte>> RequstData)
        {
            this.RequstData = RequstData;
        }

    }
}
