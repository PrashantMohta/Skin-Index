using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Cache;
using Newtonsoft.Json;

namespace SkinIndex{
    class SkinDB{
        public static string skinDB = "https://cdn.jsdelivr.net/gh/PrashantMohta/Skin-Index@main1.5/res/skinmap.json";

        public static List<Skin> skins = new List<Skin>{};

        public static void loadJson(){
            try{
                loadJsonFromGithub();
            }catch(Exception e){
                SkinIndex.Instance.Log(e.ToString());
                SkinIndex.Instance.Log("falling back to the skin db in assembly");
                loadJsonFromAssembly();
            }
        }
        public static void loadJsonFromGithub(){
            var wc = new WebClient
            {
                CachePolicy = new RequestCachePolicy(RequestCacheLevel.Revalidate)
            };
            string jsonString = wc.DownloadString(skinDB);
            skins = JsonConvert.DeserializeObject<List<Skin>>(jsonString);
        }
        public static void loadJsonFromAssembly(){
            Assembly asm = Assembly.GetExecutingAssembly();
            foreach (string res in asm.GetManifestResourceNames())
            {   
                if(!res.EndsWith("skinmap.json")) {
                    continue;
                } 

                using (Stream s = asm.GetManifestResourceStream(res))
                using (StreamReader reader = new StreamReader(s))
                {
                        string json = reader.ReadToEnd();
                        skins = JsonConvert.DeserializeObject<List<Skin>>(json);
                }
            }
        }
    }
}