using System.Collections.Generic;
using Logic;
using Logic.Event;

namespace Manager
{
    public class EmergencyManager : BaseModel<EmergencyManager>
    {
        /********************************* 接口 ***********************************************/

        public Emergency GetEmergencyByID(long id)
        {
            if (!map.TryGetValue(id, out var emergency))
            {
                emergency = new Emergency(id);
                map[id] = emergency;
            }

            return emergency;
        }
        
        /********************************* 实现 ***********************************************/

        private Dictionary<long, Emergency> map = new Dictionary<long, Emergency>();
        
    }
}