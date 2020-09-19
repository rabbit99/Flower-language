using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public float AtkCDTime = 5f;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        InvokeRepeating("AutoAttack", 3, AtkCDTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AutoAttack()
    {
        animator.SetTrigger("Atk");
    }
}
