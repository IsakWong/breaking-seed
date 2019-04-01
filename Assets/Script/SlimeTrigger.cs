using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTrigger : MonoBehaviour
{

    public ItemBehaviour TriggerItem;

    private void OnTriggerEnter(Collider trigger)
    {
        TriggerItem = trigger.gameObject.GetComponent<ItemBehaviour>();
    }
    private void OnTriggerStay(Collider trigger)
    {
        TriggerItem = trigger.gameObject.GetComponent<ItemBehaviour>();
    }
    private void OnTriggerExit(Collider other)
    {
        if (TriggerItem == other.gameObject.GetComponent<ItemBehaviour>())
            TriggerItem = null;
    }
}
