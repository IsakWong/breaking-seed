using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water_drop : MonoBehaviour {


    public float lifetime=0.2f;

    private float time;
    public Vector3 des;
	// Use this for initialization
	void Start () {
        time = 0.0f;	
	}
	
	// Update is called once per frame
	void Update () {

        time += Time.deltaTime;

        Vector3 temp= new Vector3(0,0,0);
        this.transform.position = Vector3.SmoothDamp(this.transform.position, des, ref temp, 0.1f);

        if (time > lifetime)
        {
            Debug.Log("dddd");
            Object.Destroy(this.gameObject);
        }
           
        //this.transform.position += new Vector3(0, 1, 0);
    }
}
