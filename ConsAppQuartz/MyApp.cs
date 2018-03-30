using Quartz;
using Quartz.Impl;
using JobLibrary;
using static System.Console;

namespace ConsAppQuartz
{
    class MyApp
    {
        static void Main(string[] args)
        {
            var testString = "Test string pass from the Client";
            Run(testString);
            ReadLine();
        }

        public static async void Run(string testValue)
        {
            //Construct scheduler factory;
            ISchedulerFactory schFactory = new StdSchedulerFactory();



            IScheduler scheduler = await schFactory.GetScheduler(new System.Threading.CancellationToken());
            scheduler.Context.Put("key1", testValue);
            await scheduler.Start();

            //Job
            IJobDetail jobDetail = JobBuilder.Create<SimplyJob>().WithIdentity("job1", "group1").Build();

            //Trigger
            ITrigger jobTrigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(10).RepeatForever())
                .Build();

            //schedule
            await scheduler.ScheduleJob(jobDetail, jobTrigger);
        }
    }
}
