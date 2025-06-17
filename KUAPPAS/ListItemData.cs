using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KUAPPAS
{
    public class ListItemData
    {
        public string ItemText;
        public int Itemdata;
        public long lItemData;
        public string Kode;
        public string Kodetambahan;
        public Object something;

        public ListItemData(string sText, int itemID,Object o =null)
        {
            ItemText = sText;
            Itemdata = itemID;
            something = new Object();
        }
        public ListItemData(string sText, string itemID, string  kodetambahan)
        {
            ItemText = sText;
            Kode = itemID;
            Kodetambahan = kodetambahan;
        }
        public ListItemData(string sText, string itemID)
        {
            ItemText = sText;
            Kode = itemID;
           
        }

        public ListItemData(string sText, long itemID,Object o =null)
        {
            ItemText = sText;
            lItemData = itemID;
            something = o;
        }
     
        public override string ToString()
        {
            return ItemText;
        }

        public override bool Equals(object obj)
        {
            //Type t = typeof(T);
            if (typeof(object) == typeof(int) || typeof(object) == typeof(long))
            {
                return Convert.ToBoolean(Convert.ToInt32(obj) == Itemdata) || Convert.ToBoolean(Convert.ToInt64(obj) == lItemData);
            }
            else
            {
                return false;
            }
        }
        
        //public override bool Equals(object obj)
        //{
        //    Type t = typeof(T);
        //    if (typeof(object) == t)
        //    {

        //        return Convert.ToBoolean((T)obj== Itemdata);
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
