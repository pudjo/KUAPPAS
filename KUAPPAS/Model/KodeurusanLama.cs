using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Formatting;
namespace DataAccess9.DTO
{
    public class KodeurusanLama
    {
        public int KOdekategoriPelaksana { set; get; }
        public int KodeUrusanPelaksana { set; get; }

        public KodeurusanLama(int idUrusan)
        {
            KOdekategoriPelaksana = DataFormat.GetInteger(idUrusan.ToString().Substring(0, 1));
            KodeUrusanPelaksana = DataFormat.GetInteger(idUrusan.ToString().Substring(1, 2));
           
        }
    }
}
