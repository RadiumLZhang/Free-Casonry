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
        public Cat GetCat(int id)
        {
            Cat cat = catMap[id];
            if (cat == null)
            {
                cat = LoadCat(id);
                catMap[id] = cat;
            }
            return cat;
        }
        
        
        
        /************************************** 实现 ******************************************/

        private Dictionary<int, Cat> catMap;

        public CatManager()
        {
            catMap = new Dictionary<int, Cat>();
        }

        private Cat LoadCat(int id)
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