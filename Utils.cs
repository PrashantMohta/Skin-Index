using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using Newtonsoft.Json;

namespace SkinIndex{
    static class Utils{
        //pngquant --quality 4-64 --ext .png -f **/*.png
        public static int SkinSort(Skin x,Skin y){
            if(x == null || y == null){ return 0;}
            return x.id - y.id;
        }
            
        public static void serialiseList(string path,List<Skin> skins){
            var Json = JsonConvert.SerializeObject(skins, Formatting.Indented);
            File.WriteAllText(path,Json);
        }

        public static List<Skin> deSerialiseList(string path){
            var Json = File.ReadAllText(path);
            List<Skin> skins = JsonConvert.DeserializeObject<List<Skin>>(Json);
            return skins;
        }
        public static void ClearId(string path){
            if(path==null){return;}
            var idPath = Path.Combine(path,"id.txt");
            if(File.Exists(idPath)){
                File.Delete(idPath);
            }
        }

        public static void WriteId(string path,byte id){
            if(path==null || id==null){ return; }
            File.WriteAllText(Path.Combine(path,"id.txt"),id.ToString());
        }

        public static void ClearId(this Skin skin){
            if(skin==null) {return;}
            ClearId(skin.localPath);
        }
        public static void WriteId(this Skin skin){
            if(skin==null){ return; }
            WriteId(skin.localPath,skin.id);
        }

        public static string getHash(this byte[] data){ 
            var sha1 = SHA1.Create();

            byte[] hashBytes = sha1.ComputeHash(data);

            string hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

            return hash;
        }


       public static void CopySkins(string src,string dest,string skip){
           var directories = Directory.GetDirectories(src);
           foreach(var d in directories){
               var dirName = new DirectoryInfo(d).Name;
               var destDirectory = Path.Combine(dest,dirName);
               var skipDirectory = Path.Combine(skip,dirName);

               var knightPath = Path.Combine(d,"Knight.png");
               var destKnightPath = Path.Combine(destDirectory,"Knight.png");

               var sprintPath = Path.Combine(d,"Sprint.png");
               var destSprintPath = Path.Combine(destDirectory,"Sprint.png");

               if(Directory.Exists(skipDirectory) || Directory.Exists(destDirectory)){
                   continue;
               }
               
               if(!Directory.Exists(destDirectory)){
                   Directory.CreateDirectory(destDirectory);
               }
               if(File.Exists(knightPath) && !File.Exists(destKnightPath)){
                   File.Copy(knightPath,destKnightPath);
               }

               if(File.Exists(sprintPath) && !File.Exists(destSprintPath) ){
                   File.Copy(sprintPath,destSprintPath);
               }
           }
       }

    }
}