using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using DTO;
using DataAccess;
using BP;

namespace BP
{
    public class KUAHistoryLogic:BP 
    {
        public KUAHistoryLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
        }
        public bool Simpan(KUAHistory _kuaHistory)
        {
            try
            {
                SSQL = "INSERT INTO tKUAHistory (IDKUA,Action,Value,UserID,Komputer,Jam,Menit,Tanggal,Bulan,Tahun,Jumlah)values (" +
                    "@pIDKUA,@pAction,@pValue,@pUserID,@pKomputer,@pJam,@pMenit,@pTanggal,@pBulan,@pTahun,@pJumlah)";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pIDKUA", _kuaHistory.IDKUA));
                paramCollection.Add(new DBParameter("@pAction",_kuaHistory.Action));
                paramCollection.Add(new DBParameter("@pValue",_kuaHistory.Value));
                paramCollection.Add(new DBParameter("@pUserID",_kuaHistory.UserID));
                paramCollection.Add(new DBParameter("@pTanggal",DateTime.Now.Date.Day));
                paramCollection.Add(new DBParameter("@pBulan", DateTime.Now.Date.Month));
                paramCollection.Add(new DBParameter("@pTahun", DateTime.Now.Date.Year));
                paramCollection.Add(new DBParameter("@pKomputer",""));
                paramCollection.Add(new DBParameter("@pJam",_kuaHistory.Jam));
                paramCollection.Add(new DBParameter("@pMenit", _kuaHistory.Menit));
                paramCollection.Add(new DBParameter("@pJumlah", _kuaHistory.Jumlah));

                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                return true;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }
            

        }
    }
}
