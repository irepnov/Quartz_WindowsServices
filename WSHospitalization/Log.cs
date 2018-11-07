using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WSHospitalization
{
    public static class Log
    {
        public static void Write(string _lines)
        {
            string apLog;
            apLog = Application.StartupPath + "\\WSHospitalizationLog_YYYYMMDD.log";
            apLog = apLog.Replace("YYYY", DateTime.Now.Year.ToString());
            apLog = apLog.Replace("MM", ("0" + DateTime.Now.Month.ToString()).Length > 2 ? ("0" + DateTime.Now.Month.ToString()).Substring(1, 2) : ("0" + DateTime.Now.Month.ToString()) );
            apLog = apLog.Replace("DD", ("0" + DateTime.Now.Day.ToString()).Length > 2 ? ("0" + DateTime.Now.Day.ToString()).Substring(1, 2) : ("0" + DateTime.Now.Day.ToString()) );
            _lines = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " - " + _lines;
            System.IO.File.AppendAllLines(apLog, new string[] { _lines });
        }
    }
}
