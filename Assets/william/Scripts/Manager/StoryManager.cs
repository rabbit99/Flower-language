
using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    public StoryController storyController;
    private void Start()
    {
        SetPlayerJumpConut();
        SetPlayerSpash();
        SetPlayerInevitable();
    }
    public void GoStroy(string passCode)
    {
        if (storyController != null && storyController.CheckStoryPassCode(passCode))
        {
            storyController.SetFlowVarBool("GoStory", true);
        }
    }

    public void SavePoint(SceneTransitionDestination sceneTransitionDestination)
    {
        if (Services.Has<DataManager>())
        {
            Services.Get<DataManager>().SetSavePoint(SceneManager.GetActiveScene().name, sceneTransitionDestination.destinationTag);
        }

    }

    public void SaveaDoorIndex(int doorIndex)
    {
        if (Services.Has<DataManager>())
        {
            Services.Get<DataManager>().SaveaDoorIndex(doorIndex);
        }

    }

    public void SaveFlower(string flowerName)
    {
        if (Services.Has<DataManager>() && Services.Has<EventManager>())
        {
            if (Services.Get<DataManager>().SaveFlower(flowerName))
            {
                Services.Get<EventManager>().SendEvent((int)EGameEvents.SetSkill, flowerName);
            }
        }
    }

    public void SetPlayerJumpConut()
    {
        if (Services.Has<DataManager>())
        {
            PlayerCharacter.PlayerInstance.OriJumpConut = Services.Get<DataManager>().PlayerData.oriJumpConut;
        }
    }

    public void SetPlayerSpash()
    {
        if (Services.Has<DataManager>())
        {
            PlayerCharacter.PlayerInstance.CanSplash = Services.Get<DataManager>().PlayerData.canSplash;
        }
    }

    public void SetPlayerInevitable()
    {
        if (Services.Has<DataManager>())
        {
            PlayerCharacter.PlayerInstance.CanInevitable = Services.Get<DataManager>().PlayerData.canInevitable;
        }
    }

    public void GainPlayerInput()
    {
        PlayerInput.Instance.GainControl();
    }

    public void ReleasePlayerInput(bool reset)
    {
        PlayerInput.Instance.ReleaseControl(reset);
    }

    public void ChangeStoryController(string referencePath)
    {
        if(storyController != null)
        {
            storyController.DestroySelf();
        }
        storyController = Services.Get<ResourcesManager>().Spawn(referencePath).GetComponent<StoryController>();
    }

    #region Process Check
    public void ClueProcess(object eventData)
    {
        storyController.ClueProcess(eventData);
    }

    public void NPCProcess(object eventData)
    {
        storyController.NPCProcess(eventData);
    }

    public void DoorProcess(object eventData)
    {
        storyController.DoorProcess(eventData);
    }

    public void StateProcess(object eventData)
    {
        storyController.StateProcess(eventData);
    }

    public void OpenUIProcess(object eventData)
    {
        storyController.OpenUIProcess(eventData);
    }

    public void ActionProcess(object eventData)
    {
        storyController.ActionProcess(eventData);
    }

    #endregion

    #region Set
    public void SetFlowVarString(string name, string value)
    {
        storyController.SetFlowVarString(name, value);
    }

    public void SetFlowVarBool(string name, bool value)
    {
        storyController.SetFlowVarBool(name, value);
    }

    public void Reset()
    {
        storyController.Reset();
    }
    #endregion
}
