using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TestWidnowsService.Properties;

namespace TestWidnowsService
{
  public partial class Service1 : ServiceBase
  {
    private Timer _timer;
    public Service1()
    {
      InitializeComponent();
    }

    protected override void OnStart(string[] args)
    {
      _timer = new Timer(1000);
      _timer.Elapsed += (s, e) =>
      {
        File.AppendAllText(@"C:\users\michal\desktop\serviceFile.txt", DateTime.Now.ToString() + "\n");
      };
      _timer.Start();
      File.AppendAllText(@"C:\users\michal\desktop\serviceFile.txt", Directory.GetCurrentDirectory() + "\n");
      File.AppendAllText(@"C:\users\michal\desktop\serviceFile.txt", Settings.Default.TextToWriteToFile + "\n");

      //try
      //{
      //  throw new Exception();
      //}
      //catch
      //{
      //  File.AppendAllText(@"C:\users\michal\desktop\serviceFile.txt", "Błąd w usłudze zatrzymywanie.\n");
      //  Stop();
      //}
    }

    protected override void OnStop()
    {
      base.OnStop();
      File.AppendAllText(@"C:\users\michal\desktop\serviceFile.txt", "Stop usłgui\n");
      _timer?.Stop();
      _timer?.Dispose();
    }
  }
}
