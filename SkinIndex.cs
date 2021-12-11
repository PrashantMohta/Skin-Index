using System;
using System.IO;
using System.Collections;
using System.Reflection;
using GlobalEnums;
using Modding;
using UnityEngine;

namespace SkinIndex
{
    
    public class SkinIndex : Mod
    {

        internal static SkinIndex Instance;
        string MODS_DIR;
        string HKMP_SKINS = Path.Combine("HKMP","Skins");
        string HKMP_DUPES = Path.Combine("HKMP","Skins_dupes");

        public static string HKMP_SKINS_PATH;
        public static string HKMP_DUPES_PATH;

        public override int LoadPriority() => 0;

        public SkinIndex(){
            Instance = this;

            //do everything here to avoid HKMP doing things before we do
            SkinDB.loadJsonFromAssembly();
            initDirectories();
            Skins.GenerateDictionary();
            Skins.GenerateLocalSkinsMap();
        }
        public override string GetVersion()
        {
            return "0.1-68"; // version + last skin id
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


            if (!Directory.Exists(HKMP_SKINS_PATH))
            {
                Directory.CreateDirectory(HKMP_SKINS_PATH);
                Log("created");
            }
            
        }

        public override void Initialize()
        {

        }

    }

}
