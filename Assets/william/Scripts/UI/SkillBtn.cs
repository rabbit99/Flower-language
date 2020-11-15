using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBtn : MonoBehaviour
{
    public GameObject iconObj;
    public Button iconBtn;
    public string SkillName = string.Empty;
    // Start is called before the first frame update
    void Start()
    {
        iconBtn.onClick.AddListener(UseSkill);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        iconBtn.onClick.RemoveListener(UseSkill);
    }

    public void SetSkill(bool active)
    {
        if (active)
        {
            iconObj.SetActive(true);
            iconBtn.interactable = true;
        }
        else
        {
            iconObj.SetActive(false);
            iconBtn.interactable = false;
        }
    }

    public void UseSkill()
    {
        //
        PlayerCharacter.PlayerInstance.UseSkill(SkillName);
    }
}
