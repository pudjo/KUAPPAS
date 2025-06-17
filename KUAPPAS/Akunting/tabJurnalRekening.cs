using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO.Akuntansi;
using BP.Akuntansi;
using Formatting;

namespace KUAPPAS.Akunting
{
    public partial class tabJurnalRekening : UserControl
    {
        private long m_inourutSumber;
        private int m_iPajak;
        public tabJurnalRekening()
        {
            InitializeComponent();
            m_inourutSumber = 0;
        }
        public int Pajak
        {
            set { m_iPajak = value;
            tabControl1.TabPages.Remove(tab1);
            tabControl1.TabPages.Remove(tab2);
            tabControl1.TabPages.Remove(tab3);
            tabControl1.TabPages.Remove(tab4);
            }
        }
        public long NoUrutSumber
        {
            set
            {
                m_inourutSumber = value;

                tabControl1.TabPages.Remove(tab1);
                tabControl1.TabPages.Remove(tab2);
                tabControl1.TabPages.Remove(tab3);
                tabControl1.TabPages.Remove(tab4);

                jr1.Clear();
                jr2.Clear();
                jr3.Clear();
                jr4.Clear();

                LoadData();
            }
        }
        private void  LoadData()
        {

            JurnalLogic oLogic = new JurnalLogic(GlobalVar.TahunAnggaran);
            List<JurnalRekeningShow> lst = new List<JurnalRekeningShow>();
            lst = oLogic.GetByNoUrutSumber(m_inourutSumber, m_iPajak);
            List<long> lstNoJurnal = new List<long>();
            long NoJurnalLama = 0;
            foreach (JurnalRekeningShow jr in lst)
            {
                if (jr.NoJurnal != NoJurnalLama)
                {
                    lstNoJurnal.Add(jr.NoJurnal);
                    NoJurnalLama = jr.NoJurnal;
                }

            }


            if (lst != null)
            {

                if (lstNoJurnal.Count > 0)
                {
                    List<JurnalRekeningShow> lst1 = new List<JurnalRekeningShow>();
                    lst1 = lst.FindAll(x => x.NoJurnal == lstNoJurnal[0]);

                    jr1.SetJurnal(lstNoJurnal[0], lst.FindAll(x => x.NoJurnal == lstNoJurnal[0]));                   

                    tabControl1.TabPages.Add(tab1);
                    tab1.Text = jr1.Judul;
                    if (lstNoJurnal.Count > 1)
                    {

                        jr2.SetJurnal(lstNoJurnal[1],lst.FindAll(x => x.NoJurnal == lstNoJurnal[1]));
                        tabControl1.TabPages.Add(tab2);
                        tab2.Text = jr2.Judul;
                        if (lstNoJurnal.Count > 2)
                        {
                            jr3.SetJurnal(lstNoJurnal[2],lst.FindAll(x => x.NoJurnal == lstNoJurnal[2]));
                            tabControl1.TabPages.Add(tab3);
                            tab3.Text = "PPKD";// jr3.Judul;

                            if (lstNoJurnal.Count > 3)
                            {
                                jr4.SetJurnal(lstNoJurnal[3],lst.FindAll(x => x.NoJurnal == lstNoJurnal[3]));
                                tabControl1.TabPages.Add(tab4);
                                tab4.Text = jr4.Judul;
                            }

                        }
                    }
                }
                
                

            }
            else
            {
                MessageBox.Show(oLogic.LastError());
            }
        }
        private void tabJurnalRekening_Load(object sender, EventArgs e)
        {

        }

        private void jr4_Load(object sender, EventArgs e)
        {

        }

        private void jr1_Load(object sender, EventArgs e)
        {

        }

        private void tab1_Click(object sender, EventArgs e)
        {

        }
    }
}
