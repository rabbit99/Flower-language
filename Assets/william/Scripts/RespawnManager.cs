using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public Transform nextSpawnPosition;
    public Transform nowSpawnPosition;
    private bool _switch = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNewPos()
    {
        if (!_switch)
        {
            nowSpawnPosition.localPosition = nextSpawnPosition.localPosition;
            _switch = true;
        }
    }
}
