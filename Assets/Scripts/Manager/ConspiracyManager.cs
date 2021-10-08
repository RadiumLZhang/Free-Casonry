using System.Collections.Generic;
using Logic;
using Logic.Conspiracy;

namespace Manager
{
    public class ConspiracyManager : BaseModel<ConspiracyManager>
    {
        /********************************* 接口 ***********************************************/
        
        /**
         * 根据id获取阴谋
         */
        public Conspiracy GetConspiracy(long id)
        {
            if (!consMap.TryGetValue(id, out var conspiracy))
            {
                conspiracy = LoadConspiracy(id);
                consMap[id] = conspiracy;
            }
            return conspiracy;
        }
        
        /********************************* 实现 ***********************************************/
        private Dictionary<long, Conspiracy> consMap;
        
        public ConspiracyManager()
        {
            if (consMap == null)
            {
                consMap = new Dictionary<long, Conspiracy>();
            }
        }
        private Conspiracy LoadConspiracy(long id)
        {
            Conspiracy conspiracy = new Conspiracy(id);
            if (conspiracy.Config != null)
            {
                consMap[id] = conspiracy;
                return conspiracy;
            }
            return null;
        }
    }
}