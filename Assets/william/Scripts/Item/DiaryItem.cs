using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryItem : MonoBehaviour
{
    public int DiaryIndex;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(Services.Get<DataManager>().CheckDiary(DiaryIndex));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckSelf()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Services.Get<EventManager>().SendEvent((int)EGameEvents.ShowDiary, DiaryIndex);
            Services.Get<DataManager>().SaveDiary(DiaryIndex);
            gameObject.SetActive(false);
        }
    }
}
