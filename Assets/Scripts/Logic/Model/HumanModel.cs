using System.Collections.Generic;

namespace Logic
{
    public class HumanModel : BaseModel<HumanModel>
    {
        private Dictionary<long, Human.Human> m_humanDic;

        public Human.Human GetHuman(long id)
        {
            if (!m_humanDic.TryGetValue(id, out var human))
            {
                human = new Human.Human(id);
                m_humanDic[id] = human;
            }

            return human;
        }
    }
}