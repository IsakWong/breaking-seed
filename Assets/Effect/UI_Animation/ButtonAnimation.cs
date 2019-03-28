using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnEnter(BaseEventData data)
    {
        GetComponent<Animator>().SetBool("leave", false);
        GetComponent<Animator>().SetBool("enter", true);
    }

    public void OnLeave(BaseEventData data)
    {
        GetComponent<Animator>().SetBool("leave",true);
        GetComponent<Animator>().SetBool("enter", false);
    }
}
