using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using DTO.Bendahara;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BP.Bendahara;

namespace KUAPPAS
{
    public partial class ctrlProgramKegiatan : UserControl
    {
        public delegate void ValueChangedEventHandler(int pIDurusan, int pIDProgram, int pIDKegiaan, long pIDSubKegiatan);

        public event ValueChangedEventHandler OnChanged;
        private int m_SelectedID;
        private int m_iDinas;
   
        private int m_iSumber;
        private int m_iKodeUK;


        private int m_idDinas;
        private int m_idUrusan;
        private int m_idProgram;
        private int  m_idKegiatan;
        private long  m_idSubKegiatan;

        private int  m_idPaket;
        private long m_NoUrutSPP;
        private long m_inourutSPD;
        private long m_iNoUrutBAST;
        private long m_iNoUrutSPJ;



        public ctrlProgramKegiatan()
        {
            InitializeComponent();
            m_NoUrutSPP = 0;
            m_inourutSPD = 0;
            m_iNoUrutBAST = 0;
            m_iNoUrutSPJ = 0;
            m_iSumber = 1;
        }
        public void Clear()
        {
            ctrlUrusanPemerintahan1.Clear();
            ctrlProgram1.Clear();
            ctrlKegiatanAPBD1.Clear();
            ctrlSubKegiatan1.Clear();

        }
        public void SetNoUrutBAST(long noUrutBaST)
        {
            m_iNoUrutBAST = noUrutBaST;
        }
        public void SetNoUrutSPJ(long noUrutSPJ)
        {
            m_iNoUrutSPJ = noUrutSPJ;
        }
        public void SetNoUrutSP2D(long noUrutSP2D)
        {
            m_NoUrutSPP = noUrutSP2D;
        }


        public void SetSumber(int iSumber)
        {
            // 1 Anggaran
            // 2 // SPD
            // 3 -> SP2D
            // 4-> BAST
            // 5 ->Fungsional
            m_iSumber = iSumber;

        }
        private void ctrlProgramKegiatan_Load(object sender, EventArgs e)
        {

        }
        public void SetIDinas(int idDinas, int kodeUK)
        {
            m_idDinas = idDinas;
            m_iKodeUK = kodeUK;
            CreateUrusan();
            
        }

        public void CreateUrusan()
        {
            //switch (m_iSumber)
            //{
               // case 0:
                    ctrlUrusanPemerintahan1.Create(m_idDinas, (int)GlobalVar.TahunAnggaran);
            //        break;
            //    case 1:
            //        ctrlUrusanPemerintahan1.Create(m_idDinas, (int)GlobalVar.TahunAnggaran);
            //        break;
            //    case 2:
            //        ctrlUrusanPemerintahan1.CreateBasedSPD(m_idDinas, (int)GlobalVar.TahunAnggaran,m_inourutSPD);
            //        break;
            //    case 3:
            //        ctrlUrusanPemerintahan1.CreateBasedSP2D(m_idDinas, (int)GlobalVar.TahunAnggaran, m_NoUrutSPP);
            //        break;
            //    case 4:
            //        ctrlUrusanPemerintahan1.CreateBasedBAST(m_idDinas, (int)GlobalVar.TahunAnggaran, m_iNoUrutBAST);
                 
            //        break;
            //    case 5:
            //        ctrlUrusanPemerintahan1.CreateBasedSPJ(m_idDinas, (int)GlobalVar.TahunAnggaran, m_iNoUrutSPJ);
            //        break;
                    

            //}
            

        }
        private int GetDinas()
        {
            return m_idDinas;
        }
        private int GetUrusan()
        {
            return m_idUrusan;
        }
        private int GetProgran()
        {
            return m_idProgram;
        }
        private long  GetKegiatan()
        {
            return m_idKegiatan;
        }

        public int IDDinas {
           
            get{return m_idDinas ;}
        }

         public int IdUrusan {
            get{return m_idUrusan ;}
        }
        
        
        public  int IdProgram
        {
            get{
            return m_idProgram;
            }
        }

        public  int IdKegiatan 
        {
            get {
            return m_idKegiatan;
            }
        }
        public long IdSubKegiatan{
            get{
                return m_idSubKegiatan ;
            }
        }

        public void SetSPD(long inourut)
        {
            m_inourutSPD = inourut;
            ctrlUrusanPemerintahan1.CreateBasedSPD(m_idDinas, (int)GlobalVar.TahunAnggaran, inourut);

        }

