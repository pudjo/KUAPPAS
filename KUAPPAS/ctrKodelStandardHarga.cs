using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DTO;
using BP;
using Formatting;


namespace KUAPPAS
{
    public partial class ctrKodelStandardHarga : UserControl
    {

        string cKode;
        int ilevel;
        private StandardBiaya m_sb;

        public ctrKodelStandardHarga()
        {
            InitializeComponent();
        }

        public void SetStandardBiaya(StandardBiaya sb)
        {
            Clear();
            m_sb = sb;
            Create(sb.IDBiaya);

        }
        private void ctrKodelStandardHarga_Load(object sender, EventArgs e)
        {
            ilevel = 0;
        }

        public void Clear()
        {
            txtKode1.Text = "";
            txtKode2.Text = "";
            txtKode3.Text = "";
            txtKode4.Text = "";
            txtKode5.Text = "";
            txtKode6.Text = "";
            txtKode7.Text = "";
            lblNama.Text = "";
        }
        public bool Create(string  oRekening )
        {
            try
            {
                StandardBiayaLogic OlOGIC = new StandardBiayaLogic(GlobalVar.TahunAnggaran);
                StandardBiaya sb = new StandardBiaya();

                int len34 = 3;
                if ((int)GlobalVar.TahunAnggaran < 2020)
                    len34 = 2;
                //sb = OlOGIC.GetByID(oRekening);

                if (m_sb == null)
                    return false;

                cKode = oRekening;

                ilevel = (int)m_sb.Level;// GetLevel();

                if (ilevel>=0)
                txtKode1.Text = oRekening.Substring(0, 1);


                if (ilevel >= 1)
                txtKode2.Text = oRekening.Substring(1, 2);

                if (ilevel >= 2)
                txtKode3.Text = oRekening.Substring(3, 2);



                if (ilevel >= 3)
                    txtKode4.Text = oRekening.Substring(5, len34);

                if (ilevel >= 4)
                    txtKode5.Text = oRekening.Substring(8, len34);


                if (ilevel >= 5)
                    txtKode6.Text = oRekening.Substring(11, 2);
                if (ilevel >= 6)
                
                txtKode7.Text =  oRekening.Substring(13, 2);
            
                txtKode1.Enabled = false;
                txtKode2.Enabled = false;
                txtKode3.Enabled = false;
                txtKode4.Enabled = false;
                txtKode5.Enabled = false;
                txtKode7.Enabled = false;


                RefreshTampilanBasedRoot();


                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
        //public int GetMaxOnLevel()
        //{
        //    if (cKode.Trim() == "")
        //        return 0;

        //    if (GetLevel() > 1)
        //    {

        //    }


        //}

        private int GetLevel()
        {
            switch (cKode.Length){
                    case 1:
                        return 0;
                       // break;
                    case 3:
                        return 1;
                      //  break;
                    case 5:
                        return 2;
                      //  break;
                    case 7:
                        return 3;
                      //  break;
                    case 9:
                        return 4;
                      //  break;

                    case 11 :
                    
                        return 5;
                    //    break;

            }
            return 6;

        }
        public int GetLevelEx (){
            return ilevel;
        }
        private void RefreshTampilanBasedRoot(int pLevel = -1)
        {
            if (pLevel == -1)
            {
                pLevel = ilevel;
            } 

            txtKode1.Visible = true;
            txtKode2.Visible = false;
            txtKode3.Visible = false;
            txtKode4.Visible = false;
            txtKode5.Visible = false;
            txtKode6.Visible = false;

            if (pLevel > 0)
            {
                txtKode2.Visible = true;
                if (pLevel > 1)
                {
                    txtKode3.Visible = true;
                    if (pLevel > 2)
                    {
                        txtKode4.Visible = true;
                        if (pLevel > 3)
                        {
                            txtKode5.Visible = true;
                            if (pLevel > 4)
                            {
                                txtKode6.Visible = true;
                                if (pLevel > 5)
                                {
                                    txtKode7.Visible = true;
                                    //if (ilevel > 6)
                                    //{
                                    //    txtKode7.Visible = true;

                                    //}
                                }
                            }
                            
                        }
                    }
                }
            }
        }

        public void  SetParent (string sParent ){
            
            StandardBiayaLogic OlOGIC = new StandardBiayaLogic(GlobalVar.TahunAnggaran);
            StandardBiaya sb = new StandardBiaya();
            

            
            if (ilevel == 1)
            {
                sParent="";;
            }
            if (ilevel == 2)
            {
                sParent="";;
            }
            if (ilevel == 3)
            {
                sParent="";;
            }
            if (ilevel == 4)
            {
                sParent="";;
            }
            if (ilevel == 5)
            {
                sParent="";;
            }


        }
     
        public void SetNewKode(string newKode)
        {
            cKode = newKode;

         //   ilevel = GetLevel();

           // txtKode1.Text = newKode.Substring(0, 1);
            //txtKode2.Text = newKode.Substring(1, 2);


            switch (ilevel+1)
            {
                case 1:
                txtKode2.Text = newKode.Substring(1, 2);
                break;
                case 2:
                txtKode3.Text = newKode.Substring(3, 2);
                break;
                case  3:
                txtKode4.Text = newKode.Substring(5, 3);
                break;
                case  4:
                txtKode5.Text = newKode.Substring(8,3);
                break;

                case 5:
                    txtKode6.Text = newKode.Substring(11, 2);
                    break;
                case 6:
                txtKode7.Text =  newKode.Substring(13, 2);     
                break;
            }



           // if (newKode.Length > 4)
            
            //txtKode3.Text = newKode.Substring(3, 2);
            //if (newKode.Length > 6)

            //    txtKode4.Text = newKode.Substring(5, 2);
            //else
            //    txtKode4.Text = "";

            //if (newKode.Length > 8)

            //    txtKode5.Text = newKode.Substring(7,2);
            //else
            //    txtKode5.Text = "";

            //if (newKode.Length > 10)
            //    txtKode6.Text = newKode.Substring(9);
            //else 
            //txtKode6.Text = "";

            txtKode1.Enabled = false;
            txtKode2.Enabled = false;
            txtKode3.Enabled = false;
            txtKode4.Enabled = false;
            txtKode5.Enabled = false;
            txtKode6.Enabled = false;

            RefreshTampilanBasedRoot(ilevel+1);
        }
        public void AddRincian()
        {
            switch (ilevel)
            {
                case 1:
                    txtKode2.Enabled = true;
                    break;
                case 2:
                    txtKode3.Enabled = true;
                    break;
                case 3:
                    txtKode4.Enabled = true;
                    break;
                case 4:
                    txtKode5.Enabled = true;
                    break;
                case 5:
                    txtKode6.Enabled = true;
                    break;

            }
        }
        public string  GetID()
        {
            string  lRet;
            //int iLevel = GetLevelEx();
            ////if iLevel > 0 
            //if (DataFormat.GetInteger(txtKode1.Text) > 9)
            //    return "Salah Kode segmen I";
            //if (iLevel > 0 )
            //if (DataFormat.GetInteger(txtKode2.Text) > 99)
            //    return "Salah Kode segmen II";

            //if (iLevel > 1)
            //if (DataFormat.GetInteger(txtKode3.Text) > 99)
            //    return "Salah Kode segmen III";

            //if (iLevel > 2)
            //if (DataFormat.GetInteger(txtKode4.Text) > 99)
            //    return "Salah Kode segmen IV";

            //if (iLevel > 3 )
            //if (DataFormat.GetInteger(txtKode5.Text) > 99)
            //    return "Salah Kode segmen V";


            lRet = txtKode1.Text + txtKode2.Text+ txtKode3.Text+txtKode4.Text+txtKode5.Text;

            return lRet;


        }
        
        public Single GetRoot()
        {
            return ilevel;
        }
        
    }
}
