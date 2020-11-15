using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventListener
{
    //return true if you wish to "eat" the event
    EventResult OnEvent(int eventId, object data);
}
