using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_cube : MonoBehaviour {

    public float left;
    public float right;

    private bool isLeft=true    ;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (isLeft)
        {
            this.transform.position -= new Vector3(1, 0, 0) * Time.deltaTime;
            if (this.transform.position.x < left)
                isLeft = false;

        }
        else
        {
            this.transform.position += new Vector3(1, 0, 0) * Time.deltaTime;
            if (this.transform.position.x >right)
                isLeft = true;
        }
    }
}
