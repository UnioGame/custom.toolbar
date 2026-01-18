using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityToolbarExtender
{
    /// <summary>
    /// Helper class for working with the new Unity 6.3+ Main Toolbar API
    /// </summary>
    public static class CustomToolbarAPI
    {
        /// <summary>
        /// Style a toolbar element by its name using UIElements
        /// </summary>
        public static void StyleElement<T>(string elementName, System.Action<T> styleAction) where T : VisualElement
        {
            EditorApplication.delayCall += () =>
            {
                ApplyStyle(elementName, (element) =>
                {
                    T targetElement = null;

                    if (element is T typedElement)
                    {
                        targetElement = typedElement;
                    }
                    else
                    {
                        targetElement = element.Query<T>().First();
                    }

                    if (targetElement != null)
                    {
                        styleAction(targetElement);
                    }
                });
            };
        }

        private static void ApplyStyle(string elementName, System.Action<VisualElement> styleCallback)
        {
            var element = FindElementByName(elementName);
            if (element != null)
            {
                styleCallback(element);
            }
        }

        private static VisualElement FindElementByName(string name)
        {
            var nameWithoutSpace = name.Replace(" ", "");
            var windows = Resources.FindObjectsOfTypeAll<EditorWindow>();
            
            foreach (var window in windows)
            {
                var root = window.rootVisualElement;
                if (root == null) continue;

                // Try to find by name
                var element = root.Query(name: name).First();
                if (element != null) return element;

                // Try without spaces
                element = root.Query(name: nameWithoutSpace).First();
                if (element != null) return element;

                // Try all children with the name
                var query = root.Query().Where(el => el.name == name).First();
                if (query != null) return query;
            }

            return null;
        }

        /// <summary>
        /// Get the MainToolbar and find an element by name
        /// </summary>
        public static VisualElement FindMainToolbarElement(string elementName)
        {
            return FindElementByName(elementName);
        }
    }
}
