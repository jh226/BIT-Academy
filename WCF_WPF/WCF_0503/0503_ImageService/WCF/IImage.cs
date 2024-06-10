using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace _0503_ImageService
{
    [ServiceContract]
    internal interface IImage
    {
        [OperationContract]
        bool SaveImage(string filename, byte[] data, string id, bool shared);

        [OperationContract]
        ImageData GetImage(string filename);

        [OperationContract]
        List<ImageData> GetImageList();

        [OperationContract]
        bool DeleteImage(string filename);
    }
}
