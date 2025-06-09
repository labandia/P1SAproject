using System.Collections.Generic;

namespace PMACS_V2.Areas.P1SA.Models
{
   
    public class CapacityPartialViewModel
    {
        public string filter { get; set; }
        public List<CapacitySummaryModel> data { get; set; }
    }
  
}