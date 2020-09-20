using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickKey : MonoBehaviour
{
    public Image btnImage;
    public string keyName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseQuickKey()
    {
        Debug.Log("UseQuickKey = " + keyName);
        if(!string.IsNullOrEmpty(keyName))
            PlayerCharacter.PlayerInstance.UseItem(keyName);
    }
}
