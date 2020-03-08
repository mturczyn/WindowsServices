using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestWidnowsService.Properties;

namespace TestWidnowsService
{
  [RunInstaller(true)]
  public partial class ProjectInstaller : System.Configuration.Install.Installer
  {
    public ProjectInstaller()
    {
      InitializeComponent();
      File.AppendAllText(@"C:\users\michal\desktop\serviceFile.txt", $"initialize comp: {Settings.Default.StartMode}\n");
      this.serviceInstaller1.StartType = Settings.Default.StartMode;
      this.serviceProcessInstaller1.Account = Settings.Default.ServiceAccount;
      this.serviceProcessInstaller1.Username = Settings.Default.User;
      this.serviceProcessInstaller1.Password = Settings.Default.Password;
    }
    /// <summary>
    /// W trakcie instalacji zmiany do instalatorów nie mają skutku. Trzeba to zrobić w InitializaComponent. Ale tam z kolei nie mamy dostępu do 
    /// parametrów z wierza poleceń.
    /// </summary>
    /// <param name="stateSaver"></param>
    public override void Install(IDictionary stateSaver)
    {
      base.Install(stateSaver);
      File.AppendAllText(@"c:\users\michal\desktop\servicefile.txt", "Przed słownikiem\n");
      var enumerator = stateSaver.Keys.GetEnumerator();
      while (enumerator.MoveNext())
        File.AppendAllText(@"c:\users\michal\desktop\servicefile.txt", "słownik: " + enumerator.Current.ToString());
      File.AppendAllText(@"c:\users\michal\desktop\servicefile.txt", "po słowniku\n");

      File.AppendAllText(@"c:\users\michal\desktop\servicefile.txt", "zaczynamy instalację usługi\n");
    }
    /// <summary>
    /// Metoda pobierające wskazany parametr z lini poleceń, dostarczony jako [nazwa]=[wartość]
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    private object GetParam(string p)
    {
      try
      {
        if (this.Context != null && this.Context.Parameters != null)
        {
          return this.Context.Parameters[p];
        }
      }
      catch
      {
      }
      return null;
    }
  }
}
