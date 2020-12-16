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
        Init();
        m_eventListener = new GameEventListener();
        m_eventListener.ListenForEvent(EGameEvents.SetSkill, OnSetSkill);
    }

    public void Init()
    {
        for(int i = 0; i < SkillIcons.Count; i++)
        {
            SkillIcons[i].SetSkill(false);
        }
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
        for (int i = 0; i < SkillIcons.Count; i++)
        {
            if (SkillIcons[i].SkillName == skillName)
            {
                SkillIcons[i].SetSkill(true);
            }
        }
    }

    public void OnDestroy()
    {
        m_eventListener.StopListeningForEvent((int)EGameEvents.SetSkill);
    }
}
