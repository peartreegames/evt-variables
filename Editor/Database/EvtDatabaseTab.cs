using UnityEngine;
using UnityEngine.UIElements;

namespace PeartreeGames.Evt.Variables.Editor
{
    public abstract class EvtDatabaseTab<T> : IEvtDatabaseTab where T : Object 
    {
        public abstract string Name { get; }
        protected VisualElement Page;

        public virtual VisualElement CreatePage()
        {
            Page ??= EvtDatabase.CreatePage<T>();
            return Page;
        }
    }
}