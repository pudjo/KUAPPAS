using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BP;
using DTO;

namespace KUAPPAS
{
    public partial class ctrlPejabat : UserControl
    {
        List<Pejabat> mlstPejabat;
        int m_SelectedID;
        public ctrlPejabat()
        {
            InitializeComponent();
            m_SelectedID = 0;

        }

        private void cmbPejabat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void Create(int dinas, int KodeUK,int jenis)
        {
            try
            {
               
                PejabatLogic oLogic = new PejabatLogic(GlobalVar.TahunAnggaran);
                mlstPejabat = new List<Pejabat>();
                cmbPejabat.Items.Clear();
                mlstPejabat = oLogic.GetByJenisAndDinas(jenis, dinas, KodeUK);
               
                if (mlstPejabat != null)
                {
                    foreach (Pejabat p in mlstPejabat)
                    {
                        ListItemData li = new ListItemData(p.Nama, p.ID, p);
                        cmbPejabat.Items.Add(li);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private int GetID()
        {
            int idx = cmbPejabat.SelectedIndex;
            ListItemData li = (ListItemData)cmbPejabat.Items[idx];
            return li.Itemdata;
        }
        public Pejabat GetPejabat()
        {
            int baris=0;
            try
            {
                //int idx = cmbPejabat.SelectedIndex;
                //ListItemData li = (ListItemData)cmbPejabat.Items[idx];
                //return (Pejabat)li.something;
                Pejabat pejabat = new Pejabat();
                baris++;
              //  MessageBox.Show(cmbPejabat.Items.Count.ToString());

                for (int i = 0; i < cmbPejabat.Items.Count; i++)
                {
                   baris++;
                    ListItemData li = (ListItemData)cmbPejabat.Items[i];
                    baris++;
                    if (li.ItemText == cmbPejabat.Text)
                    {
                        pejabat = mlstPejabat.FirstOrDefault(x => x.ID == li.Itemdata);
                        break;
                    }

                }
                baris++;
                return pejabat;
            }
            catch (Exception ex)
            {
                MessageBox.Show(baris.ToString() + "" + ex.Message);
                return null;

            } 
       }
        public void SetID(int id)
        {
            int selecttdIndex = 0;
            foreach (ListItemData li in cmbPejabat.Items)
            {
                if (li.Itemdata== id)
                {
                    cmbPejabat.SelectedItem = li;
                    cmbPejabat.SelectedIndex =selecttdIndex;

                    break;
                }
                selecttdIndex++;
            }

        }

    }
}
