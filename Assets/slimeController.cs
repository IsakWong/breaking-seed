using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeController : MonoBehaviour {


    public float walk_speed;
    public float rotation_speed;

    

    public bool isshadow;
    private Rigidbody m_rigidbody;

    private float time = 0.0f;
    // private Animator animator;

    private Vector3 d;
    private Vector3 v = Vector3.zero;
    private float m_TurnAmount=0;

    private bool isRotate = false;


    private void Move(Vector3 move)
    {
        var s1 = move * walk_speed;
        var s2 = m_rigidbody.velocity;
        s2.x = s1.x;
        s2.z = s1.z;
        m_rigidbody.velocity = s2;
        //转向

        d =- transform.InverseTransformDirection(move);

        m_TurnAmount = Mathf.Atan2(d.x, d.z);

       if(d.magnitude!=0)
        {
            isRotate = true;
        }
       else
        {
            isRotate = false;
        }
        //m_rigidbody.AddForce(move);
        // parent.transform.position += move * walk_speed/10;
        //Debug.Log(move.ToString());
        // parent.AddForce(move * walk_speed*100,ForceMode.Force);

        //this.transform.position += move*walk_speed/10;
    }

    // Use this for initialization
    void Start () {
        InputManager.Current.OnAxisChanged += Move;
        m_rigidbody = GetComponent<Rigidbody>();
        //animator = GetComponent<Animator>();
       // rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        m_rigidbody.WakeUp();
    }
    // Update is called once per frame
    void Update () {

        if (isRotate)
        {
            Vector3 d_w = this.transform.TransformDirection(new Vector3(0, 0, 1));
            Vector3 temp = Vector3.SmoothDamp(d_w, d, ref v, 1f);


            this.transform.Rotate(0, m_TurnAmount * rotation_speed * Time.deltaTime * 50.0f, 0);
        }
       
       

        time += Time.deltaTime;

		if(time>2.0f)
        {
           // m_rigidbody.AddForce(new Vector3(0, 300.0f, 0));
           // Debug.Log("jump");
          
            time = 0;
        }
	}

    
}
