using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DTO;

namespace KUAPPAS
{
    public partial class ctrlNavigation : UserControl
    {
        public delegate EventResponseMessage MyDelegate();
        public delegate void MyVoidDelegate();

        public event MyDelegate OnAdd;
        public event MyDelegate OnEdit;
        public event MyDelegate OnSave;
        public event MyDelegate OnDelete;
        public event MyVoidDelegate OnExit;
        public event MyDelegate OnCancel;
        public event MyDelegate OnValidation;

        public ctrlNavigation()
        {
            InitializeComponent();
            BackColor = Color.LightGoldenrodYellow;
        }

        private void ctrlNavigation_Load(object sender, EventArgs e)
        {
            //

        }
        public bool AllowDelete {

            set { cmdHapus.Visible = !value; }        
        }
        public void SetNew()
        {
            if (OnAdd != null)
            {
                cmdAdd.Enabled = false;
                cmdEdit.Enabled = false;
                cmdSimpan.Enabled = true;
                cmdExit.Enabled = false;
                cmdCancel.Enabled = true;
                OnAdd();
            }
        }


        private void cmdAdd_Click(object sender, EventArgs e)
        {
            ToAdd();
        }
        public void ToAdd()
        {
            if (OnAdd != null)
            {
                cmdAdd.Enabled = false;
                cmdEdit.Enabled = false;
                cmdSimpan.Enabled = true;
                cmdExit.Enabled = false;
                cmdCancel.Enabled = true;
                OnAdd();
            }
        }


        private void cmdEdit_Click(object sender, EventArgs e)
        {
            EventResponseMessage response = new EventResponseMessage();
            response.ResponseStatus = true;
            if (OnEdit != null)
            {
                response = OnEdit();
                if (response.ResponseStatus == true)
                {
                    cmdAdd.Enabled = false;
                    cmdEdit.Enabled = false;
                    cmdSimpan.Enabled = true;
                    cmdExit.Enabled = false;
                    cmdCancel.Enabled = true;

                }
            }
            else
            {
                cmdAdd.Enabled = false;
                cmdEdit.Enabled = false;
                cmdSimpan.Enabled = true;
                cmdExit.Enabled = false;
                cmdCancel.Enabled = true;
            }

        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            EventResponseMessage response = new EventResponseMessage();                
            if (OnCancel != null)
            {
                response= OnCancel();
                if (response.ResponseStatus == true)
                {
                    cmdCancel.Enabled = false;
                    cmdAdd.Enabled = true;
                    cmdEdit.Enabled = true;
                    cmdSimpan.Enabled = false;
                    cmdExit.Enabled = true;
                }

            }
            else
            {
                if (MessageBox.Show("Akan membatalkan input/edit?", "Pembatalan", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    cmdCancel.Enabled = false;
                    cmdAdd.Enabled = true;
                    cmdEdit.Enabled = true;
                    cmdSimpan.Enabled = false;
                    cmdExit.Enabled = true;
                }

            }

        }

        private void cmdSimpan_Click(object sender, EventArgs e)
        {
            EventResponseMessage response = new EventResponseMessage();

            if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_AUDIT)
            {
                MessageBox.Show("Tidak bisa memproses ..");
                response.ResponseStatus = false;
                return ;

            }

                
            if (OnSave != null)
            {
                if (OnValidation != null)
                {
                    response = OnValidation();
                    if (response.ResponseStatus == true)
                    {
                        response = OnSave();
                        if (response.ResponseStatus == true)
                        {
                            cmdCancel.Enabled = false;
                            cmdAdd.Enabled = true;
                            cmdEdit.Enabled = true;
                            cmdSimpan.Enabled = false;
                            cmdExit.Enabled = true;
                            MessageBox.Show("Penyimpanan berhasil");
                        }
                        else
                        {
                            MessageBox.Show("Penyimpanan gagal dilakukan");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Penyimpanan gagal dilakukan");
                    }
                }
                else
                {
                   response = OnSave();
                    if (response.ResponseStatus == true)
                    {
                        cmdCancel.Enabled = false;
                        cmdAdd.Enabled = true;
                        cmdEdit.Enabled = true;
                        cmdSimpan.Enabled = false;
                        cmdExit.Enabled = true;
                        MessageBox.Show("Penyimpanan berhasil");
                    }
                    else
                    {
                        MessageBox.Show("Penyimpanan gagal dilakukan");
                    }
                }
            }
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            try
            {
                if (OnExit != null)
                {
                    OnExit();
                }

                Form f = this.FindForm();
                if (f != null)
                    f.Dispose();

            }
            catch (Exception)
            {

            }


        }

        private void cmdHapus_Click(object sender, EventArgs e)
        {
            // if MessageBox.Show ("Benar akan menghapus data ini?","Penghapusan",MessageBoxButtons.YesNo )==MessageBoxButtons
            if (OnDelete != null)
            {
                OnDelete();
            }


        }
    }
}
