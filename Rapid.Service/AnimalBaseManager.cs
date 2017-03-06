using Microsoft.Practices.Unity;
using Rapid.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rapid.Service
{
    public abstract class AnimalBaseManager
    {
        [Dependency]
        public IConfigGateway ConfigGateway { get; set; }

        public virtual string Eat()
        {
            return string.Format("{0} animal eat food.", getprefix());
        }

        internal virtual string getprefix()
        {
            return ConfigGateway.GetAppSetting<string>(Constants.ConfigAppSettings.ANIMAL_PREFIX);
        }
    }
}
