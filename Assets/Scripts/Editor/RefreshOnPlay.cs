using UnityEditor;

namespace Editor
{
    [InitializeOnLoad]
    public class RefreshOnPlay
    {
        static RefreshOnPlay()
        {
            EditorApplication.playModeStateChanged += OnRefreshOnPlay;
        }

        private static void OnRefreshOnPlay(PlayModeStateChange obj)
        {
            if (obj == PlayModeStateChange.ExitingEditMode)
            {
                //运行前更新代码和资源
                AssetDatabase.Refresh();
            }
        }
    }
}