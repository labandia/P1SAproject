using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;

namespace NCR_system
{
    public partial class NonConformity : Form
    {
        public NonConformity()
        {
            InitializeComponent();
        }

        private async void NonConformity_Load(object sender, EventArgs e)
        {
            //await webView21.EnsureCoreWebView2Async(null);
            //webView21.CoreWebView2.Navigate("http://p1saportalweb.sdp.com/");
        }
    }
}
