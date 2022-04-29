using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace WindowsService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        public override void Install(IDictionary stateSaver)
        {
            serviceInstaller1.ServiceName = Configuration.GetInstanceName(GetAssemblyPath());
            serviceInstaller1.DisplayName = serviceInstaller1.ServiceName;
            base.Install(stateSaver);
        }

        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            serviceInstaller1.ServiceName = Configuration.GetInstanceName(GetAssemblyPath());
            base.OnBeforeUninstall(savedState);
        }

        private string GetAssemblyPath()
        {
            var mypath = this.Context.Parameters["assemblypath"];
            return mypath;
        }
    }
}
