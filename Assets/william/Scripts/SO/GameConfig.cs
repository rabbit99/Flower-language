using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "CustomPlugin/GameConfig", order = 1)]
public class GameConfig : ScriptableObject
{
    public string[] gameDataFilePaths = new string[] {  };
    public string storyControllerPath = "Story/";
    public int oldDemoFirstStoryIndex = 0;
    public int newDemoFirstStoryIndex = 0;
    public int interViewFirstStoryIndex = 0;
}
