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

            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("WSHospitalizationS"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "WSHospitalizationS", "LogHospit");
            }
            eventLog1.Source = "WSHospitalizationS";
            eventLog1.Log = "LogHospit";
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }


        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("In OnStart");

            Sheduler.Start();
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("In OnStop");
        }
    }
}
