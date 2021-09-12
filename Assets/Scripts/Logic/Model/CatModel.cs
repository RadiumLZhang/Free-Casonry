using System.Collections.Generic;

namespace Logic
{
    public class CatModel : BaseModel<CatModel>
    {
        private Dictionary<long, Cat> m_catDic;

        public Cat GetCatByID(long id)
        {
            if(!m_catDic.TryGetValue(id, out var cat))
            {
                cat = new Cat(id);
                m_catDic[id] = cat;
            }

            return cat;
        }
    }
}