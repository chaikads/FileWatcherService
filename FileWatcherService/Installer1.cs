using System.ComponentModel;
using System.ServiceProcess;
using System.Configuration.Install;

namespace FileWatcherService
{
   // Атрибут[RunInstaller(true)] указывает на то,
    // что класс Installer1 должен вызываться при установке сборки, то есть службы.
   [RunInstaller(true)]
    public partial class Installer1 : Installer
    {
        //настройки значений для каждой из запускаемых служб
        ServiceInstaller serviceInstaller;
        //управляет настройкой значений для всех запускаемых служб внутри одного процесса
        ServiceProcessInstaller processInstaller;
        public Installer1()
        {
            InitializeComponent();
            serviceInstaller = new ServiceInstaller();
            processInstaller = new ServiceProcessInstaller();

            processInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = "ServiceChaika";
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
