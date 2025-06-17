using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUAPPAS
{
    public partial class frmCulture : Form
    {
        public frmCulture()
        {
            InitializeComponent();
        }

        private void frmCulture_Load(object sender, EventArgs e)
        {
            // Clear cached data for the current culture
            Thread.CurrentThread.CurrentCulture.ClearCachedData();

            // In a new thread instance we get current culture.
            // This code avoid getting wrong cached cultureinfo objects when user replaces some values in the regional settings without restarting the application
            CultureInfo currentCulture = new Thread(() => { }).CurrentCulture;

        }
    }
}
