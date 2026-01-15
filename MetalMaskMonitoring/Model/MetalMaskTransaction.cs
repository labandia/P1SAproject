using System;

namespace MetalMaskMonitoring.Model
{
    public class MetalMaskTransaction
    {
        public int RecordID { get; private set; }

        public bool? Shift { get; private set; } // false = Day, true = Night

        public DateTime CleanDate { get; private set; }

        public string Partnumber { get; private set; }

        public int AREA { get; private set; }

        public string RevisionNo { get; private set; }

        public int ReadOne { get; private set; }
        public int ReadTwo { get; private set; }
        public int ReadThree { get; private set; }
        public int ReadFour { get; private set; }
        public int ReadFive { get; private set; }

        public string Remarks { get; private set; }

    }
}
