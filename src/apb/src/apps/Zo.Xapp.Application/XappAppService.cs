using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Zo.Xapp.Localization;

namespace Zo.Xapp
{
    /* Inherit your application services from this class.
     */
    public abstract class XappAppService : ApplicationService
    {
        protected XappAppService()
        {
            LocalizationResource = typeof(XappResource);

        }
    }
}