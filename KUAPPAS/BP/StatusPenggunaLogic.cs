using KUAPPAS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP
{
    public class StatusPenggunaLogic
    {
        private List<ListItemData> m_lstStatus;

        public List<ListItemData> GetListStatus()
        {
            m_lstStatus = new List<ListItemData>();
            m_lstStatus.Clear();
            m_lstStatus.Add(new ListItemData("Pengguna Baru", 0));
            m_lstStatus.Add(new ListItemData("AKtif", 1));
            m_lstStatus.Add(new ListItemData("Aktife Tapi Tidak bisa Operasi (Hanya melihat)", 5));
            m_lstStatus.Add(new ListItemData("Tidak Aktiv", 9));
            return m_lstStatus;

        }
    }
}
