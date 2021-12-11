using System;
using System.IO;
using System.Collections;
using System.Reflection;
using GlobalEnums;
using Modding;
using UnityEngine;

namespace SyncSong
{
    
    public class SyncSong : Mod
    {

        internal static SyncSong Instance;
        string MODS_DIR;
        string HKMP_SKINS = Path.Combine("HKMP","Skins");
        string HKMP_DUPES = Path.Combine("HKMP","Skins_dupes");

        string CK_SKINS = "CustomKnight";

        public static string HKMP_SKINS_PATH;
        public static string HKMP_DUPES_PATH;

        public static string CK_SKINS_PATH;

        public SyncSong(){
            Instance = this;

            //do everything here to avoid HKMP doing things before we do
            SkinDB.loadJsonFromAssembly();
            initDirectories();
            Skins.GenerateDictionary();
            Skins.GenerateLocalSkinsMap();
        }
        public override string GetVersion()
        {
            return "1.0";
        }

        public void initDirectories(){
            switch (SystemInfo.operatingSystemFamily)
            {
                case OperatingSystemFamily.MacOSX:
                    MODS_DIR = Path.GetFullPath(Application.dataPath + "/Resources/Data/Managed/Mods");
                    break;
                default:
                    MODS_DIR = Path.GetFullPath(Application.dataPath + "/Managed/Mods");
                    break;
            }
            HKMP_SKINS_PATH = Path.Combine(MODS_DIR,HKMP_SKINS);
            HKMP_DUPES_PATH = Path.Combine(MODS_DIR,HKMP_DUPES);

            CK_SKINS_PATH = Path.Combine(MODS_DIR,CK_SKINS);

            if (!Directory.Exists(HKMP_SKINS_PATH))
            {
                Directory.CreateDirectory(HKMP_SKINS_PATH);
                Log("created");
            }

            if (Directory.Exists(CK_SKINS_PATH) && Directory.GetDirectories(CK_SKINS_PATH).Length > 0)
            {
                Utils.CopySkins(CK_SKINS_PATH,HKMP_SKINS_PATH,HKMP_DUPES_PATH);
                Log("copied");
            }
        }

        public override void Initialize()
        {

            ModHooks.Instance.HeroUpdateHook += update;
        }


        public void update()
        {
            
        }

    }

}
