using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace PeartreeGames.Evt.Variables.Editor
{
    public class EvtVariableReset : IPreprocessBuildWithReport
    {
        [InitializeOnLoadMethod]
        private static void RegisterResets()
        {
            EditorApplication.playModeStateChanged += PlayModeChanged; 
        }

        private static void PlayModeChanged(PlayModeStateChange playModeStateChange)
        {
            if (playModeStateChange != PlayModeStateChange.ExitingPlayMode) return;
            ResetVariables();
        }

        private static void ResetVariables()
        {
            var guids = AssetDatabase.FindAssets("t:EvtVariable");
            foreach (var guid in guids)
            {
                var asset = AssetDatabase.LoadAssetAtPath<EvtVariable>(AssetDatabase.GUIDToAssetPath(guid));
                if (asset != null) asset.Reset();
            }
        }

        public int callbackOrder { get; }
        public void OnPreprocessBuild(BuildReport report) => ResetVariables();
    }
}