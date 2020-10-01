using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour
{
    public BoxCollider2D _b;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //MovementWithAsset movement = collision.GetComponent<MovementWithAsset>();
        //if (movement != null)
        //{
        Debug.Log("PlayerInput.Instance.Vertical.Value = " + PlayerInput.Instance.Vertical.Value);
        if(PlayerInput.Instance.Vertical.Value == 1f || PlayerInput.Instance.Vertical.Value == -1f)
        {
            NotificationCenter.Default.Post(this, NotificationKeys.InTheLadder, collision.gameObject.name);
            Debug.Log("Ladder Trigger In, " + collision.gameObject.name);
            _b.enabled = false;
        }

        //}
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (PlayerInput.Instance.Vertical.Value == 1f || PlayerInput.Instance.Vertical.Value == -1f)
        {
            NotificationCenter.Default.Post(this, NotificationKeys.InTheLadder, collision.gameObject.name);
            Debug.Log("Ladder Trigger In, " + collision.gameObject.name);
            _b.enabled = false;
        }
        if (PlayerInput.Instance.Jump.Down)
        {
            NotificationCenter.Default.Post(this, NotificationKeys.OutTheLadder, collision.gameObject.name);
            Debug.Log("Ladder Trigger In, " + collision.gameObject.name);
            _b.enabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //MovementWithAsset movement = collision.GetComponent<MovementWithAsset>();
        //if (movement != null)
        //{
        NotificationCenter.Default.Post(this, NotificationKeys.OutTheLadder, collision.gameObject.name);
        Debug.Log("Ladder Trigger Out, " + collision.gameObject.name);
        //}

    }
}
