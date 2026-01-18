using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;
using System.Collections.Generic;

namespace UnityToolbarExtender
{
    /// <summary>
    /// Adapter class for migration from old toolbar API to new Unity 6.3+ API
    /// This helps maintain backward compatibility while transitioning
    /// </summary>
    [InitializeOnLoad]
    public static class ToolbarMigrationAdapter
    {
        static ToolbarMigrationAdapter()
        {
            EditorApplication.update += OnEditorUpdate;
        }

        private static void OnEditorUpdate()
        {
            // Legacy support - if old toolbar elements are still being used
            // This space can be used for any migration logic needed
        }

        /// <summary>
        /// Helper to convert old GUILayout toolbar elements to new UIElements
        /// </summary>
        public static string GetMainToolbarElementID(string group, string name)
        {
            return $"{group}/{name}";
        }

        /// <summary>
        /// Get default dock position for toolbar elements
        /// </summary>
        public static MainToolbarDockPosition GetDefaultPosition(bool rightSide = false)
        {
            return rightSide ? MainToolbarDockPosition.Right : MainToolbarDockPosition.Middle;
        }
    }
}
