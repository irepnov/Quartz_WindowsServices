using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSHospitalization
{
    class Sheduler
    {
        public static async void Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<JobHospital>().Build();

            //каждый день, каждый час в 30 минут
            ITrigger trigger3 = TriggerBuilder.Create()
              .WithIdentity("trigger2", "group2")
              .StartAt(DateBuilder.DateOf(14, 00, 00))
              .WithSimpleSchedule(s => s.WithIntervalInMinutes(2) //повторять с интервалом в 1 час
                                        .RepeatForever()  //повтоярть бесконечно
                                        .WithMisfireHandlingInstructionNextWithExistingCount()) //если задание ыбло пропущено, ждать слудующее
              .Build();

            await scheduler.ScheduleJob(job, trigger3);        // начинаем выполнение работы
        }
    }
}
