using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zo.Xapp.Text
{
    public class StringHelper
    {
        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ConverByteArrayToHexString(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }

        /// <summary>
        /// 字节数组转ASCII字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ConverByteArrayToASCIIString(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                returnStr = Encoding.ASCII.GetString(bytes);
            }
            return returnStr;
        }

        /// <summary>
        /// 将字符串转换成ASCII Byte数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[]  ConverASCIIStringToByteArray(string str)
        {
            if (!String.IsNullOrEmpty(str))
            {
                return Encoding.ASCII.GetBytes(str); ;
            }
            return null;
        }
    }
}
