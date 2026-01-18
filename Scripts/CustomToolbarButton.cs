using UnityEditor;
using UnityEditor.Toolbars;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

namespace UnityToolbarExtender
{
    /// <summary>
    /// Example of a custom button in the main toolbar using the new Unity 6.3+ API
    /// </summary>
    public class CustomToolbarButton
    {

        [MainToolbarElement("UniGame/ClearPrefs", defaultDockPosition = MainToolbarDockPosition.Middle)]
        public static MainToolbarElement CreateClearPrefsButton()
        {
            var icon = EditorGUIUtility.IconContent("TreeEditor.Trash").image as Texture2D;
            var content = new MainToolbarContent(icon, "Clear Prefs");
            
            var button = new MainToolbarButton(content, () =>
            {
                PlayerPrefs.DeleteAll();
                PlayerPrefs.Save();
                Debug.Log("Player preferences cleared!");
            });

            return button;
        }

        [MainToolbarElement("UniGame/ReloadScene", defaultDockPosition = MainToolbarDockPosition.Middle)]
        public static MainToolbarElement CreateReloadSceneButton()
        {
            var icon = EditorGUIUtility.IconContent("Refresh").image as Texture2D;
            var scene = EditorSceneManager.GetActiveScene();
            var label = new Label($"Reload: {scene.name}");
            label.style.marginLeft = 4;
            label.style.marginRight = 4;
            
            var content = new MainToolbarContent(icon, "Reload Scene");
            
            var button = new MainToolbarButton(content, () =>
            {
                var activeScene = EditorSceneManager.GetActiveScene();
                if (activeScene.isLoaded)
                {
                    EditorSceneManager.OpenScene(activeScene.path, OpenSceneMode.Single);
                    label.text = $"Reload: {activeScene.name}";
                }
            });

            return button;
        }

        [MainToolbarElement("UniGame/RecompileCode", defaultDockPosition = MainToolbarDockPosition.Middle)]
        public static MainToolbarElement CreateRecompileCodeButton()
        {
            var icon = EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D;
            var content = new MainToolbarContent(icon, "Recompile");
            
            var button = new MainToolbarButton(content, () =>
            {
                AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
                EditorUtility.RequestScriptReload();
                Debug.Log("Code recompilation triggered");
            });

            return button;
        }

        [MainToolbarElement("UniGame/TimeScale", defaultDockPosition = MainToolbarDockPosition.Middle)]
        public static MainToolbarElement CreateTimeScaleControl()
        {
            var content = new MainToolbarContent("Time Scale", "Time Scale");
            return new MainToolbarSlider(content, Time.timeScale, 0f, 10f, OnTimeScaleSliderValueChanged);
        }

        static void OnTimeScaleSliderValueChanged(float newValue)
        {
            Time.timeScale = newValue;
        }
    }
}