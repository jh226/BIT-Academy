using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace _0503_ImageService
{
    [DataContract]
    public class ImageData
    {
        [DataMember(Order = 1)]
        public string FileName;

        [DataMember(Order = 2)]
        byte[] Data;

        [DataMember(Order = 3)]
        string UserId;

        [DataMember(Order = 4)]
        DateTime Day;

        public ImageData() { }
        public ImageData(string filename, byte[] data, string userid, DateTime day)
        {
            FileName = filename;
            Data = data;
            UserId = userid;
            Day = day;
        }
    }
}
