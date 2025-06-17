using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Windows.Forms;

namespace KUAPPAS
{
    class DataGridViewBarGraphColumn :
    DataGridViewColumn
    {
        public DataGridViewBarGraphColumn()
        {
        }

        public long MaxValue;
        private bool needsRecalc = true;

        public void CalcMaxValue()
        {
        }
    }
}
