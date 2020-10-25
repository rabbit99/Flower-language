using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotweenTest : MonoBehaviour
{
    public DOTweenPath[] dop;

    private DOTweenPath _downDtp;
    private DOTweenPath _upDtp;
    // Start is called before the first frame update
    void Start()
    {
        //dop.DOPlay();
        //dop.
        dop = GetComponents<DOTweenPath>();
        for(int i = 0; i < dop.Length +1; i++)
        {
            if(dop[i].id == "1")
            {
                _downDtp = dop[i];
            }
            else if (dop[i].id == "2")
            {
                _upDtp = dop[i];
            }


        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            _upDtp.DOPlay();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _downDtp.DOPlay();
        }

    }
}
