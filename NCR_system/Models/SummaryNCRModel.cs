using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCR_system.Models
{
    public  class SummaryNCRModel
    {
        public int SectionID { get; set; }  
        public string DepartmentName { get; set; }  
        public int SDC { get; set; }
        public int External { get; set; }
    }

    public class SummaryInprocessModel
    {
        public int SectionID { get; set; }
        public string DepartmentName { get; set; }
        public int OpenCase { get; set; }   
    }

    public class OverallNCR
    {
        public string SectionName { get; set; }
        public int OpenTotals { get; set; }
    }
}
