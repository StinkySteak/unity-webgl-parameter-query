# WebGL Parameter Query
WebGL Parameter Query allow developers or users launch their webGL app with parameter similarly to native apps. The query will returns as a JSON

**Windows**
```
myUnityGame.exe -debug -resetSettings
```

**WebGL**
```
https://agar.io?debug=true&resetSettings=true
```

## Example Use?

### Launch URL
```
https://agar.io?debug=true
```

```cs
string json = WebGLParameterInfo.GetParameters();

var settings = JsonUtility.FromJson<LaunchSettings>(json);

public class LaunchSettings
{
	public bool Debug;
}
```

## Installation
#### Install via git URL (Unity Package Manager [UPM])
```
https://github.com/StinkySteak/unity-webgl-parameter-query.git
```

## Compatibility
#### Tested on
| Unity Version 	| Status 	| 
|---------------	|--------	|
| 2022.3        	| Passed    |
| 2021.3        	| Passed    |

Major unity version has different emscripten runtime, please refer to https://docs.unity3d.com/Manual/webgl-native-plugins-with-emscripten.html for more info.
