/*****************************************************************
 * Copyright (C) Knights Warrior Corporation. All rights reserved.
 * 
 * Author:   圣殿骑士（Knights Warrior） 
 * Email:    KnightsWarrior@msn.com
 * Website:  http://www.cnblogs.com/KnightsWarrior/       http://knightswarrior.blog.51cto.com/
 * Create Date:  5/8/2010 
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnightsWarriorAutoupdater
{
    public class ConstFile
    {
        public const string TEMPFOLDERNAME = "TempFolder";
        public const string CONFIGFILEKEY = "config_";
        public const string FILENAME = "AutoUpdater.config";
        public const string ROOLBACKFILE = "KUAPPAS.exe";
        public const string MESSAGETITLE = "AutoUpdate Program";
        public const string CANCELORNOT = "KUA PPAS Sedang update aplikasi. Apa akan membatalkan?";
        public const string APPLYTHEUPDATE = "Program harus distart ulang. Klik OK untuk start/memulai ulang!";
        public const string NOTNETWORK = "Update KUAPPAS gagal. KUAPPAS akanrestart. Silakan coba lagi untuk update.";
    }
}
