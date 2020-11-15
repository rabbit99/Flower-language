using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenusFlytrap : MonoBehaviour
{
    public LineRenderer LR;
    public Transform headPos;
    public Transform bodyPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LR.SetPosition(0, headPos.position);
        LR.SetPosition(1, bodyPos.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
