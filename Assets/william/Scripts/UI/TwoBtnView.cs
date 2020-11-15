using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwoBtnView : BtnView
{
    public List<GameObject> btns;
    private int _index = 0;
    public override void SetBtn(string name)
    {
        if(_index < btns.Count)
        {
            btns[_index].GetComponentInChildren<Text>().text = name;
            _index++;
        }
    }
    public override void ResetBtn()
    {
        _index = 0;
        this.gameObject.SetActive(false);
    }
}
