using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FlowerCanvas : MonoBehaviour
{
    public Button JumpButton;
    public Button CollectionGalleryButton;
    public Button SettingButton;
    public List<SkillBtn> SkillIcons = new List<SkillBtn>();

    protected GameEventListener m_eventListener;

    // Start is called before the first frame update
    void Start()
    {
        m_eventListener = new GameEventListener();
        m_eventListener.ListenForEvent(EGameEvents.SetSkill, OnSetSkill);
        Init();
    }

    public void Init()
    {
        if (Services.Has<DataManager>())
        {
            foreach (var go in Services.Get<DataManager>().PlayerData.flowerItems)
            {
                setSkill(go);
            }
        }

    }

    public EventResult OnSetSkill(object eventData)
    {
        EventResult eventresult = new EventResult(false);
        string skillName = (string)eventData;
        setSkill(skillName);
        return eventresult;
    }

    private void setSkill(string skillName)
    {
        switch (skillName)
        {
            case "Strelitzia":
                //天堂鳥
                SkillIcons[0].SetSkill(true);
                break;
            case "Tinglihua":
                //葶歷花
                SkillIcons[1].SetSkill(true);
                break;
            case "Pansy":
                //三色堇
                SkillIcons[2].SetSkill(true);
                break;
        }
    }
}
