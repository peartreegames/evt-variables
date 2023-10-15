using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace PeartreeGames.Evt.Variables.Editor
{
    public class EvtDatabaseTabVariable : EvtDatabaseTab<EvtVariable>
    {
        public override string Name => "Variables";

        public override VisualElement CreatePage()
        {
            Page ??= EvtDatabase.CreatePage<EvtVariable>(null, OnSelectionIndexChange);
            return Page;
        }

        private void OnSelectionIndexChange(IEnumerable<int> indices)
        {
            EvtDatabase.OnListSelectionChange<EvtVariable>(indices);
            var toolbar = Page.Q<Toolbar>("ItemToolbar");
            var scene = new ToolbarButton(() => EvtDatabaseUtilities.SceneSearch<EvtVariable>(Page)) {text = "Find In Scene"};
            var project = new ToolbarButton(() => EvtDatabaseUtilities.ProjectPing<EvtVariable>(Page)) {text = "Find In Project"};
            toolbar.Insert(1, scene);
            toolbar.Insert(2, project);
        }

        
    }
}