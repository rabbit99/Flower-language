using ES3Types;
using Gamekit2D;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public bool isNewPlayer = true;
    public string playerName = string.Empty;
    //public Dictionary<string, int> PropertyDic = new Dictionary<string, int>();
    
    public string saveSceneName;
    public SceneTransitionDestination.DestinationTag saveTransitionDestinationTag;

    public List<string> flowerItems = new List<string>();
    public List<int> storyIndexes = new List<int>();
    public List<int> activeDoorIndexes = new List<int>();

    public int oriJumpConut = 0;
    public bool canSplash = false;
    public bool canInevitable = false;
}