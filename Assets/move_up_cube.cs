using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_up_cube : MonoBehaviour {


    public float up;
    public float down;

    private bool isUp=false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if(!isUp)
        {
            this.transform.position += new Vector3(0, Time.deltaTime, 0);
            if (this.transform.position.y > up)
                isUp = true;
        }
        else
        {
            this.transform.position -= new Vector3(0, Time.deltaTime, 0);
            if (this.transform.position.y < down)
                isUp = false;
        }
    }
}
