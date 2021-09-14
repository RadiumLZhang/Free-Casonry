using ResultEventInfo;
using UnityEngine;

namespace Logic.Event
{
    public class Result
    {
        public long ID { get; private set; }
        
        public ResultEventInfo.ResultEventInfo.Types.ResultEventItem Config { get; private set; }

        public string Name => Config?.Name;

        public string Description1 => Config?.Description1;

        public string Description2 => Config?.Description2;

        public string BtnTxt => Config?.BtnText;

        public long RecordId => Config?.RecordId ?? 0;

        public Result(long id)
        {
            ID = id;
            Config = ResultEventInfoLoader.Instance.FindResultEventItem(id);
            if (Config == null)
            {
                Debug.LogError($"Invalid ResultId");
            }
        }
        
        public void Execute()
        {
            //todo 效果生效
            // Config.Effects;
        }
    }
}