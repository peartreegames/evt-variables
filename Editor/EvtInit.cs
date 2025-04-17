using System.Linq;
using UnityEditor;
using UnityEngine;

namespace PeartreeGames.Evt.Variables.Editor
{
    public static class EvtInit
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Init()
        {
            var variables = AssetDatabase.FindAssets("t:EvtVariable")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<EvtVariable>);
            foreach (var variable in variables) variable.Reset();
        }
    }
}