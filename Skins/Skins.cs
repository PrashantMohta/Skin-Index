using System.Collections.Generic;
using System.IO;
using System;

namespace SyncSong{
    class Skins{
       public static Dictionary<string,Skin> GlobalMap = new Dictionary<string,Skin>();
       public static Dictionary<string,Skin> GlobalProxyMap = new Dictionary<string,Skin>();

       public static Dictionary<string,Skin> localMap = new Dictionary<string,Skin>();
       
       public static List<Skin> localDb = new List<Skin>();
       
       public static List<string> localSkinPaths = new List<string>();
       public static List<string> localSkinHashes = new List<string>();
       public static List<string> unknownSkins = new List<string>();
       
       public static void GenerateDictionary(){
           foreach(Skin skin in SkinDB.skins){
               GlobalMap[skin.knight] = skin;
               if(skin.knightProxy != null){
                    GlobalProxyMap[skin.knightProxy] = skin;
               }
           }
       }

       public static void processSkin(string path){
           var knightPath = Path.Combine(path,"Knight.png");
           if(!File.Exists(knightPath)){ 
               Utils.ClearId(path);
               return;
           }
           localSkinPaths.Add(path);
           var hash = File.ReadAllBytes(knightPath).getHash();
           localSkinHashes.Add(hash);
            if(GlobalMap.TryGetValue(hash , out Skin globalSkin)){
                localMap[hash] = new Skin(globalSkin,path);
            } else {
                unknownSkins.Add(hash);
                var name = new DirectoryInfo(path).Name;
                // make this 4 => 100
                localMap[hash] = new Skin((byte)Convert.ToByte(100 + unknownSkins.Count),name,hash,"",path);
                // potentially give user an option to submit these hashes so that our DB can be updated
            }
            localMap[hash].ClearId();
       }

       public static void processProxies(string path){
           var knightPath = Path.Combine(path,"Knight.png");
           if(!File.Exists(knightPath)){ 
               Utils.ClearId(path);
               return;
           }
           var hash = File.ReadAllBytes(knightPath).getHash();
           
            if(GlobalProxyMap.TryGetValue(hash , out Skin globalSkin)){
                if(!localMap.TryGetValue(globalSkin.knight , out Skin localSkin)){
                    localMap[hash] = new Skin(globalSkin,path);
                }
            }
       }
       public static void GenerateLocalSkinsMap(){
           string[] Paths = Directory.GetDirectories(SyncSong.HKMP_SKINS_PATH);
           foreach(string path in Paths){
                processSkin(path);
           }
           foreach(string path in Paths){
               processProxies(path);
           }
           foreach(KeyValuePair<string,Skin> pair in localMap){
                pair.Value.WriteId();
                localDb.Add(pair.Value);
           }
           for(int i=0;i<localSkinPaths.Count;i++){
               if(!File.Exists(Path.Combine(localSkinPaths[i],"id.txt"))){ 
                    var Name = new DirectoryInfo(localSkinPaths[i]).Name;
                    if(!Directory.Exists(SyncSong.HKMP_DUPES_PATH)){
                        Directory.CreateDirectory(SyncSong.HKMP_DUPES_PATH);
                    }
                    Directory.Move(localSkinPaths[i],Path.Combine(SyncSong.HKMP_DUPES_PATH,Name));
                }
           }
           Utils.serialiseList(Path.Combine(SyncSong.HKMP_SKINS_PATH,"localmap.json"),localDb);
       }
    }
}