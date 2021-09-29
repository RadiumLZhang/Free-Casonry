using System.Collections.Generic;
using HumanInfo;
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
            return humanMap[id];
        }
        
        /************************************** 实现 ******************************************/
        
        private Dictionary<long, Human> humanMap;

        public void Init()
        {
            foreach (var person in HumanInfoLoader.Instance.People)
            {
                humanMap[person.HumanId] = new Human(person.HumanId);
            }
        }

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