using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed_level4 : MonoBehaviour {

    public float need_water=10.0f;
    public float num_water=0;
    public seed_type type;
    //public bool IsSelected = false;

    public GameObject hole;
    private bool isHole = false;
    private bool finished=false;
	// Use this for initialization
    public enum seed_type
    {
        putong,
        tantiao
    }

	void Start () {

       // this.GetComponent<Rigidbody>().e
    }

    private void FixedUpdate()
    {
       
    }

    // Update is called once per frame
    void Update () {
		if(num_water>=need_water)
        {
            //生长
            
            if(type==seed_type.putong)
            {
                Debug.Log("seed is run");
                GameObject tree = (GameObject)Instantiate(Resources.Load("生长平台树_p"));
                tree.transform.position = hole.transform.position + new Vector3(0, -0.12f, 0);

                GameObject pong = (GameObject)Instantiate(Resources.Load("PONG"));
                pong.transform.position = hole.transform.position;

                GameObject yw = (GameObject)Instantiate(Resources.Load("烟雾效果"));
                yw.transform.position = hole.transform.position;

                Destroy(this.gameObject);
            }
            else if(type==seed_type.tantiao)
            {
                Debug.Log("mianhua");
                GameObject tree = (GameObject)Instantiate(Resources.Load("弹跳棉花树"));
                tree.transform.position = hole.transform.position;
                Destroy(this.gameObject);
            }
            // LoadAssetAtPath<GameObject>("Resource");
            


        }

        if (isHole && !finished)
        {
            Vector3 temp = new Vector3(0, 0, 0);
            this.transform.position = Vector3.SmoothDamp(this.transform.position, hole.transform.position, ref temp, 0.1f);

            if ((transform.position - hole.transform.position).magnitude < 0.1)
            {
                finished = true;
                Debug.Log("seed in");

                GetComponent<BoxCollider>().enabled = true;
                transform.parent = hole.transform;
                //this.gameObject.GetComponentInChildren<GameObject>().AddComponent<Floating>();
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="hole")
        {
          
            hole = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
       
        if(other.tag=="hole")
       {
            
            hole = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "hole")
        {
            hole = other.gameObject;
        }
    }

    public void CheckHole()
    {
        Debug.Log("check");
        if (hole != null)
        {
            
            this.tag = "seed_in";
            isHole = true;
            this.GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            this.gameObject.AddComponent<Floating>();
            isHole = false;
        }
    }
}
