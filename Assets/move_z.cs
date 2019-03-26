using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_z : MonoBehaviour {

    // Use this for initialization
    public float left;
    public float right;
    public float speed;

    private bool isLeft = true;
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (isLeft)
        {
            this.transform.position -= new Vector3(0, 0, 1) * Time.deltaTime*speed;
            if (this.transform.position.z < left)
                isLeft = false;

        }
        else
        {
            this.transform.position += new Vector3(0, 0, 1) * Time.deltaTime*speed;
            if (this.transform.position.z > right)
                isLeft = true;
        }
    }
}
