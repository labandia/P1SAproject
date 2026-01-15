using MetalMaskMonitoring.Subforms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetalMaskMonitoring
{
    public partial class MetalMaskFormOut : Form
    {
        private int currentStep = 0;
        private UserControl[] steps;

        private readonly Brushing _br;
        private readonly TensionForms _tf;
        private readonly Washingprocess _wash;

        private readonly int _RecordID;
        private readonly int _statsStep;

        public MetalMaskFormOut(int recordID)
        {
            InitializeComponent();
            _RecordID = recordID;

            steps = new UserControl[]
            {
                new Washingprocess(recordID),
                new TensionForms(recordID),
                new Brushing(recordID),
                new Brushing(recordID)
            };

            // 🔹 Load FIRST step immediately
            LoadStep();
        }

        private void LoadStep()
        {
            ColorStepsDone();

            panelContent.Controls.Clear();
            panelContent.Controls.Add(steps[currentStep]);
            steps[currentStep].Dock = DockStyle.Fill;

            btnBack.Enabled = currentStep > 0;
            btnNext.Text = currentStep == steps.Length - 1 ? "Finish" : "Next";

            lblStep.Text = $"Step {currentStep + 1} of {steps.Length}";
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //if (steps[currentStep] is Brushing s1 && !s1.IsValid())
            //{
            //    MessageBox.Show("Please complete Step 1");
            //    return;
            //}

            if (currentStep == steps.Length - 1)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                currentStep++;
                LoadStep();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (currentStep > 0)
            {
                currentStep--;
                LoadStep();
            }
        }

        public void ColorStepsDone()
        {
            if (currentStep == 0)
            {
                StepsIcon_1.BackColor = Color.Aqua;
            }else if(currentStep <= 1)
            {
                StepsIcon_1.BackColor = Color.Aqua;
                StepsIcon_2.BackColor = Color.Aqua;
            }
            else if (currentStep <= 2)
            {
                StepsIcon_1.BackColor = Color.Aqua;
                StepsIcon_2.BackColor = Color.Aqua;
                StepsIcon_3.BackColor = Color.Aqua;
            }
            else
            {
                StepsIcon_1.BackColor = Color.Aqua;
                StepsIcon_2.BackColor = Color.Aqua;
                StepsIcon_3.BackColor = Color.Aqua;
                StepsIcon_4.BackColor = Color.Aqua;
            }
        }
    }
}
