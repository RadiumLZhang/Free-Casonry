using System.Collections.Generic;
using Logic;

namespace Manager
{
    public class CatManager : BaseModel<CatManager>
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
            catMap = new Dictionary<long, Cat>();
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
    }
}