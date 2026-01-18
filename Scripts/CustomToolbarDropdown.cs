using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;
using System;

namespace UnityToolbarExtender
{
    /// <summary>
    /// Example dropdown elements for the main toolbar using the new Unity 6.3+ API
    /// </summary>
    public class CustomToolbarDropdown
    {
        /// <summary>
        /// Toolbar class for dropdowns and other elements
        /// </summary>
        public class CustomToolbarDropdownElements
        {
            [MainToolbarElement("UniGame/EnterPlayMode", defaultDockPosition = MainToolbarDockPosition.Middle)]
            public static MainToolbarElement CreateEnterPlayModeDropdown()
            {
                var content = new MainToolbarContent("Enter Play Mode Options");
                
                var dropdown = new MainToolbarDropdown(content, rect =>
                {
                    var menu = new GenericMenu();
                    
                    #if UNITY_2022_2_OR_NEWER
                    menu.AddItem(new GUIContent("Disabled"), !EditorSettings.enterPlayModeOptionsEnabled, () =>
                    {
                        EditorSettings.enterPlayModeOptionsEnabled = false;
                    });

                    menu.AddItem(new GUIContent("Reload Domain"), 
                        EditorSettings.enterPlayModeOptionsEnabled && 
                        EditorSettings.enterPlayModeOptions == EnterPlayModeOptions.None, () =>
                    {
                        EditorSettings.enterPlayModeOptionsEnabled = true;
                        EditorSettings.enterPlayModeOptions = EnterPlayModeOptions.None;
                    });

                    menu.AddItem(new GUIContent("Reload Scene Only"), 
                        EditorSettings.enterPlayModeOptionsEnabled && 
                        (EditorSettings.enterPlayModeOptions & EnterPlayModeOptions.DisableDomainReload) != 0, () =>
                    {
                        EditorSettings.enterPlayModeOptionsEnabled = true;
                        EditorSettings.enterPlayModeOptions = EnterPlayModeOptions.DisableDomainReload;
                    });
                    #endif
                    
                    menu.ShowAsContext();
                });

                return dropdown;
            }

            private static float _lastFrameRate = 60;

            [MainToolbarElement("UniGame/FPS", defaultDockPosition = MainToolbarDockPosition.Middle)]
            public static MainToolbarElement CreateFPSSlider()
            {
                var currentFPS = Application.targetFrameRate > 0 ? Application.targetFrameRate : 60;
                var content = new MainToolbarContent($"FPS: {currentFPS}");
                
                return new MainToolbarSlider(
                    content,
                    currentFPS,
                    0,
                    240,
                    OnFPSSliderValueChanged
                );
            }

            static void OnFPSSliderValueChanged(float newValue)
            {
                int fpsValue = (int)newValue;
                Application.targetFrameRate = fpsValue > 0 ? fpsValue : -1;
                _lastFrameRate = fpsValue;
            }
        }
    }
}