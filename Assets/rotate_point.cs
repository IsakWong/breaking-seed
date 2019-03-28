using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_point : MonoBehaviour
{


    public float rotate_speed;

    private Vector3 point;
    private Vector3 axi;

    // Use this for initialization
    void Start()
    {
        point = GetComponentInChildren<Transform>().position;
        axi = new Vector3(0, 1, 0);
        //this.transform.TransformDirection(new Vector3(0, 1, 0));

    }

    // Update is called once per frame
    void Update()
    {
        //  this.transform.Rotate();
        this.transform.RotateAround(point, axi, Time.deltaTime * rotate_speed);
        //this.transform.Rotate(axi, Time.deltaTime * rotate_speed,Space.Self);
        // this.transform.Rotate(0, Time.deltaTime * rotate_speed, 0);
    }
}
