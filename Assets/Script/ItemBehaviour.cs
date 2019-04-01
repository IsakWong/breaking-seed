using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour {

    public enum TreeSeedType : uint
    {
        GrowingTreeSeed,
        JumpTreeSeed,
        UnKnown
    }

    public float GrowNeedWater = 10.0f;
    public float num_water=0;

    public TreeSeedType TreeType;
    public GameObject hole;
    private AudioSource _audio;
    public SlimeTrigger slimeTrigger;

    public enum ItemState : uint
    {
        OnGround,
        Obtaining,
        Discarding,
        Obtained
    }

    public ICanObtainItem Obtainer = null;
    public ICanObtainItem Owner = null;

    private ItemState currentState = ItemState.OnGround;

    public ItemState CurrentState
    {
        get
        {
            return currentState;
        }
        set
        {
            switch(value)
            {
                case ItemState.Obtaining:
                    GetComponentInChildren<Floating>().SetFloatingEnable(false);
                    break;
                case ItemState.Obtained:
                    GetComponentInChildren<Floating>().SetFloatingEnable(false);
                    break;
                case ItemState.OnGround:
                    GetComponentInChildren<Floating>().SetFloatingEnable(true);
                    break;
            }
            currentState = value;
        }
    }
	void Start () {
        _audio = GetComponent<AudioSource>();
        _audio.loop = false;
    }

    private Vector3 _velocity;
    private float _obtainTime = 0.5f;
    private Vector3 _tmpObtainSpeed = Vector3.zero;
    private Vector3 _tmpDiscardSpeed = Vector3.zero;
    private float _discardTime = 0.2f;
    private float _discardSpeed = 3f;
    private Vector3 discardDirection = Vector3.zero;


    private void FixedUpdate()
    {
        switch (CurrentState)
        {
            case ItemState.OnGround:
                break;
            case ItemState.Obtaining:
                Vector3 oldPosition = transform.position;
                Vector3 targetPosition = Obtainer.GetObtainLocation();
                Vector3 newPosition = Vector3.SmoothDamp(oldPosition, targetPosition, ref _tmpObtainSpeed, _obtainTime);
                _obtainTime -= Time.fixedDeltaTime;
                if ((transform.position - targetPosition).magnitude < 0.01)
                {
                    Owner = Obtainer;
                    Obtainer.ObtainItem(this);
                    CurrentState = ItemState.Obtained;
                }
                transform.position = newPosition;
                break;
            
            case ItemState.Discarding:
                _discardTime += Time.fixedDeltaTime;
                if (_discardTime >= 0.3f)
                {
                    CurrentState = ItemState.OnGround;
                }
                break;
            case ItemState.Obtained:
                transform.position = Obtainer.GetObtainLocation();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Obtain(ICanObtainItem obtainer)
    {
        switch (CurrentState)
        {
            case ItemState.OnGround:
                
                Obtainer = obtainer;
                _obtainTime = 0.5f;
                CurrentState = ItemState.Obtaining;
                break;
            case ItemState.Obtained:
                if (obtainer == Obtainer)
                    break;
                Obtainer = obtainer;
                _obtainTime = 0.5f;
                CurrentState = ItemState.Obtaining;
                break;
        }
    }
    public void Discard(ICanObtainItem owner, Vector3 discardDirection)
    {
        GetComponentInParent<Rigidbody>().AddForce(discardDirection * 1, ForceMode.VelocityChange);
        switch (CurrentState)
        {
            case ItemState.Obtained:
                this.discardDirection = discardDirection;
                Obtainer = null;
                Owner = null;
                CurrentState = ItemState.Discarding;
                _discardTime = 0f;
                break;
        }

    }

    void Update () {
        switch(CurrentState)
        {
            case ItemState.OnGround:
                break;
            case ItemState.Obtaining:
                Vector3 target = Obtainer.GetGO().transform.position;
                Vector3 newPos = Vector3.SmoothDamp(transform.position, target, ref _velocity, _obtainTime);
                transform.position = newPos;
                break;
            case ItemState.Discarding:
                break;
            case ItemState.Obtained:
                break;
            default:
                break;

        }
		if(num_water>=GrowNeedWater)
        {
            //生长
            if(TreeType==TreeSeedType.GrowingTreeSeed)
            {
                _audio.Play();
                GameObject tree = (GameObject)Instantiate(Resources.Load("生长平台树_p"));
                tree.transform.position = hole.transform.position + new Vector3(0, -0.12f, 0);

                GameObject pong = (GameObject)Instantiate(Resources.Load("PONG"));
                pong.transform.position = hole.transform.position;

                GameObject yw = (GameObject)Instantiate(Resources.Load("烟雾效果"));
                yw.transform.position = hole.transform.position;

                Destroy(this.gameObject);

                
            }

            if(TreeType==TreeSeedType.JumpTreeSeed)
            {
                _audio.Play();
                GameObject tree = (GameObject)Instantiate(Resources.Load("弹跳棉花树"));
                tree.transform.position = hole.transform.position;
                Destroy(this.gameObject);
                
            }
            // LoadAssetAtPath<GameObject>("Resource");

        }
     
    }
}
