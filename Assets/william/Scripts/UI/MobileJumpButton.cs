using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJumpButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    private bool isSetting = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jump()
    {
        //Debug.Log("MobileJumpButton Jump");
        //PlayerInput.Instance.Jump.Down = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        MobileInput.JumpDown = true;
        StartCoroutine(RelaseJumpDown());
        MobileInput.JumpHeld = true;
        MobileInput.JumpUp = false;
    }

    IEnumerator RelaseJumpDown()
    {
        yield return new WaitForEndOfFrame();
        MobileInput.JumpDown = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        MobileInput.JumpDown = false;
        MobileInput.JumpHeld = false;
        MobileInput.JumpUp = true;
    }

    IEnumerator set()
    {
        yield return new WaitForEndOfFrame();
        MobileInput.JumpDown = false;
    }
}
