using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryConditionsData
{
    public List<string> Conditions = new List<string>();

    public virtual bool CheckResult()
    {
        return false;
    }
}

public class CompleteConditionsData : StoryConditionsData
{
    public override bool CheckResult()
    {
        bool result = Conditions.Count == 0 ? true : false;
        return result;
    }
}

public class IncompleteConditionsData : StoryConditionsData
{
    public int ori_count = 0;

    public override bool CheckResult()
    {
        bool result = Conditions.Count != ori_count ? true : false;
        return result;
    }
}

public class StoryController : MonoBehaviour
{
    public Flowchart flowchart;
    public ActionManager actionManager;
    private StoryConditionsData storyConditionsData;

    private const string Clue_StringVariable = "";
    private const string Npc_StringVariable = "talkedNPCName";
    private const string Door_StringVariable = "destination";
    private const string State_StringVariable = "stateName";
    private const string OpenUI_StringVariable = "openUIName";

    private const string Action_StringVariable = "actionName";

    private const string HasFinishedConditions_BooleanVariable = "hasFinishedConditions";

    public void ClueProcess(object eventData)
    {
        string clueName = (string)eventData;
        Debug.Log("ClueProcess clueName = " + clueName);

        Process(Clue_StringVariable, clueName);
    }

    public void NPCProcess(object eventData)
    {
        string npcName = (string)eventData;
        Debug.Log("NPCProcess npcName = " + npcName);

        Process(Npc_StringVariable, npcName);
    }

    public void DoorProcess(object eventData)
    {
        string placeName = (string)eventData;
        Debug.Log("DoorProcess placeName = " + placeName);

        Process(Door_StringVariable, placeName);
    }

    public void StateProcess(object eventData)
    {
        string stateName = (string)eventData;
        Debug.Log("StateProcess stateName = " + stateName);

        Process(State_StringVariable, stateName);
    }

    public void OpenUIProcess(object eventData)
    {
        string openUIName = (string)eventData;
        Debug.Log("OpenUIProcess openUIName = " + openUIName);

        Process(OpenUI_StringVariable, openUIName);
    }

    public void ActionProcess(object eventData)
    {
        string actionName = (string)eventData;
        Debug.Log("ActionProcess actionName = " + actionName);

        Process(null, actionName);
    }

    private void Process(string stringVariableName, string eventDataName)
    {
        ConditionsProcess(eventDataName);
        if (!string.IsNullOrEmpty(stringVariableName))
        {
            flowchart.SetStringVariable(stringVariableName, eventDataName);
        }
    }

    private void ConditionsProcess(string name)
    {
        if (storyConditionsData == null)
            return;

        for (int i = 0; i < storyConditionsData.Conditions.Count; i++)
        {
            if (storyConditionsData.Conditions[i] == name)
                storyConditionsData.Conditions.Remove(storyConditionsData.Conditions[i]);
        }
        flowchart.SetBooleanVariable(HasFinishedConditions_BooleanVariable, storyConditionsData.CheckResult());
    }

    public void CreateCompleteConditionsData()
    {
        flowchart.SetBooleanVariable(HasFinishedConditions_BooleanVariable, false);
        storyConditionsData = new CompleteConditionsData();
    }

    public void CreateIncompleteConditionsData()
    {
        flowchart.SetBooleanVariable(HasFinishedConditions_BooleanVariable, false);
        storyConditionsData = new IncompleteConditionsData();
    }

    public void ClearConditionsData()
    {
        storyConditionsData = null;
    }

    public void SetConditions(string conditionName)
    {
        if (!string.IsNullOrEmpty(conditionName))
        {
            storyConditionsData.Conditions.Add(conditionName);
        }
    }
    
    public void Say(int index)
    {
        actionManager.StartChapterAction(index, () => {
            Debug.Log("對話結束 = " + index);
            SetStoryIndex(index);
        });
    }

    public void SetStateName(string stateName)
    {
        flowchart.SetStringVariable(State_StringVariable, stateName);
    }

