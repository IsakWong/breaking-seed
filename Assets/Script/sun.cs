using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sun : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="character")
        {
           // Debug.Log("trigger");
            other.GetComponent<character_water>().isInSun = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "character")
        {
           // Debug.Log("trigger");
            other.GetComponent<character_water>().isInSun = true;
        }
    }
}
