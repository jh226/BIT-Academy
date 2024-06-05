using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace _0428_성적.MVVM
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        // Model 객체 선언
        private Model _model;

        // DB 접근 객체 선언
        private Repo _repo;

        // Timer 선언
        private Timer _timer;

        const string DB_NAME = "DESKTOP-ELKJL3J\\SQLEXPRESS";
        const string DB_DATABALSE = "SampleDB";
        const string DB_ID = "wb37";
        const string DB_PW = "1234";

        string _connstring = string.Format("Data Source= {0};Initial Catalog={1}; User ID= {2}; Password={3}", DB_NAME, DB_DATABALSE, DB_ID, DB_PW);


        public MainViewModel()
        {
            _model = new Model();
            //_repo = new Core.Repo(_connstring);

            // 타이머 설정
            _timer = new Timer(1000);
            _timer.Elapsed += TmrMonitoring;
            //_timer.Start();

        }
        public DataView DV_1
        {
            get { return _model.dv_1; }
            set { _model.dv_1 = value; OnPropertyChanged("DV_1"); }
        }



        private void TmrMonitoring(object sender, ElapsedEventArgs e)
        {
            //DV_1 = _repo.GetData().Copy().DefaultView;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            { handler(this, new PropertyChangedEventArgs(name)); }
        }
    }
}
