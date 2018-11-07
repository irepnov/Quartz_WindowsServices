using GGPlatform.DBServer;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WSHospitalization
{
    class JobHospital : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() =>
            {
                try
                {
                    string apServer, apDatabase;
                    apServer = System.Configuration.ConfigurationManager.AppSettings["ServerName"];
                    apDatabase = System.Configuration.ConfigurationManager.AppSettings["BaseName"];

                    DBSqlServer _sql = new DBSqlServer(apServer, apDatabase, null, "");
                    _sql.Connect("sa", "sasa", false);

                    getKDInformation(_sql);
                    getPlanHospitalization(_sql);
                }
                catch(Exception ee)
                {
                    Log.Write(ee.Message); 
                }                
            });
        }

        private bool getPlanHospitalization(DBSqlServer sql)
        {
            bool res = false;

            try
            {
                sql.SQLScript = "select id from packages where PackageType = 2 and DATEADD(day, 0, DATEDIFF(day, 0, CreateDate)) = @date";
                sql.AddParameter("@date", EnumDBDataTypes.EDT_DateTime, DateTime.Now.ToShortDateString());
                if (sql.ExecuteScalar() != null)
                    return true;

                sql.SQLScript = "insert into packages(PackageType, CreateDate) values(2, getdate())";
                sql.ExecuteNonQuery();
                res = true;
            }
            catch (Exception ee)
            {
                res = false;
                Log.Write("getPlanHospitalization " + ee.Message);
            }

            return res;
        }

        private bool getKDInformation(DBSqlServer sql)
        {
            bool res = false;

            try
            {
                sql.SQLScript = "select id from packages where PackageType = 1 and DATEADD(day, 0, DATEDIFF(day, 0, CreateDate)) = @date";
                sql.AddParameter("@date", EnumDBDataTypes.EDT_DateTime, DateTime.Now.ToShortDateString());
                if (sql.ExecuteScalar() != null)
                    return true;

                sql.SQLScript = "insert into packages(PackageType, CreateDate) values(1, getdate())";
                sql.ExecuteNonQuery();
                res = true;
            }
            catch (Exception ee)
            {
                res = false;
                Log.Write("getKDInformation " + ee.Message);
            }

            return res;
        }

    }
}
