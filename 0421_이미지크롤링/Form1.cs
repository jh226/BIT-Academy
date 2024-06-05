using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace _0421
{
    public partial class Form1 : Form
    {
        private WbDB db = WbDB.Instance;

        ChromeDriverService _driverService = null;
        ChromeOptions _options = null;
        ChromeDriver _driver = null;
        int i = 0;


        public Form1()
        {
            InitializeComponent();

            db.FillTable();

            _driverService = ChromeDriverService.CreateDefaultService();
            _driverService.HideCommandPromptWindow = true;

            _options = new ChromeOptions();
            _options.AddArgument("disable-gpu");
        }

        #region 테이블 출력
        private void Print_ImageTable()
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("Title\t\t Size\t\t Data\r\n");
           listBox1.Items.Add("----------------------------------------------");
            foreach(DataRow row in db.dt.Rows)
            {
                byte[] data = (byte[])row["Data"];

                listBox1.Items.Add(row["Title"] + "\t\t" + row["Size"] + " byte " + "\t" + data);
            }
            listBox1.Items.Add("----------------------------------------------");

        }
        #endregion

        #region 검색
        private void button1_Click(object sender, EventArgs e)
        {
            string search = textBox1.Text;
            string url = "https://www.google.com/search?q=\"" + search + "&source=lnms&tbm=isch";
            _options.AddArgument("headless"); //창 숨기기

            _driver = new ChromeDriver(_driverService, _options);
            _driver.Navigate().GoToUrl(url); //웹 사이트 접속
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            
            i = 0;
            foreach (IWebElement item in _driver.FindElements(By.ClassName("rg_i"))) //rg_i 이미지의 공통 클래스
            {
                if (item.GetAttribute("src") != null)
                {
                    byte[] data = GetImage(item.GetAttribute("src"));

                    if (db.Insert_Image("image" + i, data, data.Length) == false)
                        MessageBox.Show("저장 실패");
                }
            }

            Print_ImageTable();
        }


        //사진 저장
        private byte[] GetImage(string base64String)
        {            
            byte[] data = null;
            try
            {
                var base64Data = Regex.Match(base64String, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;  // 정규식 검색
                var binData = Convert.FromBase64String(base64Data);

                using (MemoryStream stream = new MemoryStream(binData))
                {
                    var image = System.Drawing.Image.FromStream(stream);
                    string savePath = @"C:\Users\user\Desktop\0421\0421\bin\Debug"; // 이미지를 저장할 디렉토리 경로
                    string fileName = $"image_{i}.jpg"; // 이미지 파일명에 숫자를 추가하여 설정
                    string imagePath = Path.Combine(savePath, fileName);
                    image.Save(imagePath); // 이미지를 파일로 저장
                    i++;
                }
                data = (byte[])binData;
                return data;
            }
            catch
            {
                return data;
            }
        }
        #endregion

        #region 리스트박스 선택
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = listBox1.SelectedIndex-2;
            if(idx >= 0)
            {
                string fileName = $"image_{idx}.jpg";

                pictureBox1.Image = Bitmap.FromFile(fileName);
            }            
        }


        #endregion

        //삭제
        private void button5_Click(object sender, EventArgs e)
        {
            int idx = listBox1.SelectedIndex-1;
            string fileName = ("image_"+ idx);
            if (db.Delete_Member(fileName) == false)
                MessageBox.Show("삭제 실패");
        }

        //SQL UPDATE
        private void button6_Click(object sender, EventArgs e)
        {
            db.SQLUpdate();
        }
    }
}
