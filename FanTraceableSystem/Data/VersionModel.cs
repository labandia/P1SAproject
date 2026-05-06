using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanTraceableSystem.Data
{
    public class UpdateVersionModel
    {
        public string SystemName { get; set; }
        public string VersionNumber { get; set; }
        public bool ForceUpdate { get; set; }
        public string MinRequiredVersion { get; set; }
    }
}
