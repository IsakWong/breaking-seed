using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tree_mianhua : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Enter collision");
        collision.gameObject.GetComponent<Rigidbody>().AddForce(0, 550, 0);
    }
}
