using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0420_메모리DB
{
    internal class WbMemoryDB
    {
        public DataTable Book_Table {get; set;} = null;

        public string Book_TableName { get { return Book_Table.TableName; } }
        public int Book_ColumeCount { get { return Book_ColumeCount; }}
        
        public void Create_BookTable()
        {
            Book_Table = new DataTable("book");

            DataColumn dc_title = new DataColumn("Title", typeof(string));
            dc_title.AllowDBNull = false;
            Book_Table.Columns.Add(dc_title);

            DataColumn dc_isbn = new DataColumn("ISBN", typeof(string));
            dc_isbn.Unique = true;
            dc_isbn.AllowDBNull = false;
            Book_Table.Columns.Add(dc_isbn);

            DataColumn dc_author = new DataColumn("Author", typeof(string));
            dc_author.AllowDBNull = false;
            Book_Table.Columns.Add(dc_author);

            DataColumn dc_price = new DataColumn("Price", typeof(int));
            dc_price.AllowDBNull = false;
            Book_Table.Columns.Add(dc_price);

            DataColumn[] pkeys = new DataColumn[1];
            pkeys[0] = dc_isbn;
            Book_Table.PrimaryKey = pkeys;
        }

        public bool Insert_Book(string isbn, string title, string author, int price)
        {
            try
            {
                DataRow dr = Book_Table.NewRow();
                dr["ISBN"] = isbn;
                dr["Title"] = title;
                dr["Author"] = author;
                dr["Price"] = price;
                Book_Table.Rows.Add(dr);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} 추가 실패", title);
                Console.WriteLine("이유:{0}", e.Message);
                return false;
            }

        }

        public string Book_GetRowData(int idx)
        {
            DataRow r = Book_Table.Rows[idx];

            string str = string.Empty;
            for(int i = 0; i <Book_Table.Columns.Count; i++)
            {
                str += r[i].ToString() + "\r\n";
            }
            return str;
        }


        #region XML 문서 다루기
        public void Xml_Write()
        {
            string schema_fname = "books.xsd";
            string fname = "book.xml";

            Book_Table.WriteXmlSchema(schema_fname, true);
            Book_Table.WriteXml(fname, true);
        }

        public void Xml_Read()
        {
            string schema_fname = "books.xsd";
            string fname = "book.xml";

            if(File.Exists(schema_fname))
            {
                Book_Table = new DataTable("book");

                Book_Table.ReadXmlSchema(schema_fname);
                if(File.Exists(fname))
                {
                    Book_Table.ReadXml(fname);
                }
            }
        }
        #endregion
    }
}
