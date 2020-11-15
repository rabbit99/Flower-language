using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager :MonoBehaviour 
{
    public talkData _talkData;
    private talkDataData[] _talkDataData;

    public characterNameListData _characterNameListData;
    private characterNameListDataData[] _characterNameListDataDatas;

    private GameObject talkGameObject;
    public TalkController talkController;

    public StoryManager storyManager;

    private void Start()
    {
        SetData();
    }
    public void SetData()
    {
        talkController.SetActionManager(this);
        //_talkData = Services.Get<ResourcesManager>().GetAsset<talkData>(gameDataFilePaths[0]);
        _talkDataData = _talkData.dataArray;
        //_characterNameListData = Services.Get<ResourcesManager>().GetAsset<characterNameListData>(gameDataFilePaths[3]);
        _characterNameListDataDatas = _characterNameListData.dataArray;

        //talkController = Services.Get<UIManager>().OpenUI<TalkController>("TalkController");
        foreach(var go in _characterNameListDataDatas)
        {
            talkController.SetCharacterDataList(go.Code, go.Chinesename);
        }
    }

    public void ChangeGameData(string[] gameDataFilePaths)
    {
        _talkData = Services.Get<ResourcesManager>().GetAsset<talkData>(gameDataFilePaths[0]);
        _talkDataData = _talkData.dataArray;
        _characterNameListData = Services.Get<ResourcesManager>().GetAsset<characterNameListData>(gameDataFilePaths[3]);
        _characterNameListDataDatas = _characterNameListData.dataArray;
    }

    public void StartChapterAction(int chapterIndex, Action callback = null)
    {
        talkDataData[] data = Array.FindAll<talkDataData>(_talkDataData, d => d.Index == chapterIndex);
        ActionData[] _actionDatas = new ActionData[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            _actionDatas[i] = CreateActionData(
                data[i].Action,
                data[i].Arg1,
                data[i].Arg2,
                data[i].Arg3,
                data[i].Arg4,
                data[i].Arg5,
                data[i].Arg6,
                data[i].Arg7
                );
        }
        talkController.SetActionData(_actionDatas, callback);
        talkController.StartTalk();

        talkController.insertAction = InsertChapterData;
    }

    private void InsertChapterData(int chapterIndex)
    {
        talkDataData[] data = Array.FindAll<talkDataData>(_talkDataData, d => d.Index == chapterIndex);
        ActionData[] _actionDatas = new ActionData[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            _actionDatas[i] = CreateActionData(
                data[i].Action,
                data[i].Arg1,
                data[i].Arg2,
                data[i].Arg3,
                data[i].Arg4,
                data[i].Arg5,
                data[i].Arg6,
                data[i].Arg7
                );
        }
        talkController.SetActionData(_actionDatas);
    }



    public void SetBg(string bgName)
    {
        talkController.SetBg(bgName);
    }

    public void SetBg(int chapterIndex)
    {
        //TO DO
        //用chapterIndex開bg
        //talkController.SetBg(bgName);
    }

    public void ActionFinished()
    {
        storyManager.SetFlowVarBool("ActionFinished", true);
    }

    public void Close()
    {

    }

    private ActionData CreateActionData(
    string Action,
    string Arg1,
    string Arg2,
    string Arg3,
    string Arg4,
    string Arg5,
    string Arg6,
    string Arg7
    )
    {
        ActionData actionData = new ActionData();
        actionData.Action = Action;
        actionData.Args.Add(Arg1);
        actionData.Args.Add(Arg2);
        actionData.Args.Add(Arg3);
        actionData.Args.Add(Arg4);
        actionData.Args.Add(Arg5);
        actionData.Args.Add(Arg6);
        actionData.Args.Add(Arg7);
        return actionData;
    }
}
