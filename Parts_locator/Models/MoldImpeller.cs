using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parts_locator.Models
{
    public class MoldImpeller
    {
        public string PartNumber { get; set; } = "";
        public string ModelName { get; set; } = "";
        public string RotorBush { get; set; } = "";
        public string ShaftPartnum { get; set; } = "";
        public string ShaftBushAssyPartnum { get; set; } = "";
        public int Quantity { get; set; } = 0;
        public int Racks { get; set; } = 0;
    }
}
