using UnityEditor;
using UnityEditor.Build;

namespace PeartreeGames.Evt.Variables.Editor
{
    [InitializeOnLoad]
    public class AddDefine
    {
        private const string DefineSymbol = "EVT_VARIABLES";

        static AddDefine()
        {
            // Iterate through all build target groups
            var targetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            var defines = PlayerSettings.GetScriptingDefineSymbols(NamedBuildTarget.FromBuildTargetGroup(targetGroup));
            if (defines.Contains(DefineSymbol)) return;
            
            PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.FromBuildTargetGroup(targetGroup), defines + ";" + DefineSymbol);
        }
    }
}
