using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WSHospitalization
{
    public partial class MainService : ServiceBase
    {
        public MainService()
        {
            InitializeComponent();
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }


        protected override void OnStart(string[] args)
        {
            Log.Write("запуск");
            Sheduler.Start();
        }

        protected override void OnStop()
        {
            Log.Write("остановка");
        }
    }
}
