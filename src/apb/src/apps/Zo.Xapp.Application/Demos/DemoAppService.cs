using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Zo.Xapp.Demos
{
    public class DemoAppService : XappAppService, IDemoAppService //, ITransientDependency
    {
        public DemoAppService()
        {
        }

        public string GetInfos()
        {
            return "I'm an App Base On Apb + MahApps.Metro";
        }
    }
}
