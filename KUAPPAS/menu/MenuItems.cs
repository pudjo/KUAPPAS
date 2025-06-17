using System.Collections.Generic;
using Z80NavBarControl.Z80NavBar;

namespace Menu
{
    public class MenuItems
    {

        
        
        public List<NavBarItem> daftarMenu;

        public MenuItems()
        {



            daftarMenu = new List<NavBarItem> {
                new NavBarItem { ID = 1, Text = "Main Menu", Icon = new ItemIcon { Default = KUAPPAS.Properties.Resources.nav_home, Hover = KUAPPAS.Properties.Resources.nav_home, Selected = KUAPPAS.Properties.Resources.nav_home }, ToolTip = "tooltip Main Menu", Height = 40 },

                new NavBarItem { ID = 2, Text = "Master", Icon = new ItemIcon { Default = KUAPPAS.Properties.Resources.nav_settings, Hover = KUAPPAS.Properties.Resources.nav_settings, Selected = KUAPPAS.Properties.Resources.nav_settings }, Height = 40,
                    Childs = new List<NavBarItem> {
                        new NavBarItem { ID = 2001, ParentID = 2, Text = "Pemda", Height = 30 },
                        new NavBarItem { ID = 2002, ParentID = 2, Text = "SKPD", Height = 30 },
                        new NavBarItem { ID = 2003, ParentID = 2, Text = "Unit Kerja", Height = 30 },
                        new NavBarItem { ID = 2004, ParentID = 2, Text = "MKode Rekening", Height = 30 },
                        new NavBarItem { ID = 2005, ParentID = 2, Text = "Pejabat ", Height = 30 },

                    }
                },

                new NavBarItem { ID = 3, Text = "Anggaran", Icon = new ItemIcon { Default = KUAPPAS.Properties.Resources.nav_bulb, Hover = KUAPPAS.Properties.Resources.nav_bulb, Selected = KUAPPAS.Properties.Resources.nav_bulb }, Height = 40,
                    Childs = new List<NavBarItem> {
                        new NavBarItem { ID = 3009, ParentID = 3, Text = "SAet Tahap Input", Height = 30, },
                        new NavBarItem { ID = 3010, ParentID = 3, Text = "Import SUPD", Height = 30, },
                        new NavBarItem { ID = 3020, ParentID = 3, Text = "Inut Anggaran", Height = 30, },
                        new NavBarItem { ID = 3030, ParentID = 3, Text = "Anggaran Kas", Height = 30, },
                        new NavBarItem { ID = 3040, ParentID = 3, Text = "Rekap Anggaran Kas", Height = 30, },
                    },
                },
                new NavBarItem { ID = 4, Text = "SPD", Icon = new ItemIcon { Default = KUAPPAS.Properties.Resources.nav_new, Hover = KUAPPAS.Properties.Resources.nav_new, Selected = KUAPPAS.Properties.Resources.nav_new }, Height = 40,
                    Childs = new List<NavBarItem> {
                        new NavBarItem { ID = 4010, ParentID = 4, Text = "Pembuatan SPD", Height = 30, },
                        new NavBarItem { ID = 4011, ParentID = 4, Text = "Register SPD", Height = 30, },

                    },
                },
                new NavBarItem { ID =5, Text = "Penata Usahaan", Icon = new ItemIcon { Default = KUAPPAS.Properties.Resources.nav_new, Hover = KUAPPAS.Properties.Resources.nav_new, Selected = KUAPPAS.Properties.Resources.nav_new }, Height = 40,
                    Childs = new List<NavBarItem> {
                        new NavBarItem { ID = 51, ParentID = 4, Text = "Pengeluaran", Height = 30,
                        Childs = new List<NavBarItem> {
                             new NavBarItem { ID = 5101, ParentID = 51, Text = "Sald Awal Bendahara", Height = 30, },
                             new NavBarItem { ID = 5102, ParentID = 51, Text = "Setiing", Height = 30, },
                             new NavBarItem { ID = 5103, ParentID = 51, Text = "Proyek", Height = 30, },
                             new NavBarItem { ID = 5104, ParentID = 51, Text = "BAST", Height = 30, },
                             new NavBarItem { ID = 5105, ParentID = 51, Text = "SPP", Height = 30, },
                             new NavBarItem { ID = 5106, ParentID = 51, Text = "Penctatan BK SP2D", Height = 30, },
                            new NavBarItem { ID = 5107, ParentID = 51, Text = "Pencairan Bank", Height = 30, },
                            new NavBarItem { ID = 5108, ParentID = 51, Text = "SPj", Height = 30, },
                             new NavBarItem { ID = 5109, ParentID = 51, Text = "Pembayaran Pajak", Height = 30, },
                             new NavBarItem { ID = 5110, ParentID = 51, Text = "Pengembalian Belanja", Height = 30, },
                             new NavBarItem { ID = 5112, ParentID = 51, Text = "Koreksi", Height = 30, },

                            }, //pengeluaran
                        }
                    }
                  },
            };
        }
    }
}
