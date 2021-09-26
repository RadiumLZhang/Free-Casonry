using System.Collections.Generic;
using Logic;
using Logic.Human;

namespace Manager
{
    public class HumanManager : BaseModel<HumanManager>
    {
        /********************************* 接口 ***********************************************/
        
        /**
         * 根据id获取人
         */
        public Human GetHuman(int id)
        {
            Human human = humanMap[id];
            if (human == null)
            {
                human = LoadHuman(id);
                humanMap[id] = human;
            }
            return human;
        }
        
        /************************************** 实现 ******************************************/
        
        private Dictionary<int, Human> humanMap;

        public HumanManager()
        {
            humanMap = new Dictionary<int, Human>();
        }

        private Human LoadHuman(int id)
        {
            Human human = new Human(id);
            if (human.Config != null)
            {
                humanMap[id] = human;
                return human;
            }
            return null;
        }
    }
}