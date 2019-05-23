using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyNetMVC.BLL.Utils
{
    public static class StringHelper
    {
        /// <summary>
        /// 生成8位随机字符串
        /// </summary>
        /// <returns></returns>
        public static string getRandomString()
        {
            byte[] r = new byte[8];
            int ran;
            Random rand = new Random((int)(DateTime.Now.Ticks % 1000000));
            //生成8字节原始数据
            for (int i = 0; i < 8; i++)
                //while循环剔除非字母和数字的随机数
                do
                {
                    //数字范围是ASCII码中字母数字和一些符号
                    ran = rand.Next(48, 122);
                    r[i] = Convert.ToByte(ran);
                } while ((ran >= 58 && ran <= 64) || (ran >= 91 && ran <= 96));
            //转换成8位String类型               
            string randomID = Encoding.ASCII.GetString(r);
            return randomID;
        }
    }
}
