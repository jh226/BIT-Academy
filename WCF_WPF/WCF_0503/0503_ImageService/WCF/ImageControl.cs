using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0503_ImageService
{
    internal class ImageControl : IImage
    {
        public bool DeleteImage(string filename)
        {
            throw new NotImplementedException();
        }

        public ImageData GetImage(string filename)
        {
            WbDB db = WbDB.Instance;

            try
            {
                db.Open();
                return db.GetImage(filename);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public List<ImageData> GetImageList()
        {
            WbDB db = WbDB.Instance;

            try
            {
                db.Open();
                return db.GetImageList();
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public bool SaveImage(string filename, byte[] data, string id, bool shared)
        {
            WbDB db = WbDB.Instance;

            try
            {                
                db.Open();
                return db.SaveImage(filename, data, id, shared);                
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                db.Close();
            }
        }
    }
}