        public void CreateFormBAST(long inourut)
        {
            m_iNoUrutBAST = inourut;
            ctrlUrusanPemerintahan1.CreateBasedSPD(m_idDinas, (int)GlobalVar.TahunAnggaran, inourut);

        }
        public void CreateFormSPJ(long inourut)
        {
            m_iNoUrutSPJ = inourut;
            ctrlUrusanPemerintahan1.CreateBasedSPJ(m_idDinas, (int)GlobalVar.TahunAnggaran, m_iNoUrutSPJ);

        }
        public void Create(int iddinas, int unitAnggaran )
        {
            m_idDinas = iddinas;
            m_iKodeUK = unitAnggaran;
            ctrlUrusanPemerintahan1.Create(m_idDinas, (int)GlobalVar.TahunAnggaran);
         }
        public bool CekPilihan()
        {
            if (m_idUrusan <=0 || m_idProgram <=0  || m_idKegiatan <=0  || m_idSubKegiatan<=0){
                MessageBox.Show("Pilihan Program/Kegiatan.SybKegiatan tidak benar..");
                return false ;
            }
            
            return true;



        }

        private void CreateProgram()
        {

            switch (m_iSumber)
            {
                case 1:
                    ctrlProgram1.Create((int)GlobalVar.TahunAnggaran, m_idDinas, m_idUrusan);
                    break;
                case 2:
                    ctrlProgram1.CreateBasedSPD((int)GlobalVar.TahunAnggaran, m_idDinas, m_idUrusan, m_inourutSPD);
                    break;
                case 3:
                    ctrlProgram1.CreateBasedSP2D((int)GlobalVar.TahunAnggaran, m_idDinas, m_idUrusan, m_NoUrutSPP);
                    
                   // ctrlUrusanPemerintahan1.CreateBasedSP2D(m_idDinas, (int)GlobalVar.TahunAnggaran, m_NoUrutSPP);
                    break;
                case 4:
                    ctrlProgram1.CreateBasedBAST((int)GlobalVar.TahunAnggaran, m_idDinas, m_idUrusan, m_iNoUrutBAST);
                    
                    //ctrlUrusanPemerintahan1.CreateBasedBAST(m_idDinas, (int)GlobalVar.TahunAnggaran, m_iNoUrutBAST);

                    break;
                case 5:
                    ctrlProgram1.CreateBasedSPJ((int)GlobalVar.TahunAnggaran, m_idDinas, m_idUrusan, m_iNoUrutSPJ);

                    //ctrlUrusanPemerintahan1.CreateBasedSPJ(m_idDinas, (int)GlobalVar.TahunAnggaran, m_iNoUrutSPJ);
                    break;
                 


            }           
    

        }
        private void CreateKegiatan()
        {

            switch (m_iSumber)
            {
                case 1:
                    ctrlKegiatanAPBD1.CreateWIthUK((int)GlobalVar.TahunAnggaran, m_idDinas,m_iKodeUK, m_idProgram);

                    break;

               case 2:
                    ctrlKegiatanAPBD1.CreateBasedSPD((int)GlobalVar.TahunAnggaran, m_idDinas, m_idUrusan, m_idProgram, m_inourutSPD);
                    //ctrlProgram1.CreateBasedSPD((int)GlobalVar.TahunAnggaran, m_idDinas, m_idUrusan, m_inourutSPD);
                    break;
                case 3:
                    ctrlKegiatanAPBD1.CreateBasedSP2D((int)GlobalVar.TahunAnggaran, m_idDinas, m_idUrusan, m_idProgram, m_NoUrutSPP);
         //          
                    break;
                case 4:
                    ctrlKegiatanAPBD1.CreateBasedBAST((int)GlobalVar.TahunAnggaran, m_idDinas, m_idUrusan, m_idProgram, m_iNoUrutBAST);
                   
                    break;
                case 5:
                    ctrlKegiatanAPBD1.CreateBasedSPJ((int)GlobalVar.TahunAnggaran, m_idDinas, m_idUrusan, m_idProgram, m_iNoUrutSPJ);
                
                    break;


            }
  

        }
        private void ctrlUrusanPemerintahan1_OnChanged(int pID)
        {
            m_idUrusan = pID;
            ctrlProgram1.Clear();
            CreateProgram();
        }

