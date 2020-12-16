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
    public float cooldownTime = 0;
    public Text cooldownText;

    private CooldownTimer CooldownTimer;
    // Start is called before the first frame update
    void Start()
    {
        CooldownTimer = new CooldownTimer(cooldownTime);
        CooldownTimer.TimerCompleteEvent += ActiveSkill;
        iconBtn.onClick.AddListener(UseSkill);
    }

    // Update is called once per frame
    void Update()
    {
        if (CooldownTimer.IsActive)
        {
            CooldownTimer.Update(Time.deltaTime);
            cooldownText.text = CooldownTimer.TimeRemaining.ToString("0.0");
        }

    }

    private void OnDestroy()
    {
        iconBtn.onClick.RemoveListener(UseSkill);
        CooldownTimer.TimerCompleteEvent -= ActiveSkill;
    }

    public void SetSkill(bool active)
    {
        iconObj.SetActive(active);
        iconBtn.interactable = active;
    }

    public void UseSkill()
    {
        //
        PlayerCharacter.PlayerInstance.UseSkill(SkillName);
        iconBtn.interactable = false;
        CooldownTimer.Start();
        cooldownText.gameObject.SetActive(true);
    }

    private void ActiveSkill()
    {
        iconBtn.interactable = true;
        cooldownText.gameObject.SetActive(false);
    }
}
