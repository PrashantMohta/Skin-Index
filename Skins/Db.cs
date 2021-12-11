using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SkinIndex{
    class SkinDB{
        public static List<Skin> skins = new List<Skin>{};
        
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