        private void ctrlProgram1_OnChanged(int pID)
        {
            m_idProgram = pID;
            ctrlKegiatanAPBD1.Clear();
            ctrlSubKegiatan1.Clear();
            CreateKegiatan();

        }
        public void SetValue(int idDinas, int kodeUK, int idUrusan, int idProgram, int idKegiatan, long idsubkegiatan)
        {
            m_idDinas = idDinas;
            m_idUrusan = idUrusan;
       
      
            m_iKodeUK = kodeUK;
          //ctrlUrusanPemerintahan1
         //   CreateUrusan();
            ctrlUrusanPemerintahan1.SetID(m_idUrusan);
            m_idProgram = idProgram;
            CreateProgram();
            m_idProgram = idProgram;
            ctrlProgram1.SetID(m_idProgram);
            CreateKegiatan();
            m_idKegiatan = idKegiatan;
            ctrlKegiatanAPBD1.SetID(m_idKegiatan);
            if (GlobalVar.TahunAnggaran >= 2021)
            {
                m_idSubKegiatan = idsubkegiatan;
                ctrlSubKegiatan1.CreateWithUK(GlobalVar.TahunAnggaran, m_idDinas, m_iKodeUK,m_idKegiatan);
                ctrlSubKegiatan1.SetID(idsubkegiatan);
            }
           
        }
        public void SetValueFromSP2D(int idDinas, int kodeUK, int idUrusan, int idProgram, int idKegiatan, long idsubkegiatan, long noUrutSPP)
        {
            m_idDinas = idDinas;
            m_idUrusan = idUrusan;


            m_iKodeUK = kodeUK;
            //ctrlUrusanPemerintahan1
            //   CreateUrusan();
            ctrlUrusanPemerintahan1.SetID(m_idUrusan);
            m_idProgram = idProgram;
            CreateProgram();
            m_idProgram = idProgram;
            ctrlProgram1.SetID(m_idProgram);
            CreateKegiatan();
            m_idKegiatan = idKegiatan;
            ctrlKegiatanAPBD1.SetID(m_idKegiatan);
            if (GlobalVar.TahunAnggaran >= 2021)
            {
                
                m_idSubKegiatan = idsubkegiatan;
                List<SPPRekening> lstSPPRekening = new List<SPPRekening>();
                SPPLogic oLogic = new SPPLogic(GlobalVar.TahunAnggaran);
                lstSPPRekening = oLogic.GetDetail(noUrutSPP);

                ctrlSubKegiatan1.CreateWithUK(GlobalVar.TahunAnggaran, m_idDinas, m_iKodeUK, m_idKegiatan,lstSPPRekening);
                ctrlSubKegiatan1.SetID(idsubkegiatan);
            }

        }
        public List<RekeningDetail> GetSPDRekening(DateTime dBatas)
        {
            List<RekeningDetail> lRet = new List<RekeningDetail>();

            lRet = ctrlKegiatanAPBD1.GetSPDRekening(dBatas);

            return lRet;


        }

        private void ctrlKegiatanAPBD1_OnChanged(long pID)
        {
           // m_idKegiatan = pID;
            ctrlSubKegiatan1.Clear();
            if (OnChanged != null)
            {
                OnChanged(m_idUrusan, m_idProgram, m_idKegiatan, 0);

            }
        }

        private void ctrlKegiatanAPBD1_OnChanged(int pID)
        {
            m_idKegiatan = pID;
            ctrlSubKegiatan1.Clear();
            if (GlobalVar.TahunAnggaran <= 2020) {
                if (OnChanged != null)
                    {
                        OnChanged(m_idUrusan, m_idProgram, m_idKegiatan, 0);

                    }
                }  else {
                    ctrlSubKegiatan1.CreateWithUK(GlobalVar.TahunAnggaran, m_idDinas,m_iKodeUK,m_idKegiatan);

            }
        }

        private void ctrlKegiatanAPBD1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlSubKegiatan1_OnChanged(long pID)
        {
            m_idSubKegiatan = pID;
            if (GlobalVar.TahunAnggaran > 2020)
            {
                if (OnChanged != null)
                {
                    OnChanged(m_idUrusan, m_idProgram, m_idKegiatan, m_idSubKegiatan);

                }
            }
         
        }

        private void ctrlUrusanPemerintahan1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlProgramKegiatan_Resize(object sender, EventArgs e)
        {
            ctrlUrusanPemerintahan1.Width = this.Width;
            ctrlProgram1.Width = this.Width;
            ctrlKegiatanAPBD1.Width = this.Width;
            ctrlSubKegiatan1.Width = this.Width;
        }
        
    }
}
