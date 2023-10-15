using UnityEngine.UIElements;

namespace PeartreeGames.Evt.Variables.Editor
{
    public interface IEvtDatabaseTab
    {
         string Name { get; }
         VisualElement CreatePage();
    }
}