using System;
using System.Threading;
using System.Threading.Tasks;
using Quartz;
using static System.Console;

namespace JobLibrary
{
    public class SimplyJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var schedulerContext = context.Scheduler.Context;
            var valueFromClient = (String)schedulerContext.Get("key1");

            return Task.Run(()=> 
            {
                WriteLine($"Time now: {DateTime.Now}");
                Thread.Sleep(2000);
                WriteLine($"Calling Service with client value: {valueFromClient}");
                Thread.Sleep(2000);
                WriteLine($"Calling Data Access Layer");
                Thread.Sleep(2000);
                WriteLine($"Cleaning/Deleting Data from table: <Customers>");
                WriteLine("----------------------------------------------");
            }); 
        }
    }
}
