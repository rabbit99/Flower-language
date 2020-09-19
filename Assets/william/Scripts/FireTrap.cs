using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    public float time_gate = 2f;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHitDamageable(Damager origin, Damageable damageable)
    {
        //Debug.Log("fire wall is OnHitDamageable " + origin.name + "  " + damageable.name);
    }

    public void OnHitNonDamageable(Damager origin)
    {
        //FindSurface(origin.LastHit);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        NotificationCenter.Default.Post(this, NotificationKeys.OnFire, collision.gameObject.name);
        //Debug.Log("FireTrap Trigger In, " + collision.gameObject.name);
        timer = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("FireTrap Trigger Stay, " + other.gameObject.name);
        timer += Time.deltaTime;
        if(timer >= time_gate)
        {
            NotificationCenter.Default.Post(this, NotificationKeys.OnFire, other.gameObject.name);
            timer = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        NotificationCenter.Default.Post(this, NotificationKeys.OutOfFire, collision.gameObject.name);
        Debug.Log("FireTrap Trigger Out, " + collision.gameObject.name);
        timer = 0;
    }
}
