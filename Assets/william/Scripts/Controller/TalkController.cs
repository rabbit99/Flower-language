using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;
using System;

public class TalkController : MonoBehaviour
{
    public Flowchart flowchart;
    public Image bgImage;
    public Image leftImage;
    public Image rightImage;
    public Text characterNameText;
    public GameObject blockInput;

    private ActionData[] _actionDatas = null;
    private Action _callback;
    private int _actionIndex = 0;

    private List<CharacterData> CharacterDataList = new List<CharacterData>();

    private List<int> BtnIndexList = new List<int>();

    public delegate void InsertAction(int index);
    public InsertAction insertAction;

    public BtnView btnView;

    private ActionManager actionManager;

    void Awake()
    {
        //flowchart = GetComponentInChildren<Flowchart>();
    }

    // Start is called before the first frame update
    void Start()
    {
        leftImage.gameObject.SetActive(false);
        rightImage.gameObject.SetActive(false);
        //blockInput.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActionManager(ActionManager _actionManager)
    {
        actionManager = _actionManager;
    }

    public void SetCharacterDataList(string code,string name)
    {
        CharacterData chData = new CharacterData();
        chData.code = code;
        chData.Charactername = name;
        CharacterDataList.Add(chData);
    }

    public void StartTalk(Action onComplete = null)
    {
        flowchart.SetBooleanVariable("Finished", false);
        //if(Services.Get<EventManager>() != null)
        //{
        //    Services.Get<EventManager>().SendEvent((int)EGameEvents.SetPlayerControl, false);
        //}

        //blockInput.SetActive(true);
        StartCoroutine(Execute(onComplete));
    }

    public IEnumerator Execute(Action onComplete = null)
    {
        if (onComplete != null)
        {
            _callback = onComplete;
        }
        yield return new WaitForEndOfFrame();
        flowchart.StopBlock("Action");
        yield return new WaitForEndOfFrame();
        flowchart.ExecuteBlock("Action");
    }

    public void SetBg(string bgName)
    {
        if (string.IsNullOrEmpty(bgName) || bgName == "none")
        {
            bgImage.gameObject.SetActive(false);
            return;
        }
            
        bgImage.gameObject.SetActive(true);
        if(bgImage.sprite == null || bgName != bgImage.sprite.name)
        {
            Sprite _sp = Services.Get<ResourcesManager>().GetAsset<Sprite>("Tex/" + bgName);
            if (_sp)
            {
                bgImage.sprite = _sp;
            }
            else
            {
                Debug.LogError("cant find the image by this bg name! name is " + bgName);
            }
        }
    }

    public void SetActionData(ActionData[] data, Action callback = null)
    {
        if (_actionDatas != null)
        {
            List<ActionData> tempActionDatas = new List<ActionData>();
            for (int i = 0; i < data.Length; i++)
            {
                tempActionDatas.Add(data[i]);
            }
            if (_actionIndex != _actionDatas.Length - 1)
            {
                for (int i = _actionIndex + 1; i < _actionDatas.Length; i++)
                {
                    tempActionDatas.Add(_actionDatas[i]);
                }
            }
            _actionDatas = tempActionDatas.ToArray();
            _actionIndex = 0;
        }
        else
        {
            _actionDatas = data;
        }
 
        if(callback != null)
        {
            _callback = callback;
        }
    }

    public void SetNextAction()
    {
        //if the next action is say
        if (_actionDatas[_actionIndex].Action == ActionKey.Say)
        {
            setTalk();
            flowchart.ExecuteBlock(ActionKey.Say);
        }
        else if (_actionDatas[_actionIndex].Action == ActionKey.Choice)
        {
            setBtn();
            flowchart.ExecuteBlock(ActionKey.Choice);
        }
        else if (_actionDatas[_actionIndex].Action == ActionKey.SetProperty)
        {
            setProperty();
            flowchart.ExecuteBlock(ActionKey.SetProperty);
        }
        else if (_actionDatas[_actionIndex].Action == ActionKey.Operation)
        {
            setOperation();
            flowchart.ExecuteBlock(ActionKey.Operation);
        }
        else
        {
            Debug.LogError(_actionDatas[_actionIndex].Action + " is invalid value!");
            flowchart.ExecuteBlock("InvalidAction");
        }
    }

    private void setTalk()
    {
        SetBg(_actionDatas[_actionIndex].Args[(int)SayArgument.bgImageName]);

        flowchart.SetStringVariable("Dialogue", _actionDatas[_actionIndex].Args[(int)SayArgument.content]);

        setRoleImage(_actionDatas[_actionIndex]);
        setCharactername(_actionDatas[_actionIndex]);
    }

    private void setBtn()
    {
        Debug.Log("setBtn");
        BtnIndexList.Add(Int32.Parse(_actionDatas[_actionIndex].Args[(int)ButtonArgument.nextIndex]));

        //get and set btn name by _actionDatas[_actionIndex].Args[(int)ActionArgument.buttonText]
        btnView.SetBtn(_actionDatas[_actionIndex].Args[(int)ButtonArgument.buttonText]);

        if (_actionDatas[_actionIndex].Args[(int)ButtonArgument.waitForClick] == "yes" ||
            _actionIndex == _actionDatas.Length - 1
            )
        {
            return;
        }

        if (_actionDatas[_actionIndex + 1].Action != "choice")
        {
            return;
        }
        else if (_actionDatas[_actionIndex + 1].Action == "choice")
        {
            //setBtn again if the action is choice after increase _actionIndex;
            _actionIndex++;
            //Recursive
            setBtn();
        }
    }

    private void setProperty()
    {
        Services.Get<DataManager>().SetProperty(_actionDatas[_actionIndex].Args[(int)PropertyArgument.propertyKey], Int32.Parse(_actionDatas[_actionIndex].Args[(int)PropertyArgument.propertyValue]));
    }

    private void setOperation()
    {
        switch (_actionDatas[_actionIndex].Args[(int)OperationArgument.operationKey])
        {
            default:
                break;
        }
    }

    private void setRoleImage(ActionData data)
    {
        if(data.Args[(int)SayArgument.leftRoleName] != "none")
        {
            Sprite sp = Services.Get<ResourcesManager>().GetAsset<Sprite>(
                "Portraits/" +
                data.Args[(int)SayArgument.leftRoleName] +
                "/" +
                data.Args[(int)SayArgument.leftRoleName] +
                "_" +
                data.Args[(int)SayArgument.leftRoleMood]
                );
            if (sp == null)
            {
                leftImage.gameObject.SetActive(false);
                Debug.LogError("cant find the image by this role name! name is " + data.Args[(int)SayArgument.leftRoleName] +
                "/" +
                data.Args[(int)SayArgument.leftRoleName] +
                "_" +
                data.Args[(int)SayArgument.leftRoleMood]);
            }
            else
            {
                leftImage.sprite = sp;
                leftImage.gameObject.SetActive(true);
            }
        }
        else
        {
            leftImage.gameObject.SetActive(false);
        }

        if (data.Args[(int)SayArgument.rightRoleName] != "none")
        {
            Sprite sp = Services.Get<ResourcesManager>().GetAsset<Sprite>(
                "Portraits/" +
                data.Args[(int)SayArgument.rightRoleName] +
                "/" +
                data.Args[(int)SayArgument.rightRoleName] +
                "_" +
                data.Args[(int)SayArgument.rightRoleMood]
                );
            
            if (sp == null)
            {
                rightImage.gameObject.SetActive(false);
                Debug.LogError("cant find the image by this role name! name is " + data.Args[(int)SayArgument.rightRoleName] +
                "/" +
                data.Args[(int)SayArgument.rightRoleName] +
                "_" +
                data.Args[(int)SayArgument.rightRoleMood]);
            }
            else
            {
                rightImage.sprite = sp;
                rightImage.gameObject.SetActive(true);
            }
        }
        else
        {
            rightImage.gameObject.SetActive(false);
        }
    }

    private void setCharactername(ActionData data)
    {
        if(data.Args[(int)SayArgument.characterName] != "none" && !string.IsNullOrEmpty(data.Args[(int)SayArgument.characterName]))
        {
            CharacterData _cd = CharacterDataList.Find(x => x.code.Contains(data.Args[(int)SayArgument.characterName]));
            if (_cd != null)
            {
                characterNameText.text = _cd.Charactername;
            }
            else
            {
                characterNameText.text = "***";
                Debug.LogError("cant find the image by this character name! name is " + data.Args[(int)SayArgument.characterName]);
            }
          
        }
        else
        {
            characterNameText.text = "";
        }
        
    }

    public void ActionFinished()
    {
        //關掉顯示
        bgImage.gameObject.SetActive(false);
        leftImage.gameObject.SetActive(false);
        rightImage.gameObject.SetActive(false);
        blockInput.SetActive(false);
        _actionIndex = 0;
        _actionDatas = null;
        //DoCallBack
        if (_callback != null)
        {
            _callback();
        }
        actionManager.ActionFinished();
        //Services.Get<EventManager>().SendEvent((int)EGameEvents.SetPlayerControl, true);
    }


    public void BtnClicked(int index)
    {
        //get and play next action index
        insertAction?.Invoke(BtnIndexList[index]);
        BtnIndexList.Clear();
        //reset btns view
        btnView.ResetBtn();

        flowchart.ExecuteBlock("Action");
    }

    public void Repeat()
    {
        if (_actionIndex == _actionDatas.Length - 1)
        {
            flowchart.SetBooleanVariable("Finished", true);
        }
        else
        {
            _actionIndex++;
        }
    }
}
