using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller_level4 : MonoBehaviour {

    
    public enum State
    {
        Get,
        Put,
        HaveSeed,
        HaveWater,
        Null
    }

    public State state = State.Null;


    public int water_time;

    public float water_speed = 1.0f;

    public GameObject water_drop;

    private character_water character_Water;

    private Rigidbody parent;

    private GameObject water;
    private GameObject seed;
    private GameObject hole;

    public GameObject joint;

    private bool rangeWater = false;
    private bool rangeSeed = false;
    private bool rangeHole = false;
    private bool rangeSeedIn = false;


    private bool isGettingSeed = false;
    private bool isPuttingSeed = false;
    private bool isGettingWater = false;
    private bool isPuttingWater = false;

    //private bool isMove = false;
 
    private bool seedIsGet=false;
    private bool waterIsGet = false;

    private float time_stemp;
    private int time_temp=0;

    private void GetObject(KeyCode keyCode)
    {

        if(keyCode==KeyCode.E)
        {
            if(rangeHole&&rangeSeedIn)//水放到坑里(浇水操作)
            {


                if (character_Water.water_chracter > 0 )
                {

                    Debug.Log("water seed");
                    float temp = Time.deltaTime * water_speed;

                    seed.GetComponent<Seed_level4>().num_water = character_Water.water_chracter;
                    character_Water.water_chracter = 0;
                    isPuttingWater = true;
                }
                rangeSeedIn = false;



                //水坑
                //rangeWater = true;

                //hole hole_ = hole.GetComponent<hole>();

                //float temp = character_Water.water_chracter;

                //if (hole_.Water >= hole_.MaxWater)//水坑是否满了
                //    return;


                ////if(hole_.Water<=0&&character_Water.water_chracter>0)//是否需要生成水的模型
                ////{
                ////    GameObject water_obj = Instantiate(water_p);
                ////    water_obj.transform.position = hole.transform.position;
                ////    water_obj.transform.parent = hole.transform;
                ////}

                //if (hole_.MaxWater<hole_.Water+temp)
                //{
                //    character_Water.water_chracter = temp - (hole_.MaxWater - hole_.Water);
                //    hole_.Water = hole_.MaxWater;
                //}
                //else
                //{
                //    hole_.Water += character_Water.water_chracter;
                //    character_Water.water_chracter = 0;
                //}

                //Debug.Log("water to hole");

            }

            else if (seedIsGet)//种子放到地上
            {
                
                rangeSeed = true;
                isPuttingSeed = true;

                seed.transform.SetParent(null);
               
                // seed.GetComponent<Seed_level4>().CheckHole();
                state = State.Put;

                putposition =// hole.transform.position;
                    this.transform.position+ this.transform.TransformDirection( new Vector3(1.2f,0,0));
                // this.transform.DetachChildren();
                Debug.Log("seed on ground");

            }
        }
    }

    private void WaterSeed(KeyCode keyCode)
    {
        if (keyCode == KeyCode.F)
        {
            Debug.Log("water seed");

            if(character_Water.water_chracter>0&&rangeSeed)
            {
                float temp = Time.deltaTime * water_speed;

                seed.GetComponent<Seed_level4>().num_water += temp;
                character_Water.water_chracter -=temp;
            }

          
           // seed.GetComponent<Seed_level4>().num_water += temp;
        }
    }

    // Use this for initialization
    void Start () {

        
        InputManager.Current.OnKeyPressed += GetObject;
        InputManager.Current.OnKeyHold += WaterSeed;

        character_Water = GetComponentInChildren<character_water>();
      //  parent = GetComponentInParent<>();
        
    }

    private void FixedUpdate()
    {
        WaterUI.WaterDisplay.SetWaterFloat(character_Water.water_chracter);
    }
    // Update is called once per frame
    void Update () {



        if (rangeWater||rangeSeed)
        {
           if(state==State.Null)
            {
                if (rangeSeed&&character_Water.water_chracter<=0)//拾取种子
                {
                    isGettingSeed = true;
                    seed.transform.parent = joint.transform;
                   // seed.GetComponent<Seed_level4>().IsSelected = true;
                    

                    Destroy(seed.GetComponentInChildren<Floating>());
                        //this.transform;
                    state = State.Get;

                    Debug.Log("Get seed ");
                }
                else if (rangeWater)//拾取水
                {
                    character_Water.water_chracter += water.GetComponent<water>().Water;
                    state = State.Get;

                    isGettingWater = true;
                  
                   // hole.GetComponent<hole>().Water = 0;
                 
                    Debug.Log("Get water ");
                }
            }

            if(state == State.HaveWater)
            {
                if (rangeWater )//拾取水
                {
                    character_Water.water_chracter += water.GetComponent<water>().Water;
                    state = State.Get;

                    isGettingWater = true;
                   
                    //设置取走的水
                    //water.GetComponent<water>().Water = 0;
                  
                    Debug.Log("Get water ");
                }
            }
        }

        Process();

        if (character_Water.water_chracter <= 0&&state==State.HaveWater)
            state = State.Null;
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.tag=="seed_in")
        {
            seed = other.gameObject;
            rangeSeedIn = true;
        }

        if(other.tag=="seed")
        {
            seed = other.gameObject;
           
            rangeSeed = true;
        }

        if (other.tag == "water")
        {
            water = other.gameObject;
            rangeWater = true;
        }

        if(other.tag=="hole")
        {
            hole = other.gameObject;
            rangeHole = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "seed_in")
        {
            rangeSeedIn = false;
        }

        if (other.tag == "seed")
        {
            rangeSeed = false;
        }
          

        if (other.tag == "water")
            rangeWater = false;

        if (other.tag == "hole")
            rangeHole = false;
    }



    private Vector3 putposition;

    private void Process()
    {
        if (isGettingSeed)
        {
            Vector3 temp = new Vector3(0, 0, 0);
            seed.transform.position = Vector3.SmoothDamp(seed.transform.position, this.transform.position, ref temp, 0.1f);

            if((this.transform.position + new Vector3(0,-0,0)-seed.transform.position).magnitude<.1f)
            {
                isGettingSeed = false;
                seedIsGet = true;
                state = State.HaveSeed;
                Debug.Log("HaveSeed");

                ///
            }
           
        }

        else if (isPuttingSeed)
        {
            Vector3 temp = new Vector3(0.1f,0.1f,0.1f);

            seed.transform.position = Vector3.SmoothDamp(seed.transform.position, putposition, ref temp, 0.1f);


            if ((putposition - seed.transform.position).magnitude < 0.2f)
            {

                seed.GetComponent<Seed_level4>().CheckHole();
                //seed.GetComponent<Seed_level4>().IsSelected = false;
                isPuttingSeed = false;
                seedIsGet = false;
                state = State.Null;
                Debug.Log("Put Seed");
            }
            // seedIsGet =
        }

        else if(isGettingWater)
        {
            Vector3 temp = new Vector3(0, 0, 0);

            water.transform.localScale = Vector3.SmoothDamp(water.transform.localScale, new Vector3(0,0,0), ref temp, 0.05f);

            if((water.transform.localScale - new Vector3(0,0,0)).magnitude<0.1)
            {
                Destroy(water);
                waterIsGet = true;
                isGettingWater = false;

                rangeWater = false;
                state = State.HaveWater;
            }
        }

        else if(isPuttingWater)
        {
            //time_stemp += Time.deltaTime;
            //if (time_stemp > 0.2)
            //{

                GameObject water_d = Instantiate(water_drop);
                water_d.transform.position = this.transform.position;
                water_d.GetComponent<water_drop>().des = seed.transform.position;

            waterIsGet = false;
            // time_stemp = 0;
            isPuttingWater = false;

            //   // time_temp++;
            //}
           // if (time_temp >= water_time)
           //     isPuttingWater = false;
                
        }
        //{

        //    GameObject water_d =  Instantiate(water_drop);
        //    water_d.GetComponent<water_drop>().des = seed.transform.position;
        //    time_stemp += Time.deltaTime;

        //    if(time_stemp>water_time)
        //    {
        //        isPuttingWater = false;
        //    }
        //}

    }
}
