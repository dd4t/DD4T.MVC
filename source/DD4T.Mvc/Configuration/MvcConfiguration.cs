using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD4T.MVC.Configuration
{
    public class MvcConfiguration : IMvcConfiguration
    {
        public string ContentManagerUrl => ConfigurationManager.AppSettings[MvcConfigurationKeys.ContentManagerUrl];
    }
}
