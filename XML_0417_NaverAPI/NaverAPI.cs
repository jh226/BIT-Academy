using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _0417_XML
{
    internal class NaverAPI
    {
        public static string Example(string str)
        {
            string text = String.Empty;

            string query = str; // 검색할 문자열
            string url = "https://openapi.naver.com/v1/search/encyc?query=" + query; // JSON 결과
            //string url = "https://openapi.naver.com/v1/search/blog.xml?query=" + query;  // XML 결과

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-Naver-Client-Id", "???"); // 클라이언트 아이디
            request.Headers.Add("X-Naver-Client-Secret", "???");       // 클라이언트 시크릿
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string status = response.StatusCode.ToString();
            if (status == "OK")
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                text = reader.ReadToEnd();
                Console.WriteLine(text);
            }
            else
            {
                Console.WriteLine("Error 발생=" + status);
            }
            
            return text;
        }
    }
}
