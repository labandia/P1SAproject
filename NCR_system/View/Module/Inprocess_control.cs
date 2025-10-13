using NCR_system.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCR_system.View.Module
{
    public partial class Inprocess_control : UserControl
    {
        private readonly IInprocess _inp;


        public DataGridView Inprocessgrid { get { return InprocessGrid; } }

        public Inprocess_control(IInprocess inp)
        {
            InitializeComponent();
            _inp = inp;
        }


        public async Task DisplayRejected()
        {
            try
            {
                // For Displaying Customer
                InprocessGrid.DataSource = null;
                var inprocesslist = (await _inp.GetInprocessData(1)).ToList();
                InprocessGrid.DataSource = inprocesslist;


                // 🔹 Define all known sections
                var sections = new List<KeyValuePair<int, string>>()
                {
                    new KeyValuePair<int, string>(1, "Molding"),
                    new KeyValuePair<int, string>(2, "Press"),
                    new KeyValuePair<int, string>(3, "Rotor"),
                    new KeyValuePair<int, string>(4, "Winding"),
                    new KeyValuePair<int, string>(5, "Circuit")
                };


                // 🔹 Group existing open items
                //var openCounts = inprocesslist
                //     .Where(c => c.Status == 1)
                //    .GroupBy(c => c.SectionID)
                //    .ToDictionary(g => g.Key, g => g.Count());

                //// 🔹 Merge all sections with counts (include 0 if missing)
                //var summary = sections
                //    .Select(s => new
                //    {
                //        Section = s.Value,
                //        TotalOpen = openCounts.ContainsKey(s.Key) ? openCounts[s.Key] : 0
                //    })
                //    .ToList();

                // 🔹 Display summary
                //SummaryInprocess.DataSource = summary;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
