using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class myEventTriggerOnEnter : MonoBehaviour
{
    public UnityEvent myEvents;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (myEvents == null)
        {
            print("myEventTriggerOnEnter was triggered but myEvents was null.");
        }
        else
        {
            print("myEventTriggerOnEnter Activated. Triggering "+myEvents);
            myEvents.Invoke();
        }
    }

    

}
