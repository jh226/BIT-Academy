using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _0417_XML
{
    internal static class WbXml
    {
        #region 첫번째 문서(XmlWriter)
        //XML 문서 Write
        public static void WriteTest1()
        {
            XmlWriterSettings xsettings = new XmlWriterSettings();
            xsettings.Indent = true;

            XmlWriter xwriter = XmlWriter.Create("data0.xml", xsettings);

            WriteTest(xwriter);

            xwriter.Close();
        }

        //StringBuilder 에 Write
        public static string WriteTest2()
        {
            XmlWriterSettings xsettings = new XmlWriterSettings();
            xsettings.Indent = true;

            StringBuilder sb = new StringBuilder();
            XmlWriter xwriter = XmlWriter.Create(sb, xsettings); //********

            WriteTest(xwriter);     //********************

            xwriter.Close();

            return sb.ToString();
        }

        //Write1 코드 복사
        public static void WriteTest(XmlWriter xwriter)
        {
            xwriter.WriteComment("XmlWriter 개체 만들기 실습 예제");

            xwriter.WriteStartElement("books");

            xwriter.WriteStartElement("book");
            xwriter.WriteValue("ADO.NET");
            xwriter.WriteEndElement();

            xwriter.WriteStartElement("book");
            xwriter.WriteValue("XML.NET");
            xwriter.WriteEndElement();

            xwriter.WriteEndElement();      //books
        }
        #endregion

        #region 두번째 문서 및 특성(XmlWriter)
        //정방향 쓰기(WriteElementString)
        public static void Write2()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create("data1.xml", settings);

            writer.WriteComment("XmlWriter 개체로 요소 쓰기");

            writer.WriteStartElement("books"); //루트 요소 쓰기

            //*************  book ***************************************
            writer.WriteStartElement("book");//book 요소 쓰기

            writer.WriteStartElement("title");//title 요소 쓰기
            writer.WriteName("XML.NET");            //???????????????
            writer.WriteEndElement();//title 요소 닫기

            writer.WriteStartElement("가격");//가격 요소 쓰기
            writer.WriteValue(12000);
            writer.WriteEndElement();//가격 요소 닫기

            writer.WriteEndElement();//book 요소 닫기
            //*****************************************************

            //*************  book ***************************************
            writer.WriteStartElement("book");//book 요소 쓰기

            writer.WriteElementString("title", "ADO.NET");//title 요소와 값 쓰기

            writer.WriteStartElement("가격");//가격 요소 쓰기
            writer.WriteValue(15000);
            writer.WriteEndElement();//가격 요소 닫기

            writer.WriteEndElement();//book 요소 닫기
            //*****************************************************

            writer.WriteEndElement();//루트 요소 닫기

            writer.Close();


            XmlReader xreader = XmlReader.Create("data1.xml"); //XmlReader 개체 생성
            XmlWriter xwriter = XmlWriter.Create(Console.Out); //콘솔 출력 스트림으로 XmlWriter 개체 생성
            xwriter.WriteNode(xreader, false); //xreader 개체로 읽어온 데이터를 xwriter 개체에 복사
            xwriter.Close();
            xreader.Close();
        }


        //특성사용
        public static string Write3()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create("data.xml", settings);

            writer.WriteComment("XmlWriter 개체로 특성 쓰기");

            writer.WriteStartElement("books"); //루트 요소 쓰기

            //******************* book ********************************
            writer.WriteStartElement("book");//book 요소 쓰기

            writer.WriteStartAttribute("title"); //title 특성 쓰기
            writer.WriteString("XML.NET"); //title 특성 값 쓰기
            writer.WriteEndAttribute(); //title 특성 닫기

            writer.WriteStartAttribute("가격");//가격 특성 쓰기
            writer.WriteValue(12000); //가격 특성 값 쓰기
            writer.WriteEndAttribute(); //가격 특성 닫기

            writer.WriteEndElement(); //book 요소 닫기
            //************************************************************

            //******************* book ********************************
            writer.WriteStartElement("book");//book 요소 쓰기

            writer.WriteAttributeString("title", "ADO.NET");//title 특성과 값 쓰기

            writer.WriteStartAttribute("가격");//가격 특성 쓰기
            writer.WriteValue(15000);//가격 특성 값 쓰기
            writer.WriteEndAttribute();//가격 특성 닫기

            writer.WriteEndElement();//book 요소 닫기
            //************************************************************

            writer.WriteEndElement();//루트 요소 닫기
            writer.Close();

            /*
                <?xml version="1.0" encoding="utf-8"?>
                <!--XmlWriter 개체로 특성 쓰기-->
                <books>
                  <book title="XML.NET" 가격="12000" />
                  <book title="ADO.NET" 가격="15000" />
                </books>
             */
            XmlReader xreader = XmlReader.Create("data.xml"); //XmlReader 개체 생성
            StringBuilder sb = new StringBuilder();
            XmlWriter xwriter = XmlWriter.Create(sb, settings); //XmlWriter 개체 생성

            while (xreader.Read())
            {
                if (xreader.NodeType == XmlNodeType.Element)
                {
                    xwriter.WriteStartElement(xreader.Name);
                    xwriter.WriteAttributes(xreader, false); //xreader의 현재 특성을 쓰기
                    if (xreader.IsEmptyElement)
                    {
                        xwriter.WriteEndElement();
                    }
                }
                else if (xreader.NodeType == XmlNodeType.EndElement)
                {
                    xwriter.WriteEndElement();
                }
            }
            xwriter.Close();
            xreader.Close();

            return sb.ToString();
        }
        #endregion

        #region 읽기(XmlReader)
        
        //1. 파일스트림 객체를 이용
        public static string Read1(string filename)
        {
            FileStream fs = new FileStream(filename,
                            FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
            XmlReader reader1 = XmlReader.Create(fs); //*************
            string str = XmlPrint(reader1);  //*출력요청*//
            reader1.Close();
            fs.Close();
            return str;
        }

        //2. 파일스트림 객체 + XmlReaderSettings 객체 이용
        public static string Read2(string filename)
        {
            FileStream fs = new FileStream(filename,
                        FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true; //주석무시

            XmlReader reader2 = XmlReader.Create(fs, settings);
            string str = XmlPrint(reader2);
            reader2.Close();
            fs.Close();
            return str;
        }

        //3. 파일명
        public static string Read3(string filename)
        {
            XmlReader reader3 = XmlReader.Create("data.xml");
            string str = XmlPrint(reader3);
            reader3.Close();
            return str; 
        }

        //4. 파일명 + + XmlReaderSettings 객체 이용
        public static string Read4(string filename)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            XmlReader reader4 = XmlReader.Create(filename, settings);
            string str = XmlPrint(reader4);
            reader4.Close();
            return str;
        }

        public static string XmlPrint(XmlReader reader)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriter xwriter = XmlWriter.Create(sb);
            xwriter.WriteNode(reader, false);
            xwriter.Close();
            return sb.ToString();
        }

        #endregion

        #region URL을 통해 Xml문서 획득하기(rss 문서)
        public static string UrlReader(string url)
        {
            XmlUrlResolver resolver = new XmlUrlResolver();
            resolver.Credentials = System.Net.CredentialCache.DefaultCredentials;

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.XmlResolver = resolver;

            XmlReader reader = XmlReader.Create(url, settings);
            string str = XmlPrint(reader);      //********************
            reader.Close();
            return str; 
        }


        #endregion

        #region 노드 분석 예제

        public static string Parse1(string filename)
        {
            XmlReader reader = XmlReader.Create(filename);
            
            reader.MoveToContent();

            string str = string.Empty;

            while (reader.Read())
            {
                str += WriteNodeInfo(reader) + "\r\n";
            }
            return str;
        }

        //특정노드 분석
        public static string WriteNodeInfo(XmlReader reader)
        {
            StringBuilder br = new StringBuilder();
            
            br.Append(string.Format("노드 형:{0}\t", reader.NodeType));
            br.Append(string.Format("▷ 노드 이름:{0}\t", reader.Name));
            br.Append(string.Format("▷노드 데이터:{0}", reader.Value));
            
            return  br.ToString();
        }

        #endregion

        #region URL파싱 (Form3)
        public static List<Item> ItemParse(string url)
        {
            List<Item> items = new List<Item>();

            //--------------------------------------------------
            XmlUrlResolver resolver = new XmlUrlResolver();
            resolver.Credentials = System.Net.CredentialCache.DefaultCredentials;

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.XmlResolver = resolver;

            XmlReader reader = XmlReader.Create(url, settings);

            reader.MoveToContent();
            while (reader.Read())
            {
                if (reader.IsStartElement("item"))
                {
                    Item item = Item.MakeItem(reader);
                    if (item != null) 
                        items.Add(item); 
                }
            }
            //------------------------------------------------
            return items;
        }
        #endregion 
    }
}
