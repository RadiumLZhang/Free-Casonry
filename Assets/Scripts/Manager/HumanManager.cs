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
        public Human GetHuman(long id)
        {
            if (humanMap.TryGetValue(id, out var human))
            {
                human = LoadHuman(id);
                humanMap[id] = human;
            }
            return human;
        }
        
        /************************************** 实现 ******************************************/
        
        private Dictionary<long, Human> humanMap;

        public HumanManager()
        {
            humanMap = new Dictionary<long, Human>();
        }

        private Human LoadHuman(long id)
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