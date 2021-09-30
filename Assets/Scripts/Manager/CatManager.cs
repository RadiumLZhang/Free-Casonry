using System.Collections.Generic;
using Logic;
using Newtonsoft.Json;

namespace Manager
{
    public class CatManager : BaseModel<CatManager>, ISaveObject
    {
        /********************************* 接口 ***********************************************/

        /**
         * 根据id获取猫猫
         */
        public Cat GetCat(long id)
        {
            if (!catMap.TryGetValue(id, out var cat))
            {
                cat = LoadCat(id);
                catMap[id] = cat;
            }
            return cat;
        }
        
        
        
        /************************************** 实现 ******************************************/

        private Dictionary<long, Cat> catMap;

        public CatManager()
        {
            if (catMap == null)
            {
                catMap = new Dictionary<long, Cat>();
            }
        }

        private Cat LoadCat(long id)
        {
            Cat cat = new Cat(id);
            if (cat.Config != null)
            {
                catMap[id] = cat;
                return cat;
            }
            return null;
        }

        public string Save()
        {
            var jsonString = JsonConvert.SerializeObject(catMap);
            return jsonString;
        }

        public void Load(string json)
        {
            catMap = JsonConvert.DeserializeObject<Dictionary<long, Cat>>(json);
        }
    }
}