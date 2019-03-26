using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destence : MonoBehaviour {


    private bool GetMinShadow = false;
    public float water_speed=3;
    Vector3 p;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (GetMinShadow)
        {
            float d = (p - this.transform.position).magnitude;
           // Debug.Log(d.ToString());
            if (d < 4)
                water_speed = 1;
            else if (d < 8 && d >= 4)
                water_speed = 2;
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "shadow")
        {
            p = other.ClosestPoint(this.transform.position);

            GetMinShadow = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "shadow")
        {
            //p = other.ClosestPoint(this.transform.position);

            GetMinShadow = false;
            water_speed = 3;
        }
    }

}
