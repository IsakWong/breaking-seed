using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_water : MonoBehaviour {


    public float water_chracter = 0;
    //public float range = 3;
    public float water_speed = 1.0f;
    public bool isInSun = true;

    
    //public CapsuleCollider sunCollider;

    // Use this for initialization
    void Start () {
        //sunCollider = GetComponentInChildren<CapsuleCollider>();
        
	}
	
	// Update is called once per frame
	void Update () {
        //sunCollider.radius = range;


      //  water_speed = GetComponentInChildren<destence>().water_speed;


        if (isInSun && water_chracter > 0)
        {
            water_chracter -= water_speed * Time.deltaTime;
        }

    }




}
