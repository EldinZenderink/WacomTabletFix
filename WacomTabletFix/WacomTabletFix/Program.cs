using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace WacomTabletFix
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hi, I am going to close all the related wacom processes and restart the wacom service, LETS GO :D.");

            Thread.Sleep(2000);
            var allProcceses = Process.GetProcesses();

            foreach(var process in allProcceses)
            {
                try
                {
                    if (process.ProcessName.Contains("Pen_"))
                    {
                        Console.WriteLine("Killing process: " + process.ProcessName);
                        process.Kill();
                    } else if (process.ProcessName.Contains("WTablet"))
                    {
                        Console.WriteLine("Killing process: " + process.ProcessName);
                        process.Kill();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine("Alright, depending if there were no errors, we can now start restarting the wacom service!");

            Thread.Sleep(2000);

            ServiceController service = new ServiceController("WTabletServiceCon");
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(1000);

                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            Console.WriteLine("Everything should work fine now :D, I will close automatically :D");

            Thread.Sleep(2000);

        }
    }
}
