using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chuansongdai : MonoBehaviour {


    public float force;
    // Use this for initialization
    List<GameObject> gameObjects;


	void Start () {

        gameObjects = new List<GameObject>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        
        if (other.tag=="Player"||other.tag=="seed")
        {
            other.GetComponent<Rigidbody>().AddForce(0, 0, force);
            Debug.Log("!!!!");
        }
       
    }


}
