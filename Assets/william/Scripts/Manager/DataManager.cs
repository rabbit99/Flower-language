using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager
{
    public DataManager()
    {
        if (!Load())
        {
            CreateNewPlayerData();
        }
    }
    private PlayerData playerData;

    public PlayerData PlayerData
    {
        get => playerData;
        set => playerData = value;
    }

    public bool Load()
    {
        object playerDataObj;
        try
        {
             playerDataObj = ES3.Load("myPlayerData");
        }
        catch
        {
            Debug.Log("Load catch failed");
            return false;
        }
        
        if (playerDataObj == null)
        {
            Debug.Log("Load failed");
            return false;
        }  
        else
        {
            Debug.Log("Load success");
            playerData = playerDataObj as PlayerData;
            return true;
        }
        playerData = ES3.Load("myPlayerData", new PlayerData());
        
    }

    public void CreateNewPlayerData()
    {
        playerData = new PlayerData();
        save();
    }

    public void Save()
    {
        save();
    }

    public void SetSavePoint(string point, SceneTransitionDestination.DestinationTag tag)
    {
        playerData.saveSceneName = point;
        playerData.saveTransitionDestinationTag = tag;
        Save();
    }

    public void SaveStoryIndex(int index)
    {
        playerData.storyIndexes.Add(index);
        Save();
    }

    public void SaveaDoorIndex(int index)
    {
        if (!playerData.activeDoorIndexes.Contains(index))
        {
            playerData.activeDoorIndexes.Add(index);
            Save();
        }
    }

    public bool SaveFlower(string flowerName)
    {
        if (!playerData.flowerItems.Contains(flowerName))
        {
            playerData.flowerItems.Add(flowerName);

            switch (flowerName)
            {
                case "Strelitzia":
                    //天堂鳥二段跳
                    PlayerCharacter.PlayerInstance.OriJumpConut = 1;
                    playerData.oriJumpConut = 1;
                    break;
                case "Tinglihua":
                    //葶歷花衝刺
                    PlayerCharacter.PlayerInstance.CanSplash = true;
                    playerData.canSplash = true;
                    break;
                case "Pansy":
                    //三色堇無敵
                    PlayerCharacter.PlayerInstance.CanInevitable = true;
                    playerData.canInevitable = true;
                    break;
            }

            Save();
            return true;
        }
        else
            return false;
    }

    public void SaveDiary(int index)
    {
        if (!playerData.activeDiarys.Contains(index))
        {
            playerData.activeDiarys.Add(index);
            Save();
        }
    }

    public bool CheckDiary(int index)
    {
        if (!playerData.activeDiarys.Contains(index))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckNormalEnd()
    {
        List<string> NormalEndFlowerItems = new List<string> { "Strelitzia", "Tinglihua", "Pansy" };
        bool equal = false;
        for(int i = 0;i < NormalEndFlowerItems.Count; i++)
        {
            equal = playerData.flowerItems.Contains(NormalEndFlowerItems[i]);
        }
 
        return equal;
    }

    public void SaveFinishedNormalEnd()
    {
        playerData.isFinishedNormalEnd = true;
        Save();
    }

    private void save()
    {
        ES3.Save("myPlayerData", playerData);
    }


    public void Reset()
    {
        playerData = null;
    }

    public void SetProperty(string key, int value)
    {
        //if (!PlayerData.PropertyDic.ContainsKey(key))
        //{
        //    PlayerData.PropertyDic[key] = value;
        //}
        //else
        //{
        //    PlayerData.PropertyDic[key] += value;
        //}
    }
}