    public void SetOpenUIName(string openUIName)
    {
        flowchart.SetStringVariable(OpenUI_StringVariable, openUIName);
    }

    public void SetFlowVarString(string name, string value)
    {
        flowchart.SetStringVariable(name, value);
    }

    public void SetFlowVarBool(string name, bool value)
    {
        flowchart.SetBooleanVariable(name, value);
    }

    public bool CheckStoryPassCode(string _passCode)
    {
        string passCode = flowchart.GetStringVariable("StroyPassCode");
        if (passCode == _passCode)
            return true;
        else
            return false;
                
    }

    public void SetNPCTalkedReward(string mapName, string npcName, string rewardValue)
    {
        Debug.Log("SetNPCTalkedReward");
        string[] package = new string[3] { mapName, npcName, rewardValue };
        Services.Get<EventManager>().SendEvent((int)EGameEvents.GotTheClueAfterNpcTalked, package);
    }

    public void SetSniperGameAccessPermission(bool value)
    {
        Services.Get<EventManager>().SendEvent((int)EGameEvents.SetSniperGameAccessPermission, value);
    }

    public void SetBulletTableAccessPermission(bool value)
    {
        Services.Get<EventManager>().SendEvent((int)EGameEvents.SetBulletTableAccessPermission, value);
    }

    public void SetHintwallStateAccessPermission(bool value)
    {
        Services.Get<EventManager>().SendEvent((int)EGameEvents.SetHintwallStateAccessPermission, value);
    }

    public void SetSniperGamePause(bool value)
    {
        Services.Get<EventManager>().SendEvent((int)EGameEvents.SetSniperGamePause, value);
    }

    public void SetDoorAccessPermission(string mapName,string doorName,bool value)
    {
        object[] package = new object[3] { mapName, doorName, value };
        Services.Get<EventManager>().SendEvent((int)EGameEvents.SetDoorAccessPermission, package);
    }

    public void SetNpcAccessPermission(string mapName, string npcName, bool value)
    {
        object[] package = new object[3] { mapName, npcName, value };
        Services.Get<EventManager>().SendEvent((int)EGameEvents.SetNpcAccessPermission, package);
    }

    public void SetClueAccessPermission(string mapName, string clueName, bool value)
    {
        object[] package = new object[3] { mapName, clueName, value };
        Services.Get<EventManager>().SendEvent((int)EGameEvents.SetClueAccessPermission, package);
    }

    public void SetBigMapBtnAccessPermission(string mapName, string btnName, bool value)
    {
        object[] package = new object[3] { mapName, btnName, value };
        Services.Get<EventManager>().SendEvent((int)EGameEvents.SetBigMapBtnAccessPermission, package);
    }

    public void CheckStoryIndex(int storyIndex)
    {
        bool result = Services.Get<DataManager>().PlayerData.storyIndexes.Contains(storyIndex);
        flowchart.SetBooleanVariable("CanPlayStory", !result);
    }

    public void SetStoryIndex(int storyIndex)
    {
        bool result = Services.Get<DataManager>().PlayerData.storyIndexes.Contains(storyIndex);
        if (!result)
        {
            Services.Get<DataManager>().SaveStoryIndex(storyIndex);
        }
    }

    public void CheckDoorIndex(int doorIndex)
    {
        bool result = Services.Get<DataManager>().PlayerData.activeDoorIndexes.Contains(doorIndex);
        flowchart.SetBooleanVariable("CanOpenDoor", result);
    }

    public void CheckFlowerItem(string flowerName)
    {
        bool result = Services.Get<DataManager>().PlayerData.flowerItems.Contains(flowerName);
        flowchart.SetBooleanVariable("GotThisFlower", result);
    }

    public void CheckNormalEnd()
    {
        bool result = Services.Get<DataManager>().CheckNormalEnd();
        flowchart.SetBooleanVariable("NormalEndPass", result);
    }

    public void Reset()
    {
        flowchart.ExecuteBlock("Start Init");
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
