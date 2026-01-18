# Unity Custom Toolbar

![Custom Toolbar](https://i.ibb.co/RTZXKtwk/ctb1.png)

## Features

- ✅ **Time Scale Control** - Adjust game speed with slider (0-10 range)
- ✅ **FPS Control** - Adjust target frame rate with slider (0-240)
- ✅ **Scene Reloading** - Quick reload current scene with dynamic label
- ✅ **Code Recompilation** - Trigger script recompilation
- ✅ **PlayerPrefs Clearing** - Quick clear all preferences
- ✅ **Play Mode Options** - Dropdown for enter play mode settings

## Quick Start

Create a custom toolbar element in just 15 lines:

```csharp
using UnityEditor;
using UnityEditor.Toolbars;

public class MyToolbar
{
    [MainToolbarElement("UniGame/PlayButton", defaultDockPosition = MainToolbarDockPosition.Middle)]
    public static MainToolbarElement PlayButton()
    {
        return new MainToolbarButton(
            new MainToolbarContent("Play"),
            () => EditorApplication.isPlaying = !EditorApplication.isPlaying
        );
    }
}
```

## Installation

```json
"com.unigame.customtoolbar" : "https://github.com/UnioGame/custom.toolbar.git",
```

## Available Toolbar Elements

All toolbar elements use the unified group naming convention: `UniGame/[ElementName]`

### 1. Time Scale Slider (`UniGame/TimeScale`)
- **Range**: 0 to 10
- **Function**: Controls Time.timeScale for game speed adjustment

### 2. FPS Slider (`UniGame/FPS`)
- **Range**: 0 to 240
- **Function**: Controls Application.targetFrameRate

### 3. Reload Scene Button (`UniGame/ReloadScene`)
- **Function**: Reloads the current scene using EditorSceneManager
- **Feature**: Dynamic label showing current scene name

### 4. Recompile Code Button (`UniGame/RecompileCode`)
- **Function**: Triggers code recompilation via AssetDatabase.Refresh() and EditorUtility.RequestScriptReload()

### 5. Clear Preferences Button (`UniGame/ClearPrefs`)
- **Function**: Clears all PlayerPrefs and saves


### 6. Enter Play Mode Dropdown (`UniGame/EnterPlayMode`)
- **Options**:
  - Disabled (no options)
  - Reload Domain (full reload)
  - Reload Scene Only (faster play mode)
- **Function**: Controls EditorSettings enter play mode behavior

## System Requirements

- **Unity 6.3 or newer**
- Editor-only package