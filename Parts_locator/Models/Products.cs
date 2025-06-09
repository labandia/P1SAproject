using System;
using System.Data;
using System.Windows.Forms;


namespace Parts_locator.Models
{
    internal class Products
    {
        // COMMON PROPERTIES
        private string _partnum;
        private string _productName;
        private int _quanitity = 0;
        private int _sectionID;


        public string Partnum { get { return _partnum;  } set { _partnum = value; } }
        public string Modelname { get { return _productName; } set { _productName = value; } }
        public int Quantity { get { return _quanitity; } set { _quanitity = value; } }
        public int SectionID { get { return _sectionID; } set { _sectionID = value; } }
    }
}
