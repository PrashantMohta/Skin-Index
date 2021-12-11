using System.IO;
using Newtonsoft.Json;

namespace SyncSong{
    class BaseSkin{
        public byte id;
        public string name;
        public string knight;
        public string knightProxy;

    }
    class Skin : BaseSkin{

        [JsonIgnore]
        public string localPath;

        public Skin(byte id,string name,string knight,string knightProxy,string localPath){
            this.id = id;
            this.name = name;
            this.knight = knight;
            this.knightProxy = knightProxy;
            this.localPath = localPath;
        }
        [JsonConstructor]
        public Skin(byte id,string name,string knight,string knightProxy){
            this.id = id;
            this.name = name;
            this.knight = knight;
            this.knightProxy = knightProxy;
        }
        public Skin(Skin globalSkin,string localPath){
            this.id = globalSkin.id;
            this.name = globalSkin.name;
            this.knight = globalSkin.knight;
            this.knightProxy = globalSkin.knightProxy;
            this.localPath = localPath;
        }


    }

}