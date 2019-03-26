using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedBehaviour : MonoBehaviour {

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

    public enum SeedState : uint
    {
        OnGround,
        Obtaining,
        Discarding,
        Obtained
    }

    public ICanObtainSeed Obtainer = null;
    public ICanObtainSeed Owner = null;

    public SeedState CurrentState = SeedState.OnGround;

	void Start () {
        _audio = GetComponent<AudioSource>();
        _audio.loop = false;
    }

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
            case SeedState.OnGround:
                break;
            case SeedState.Obtaining:
                GetComponentInParent<Rigidbody>().useGravity = false;
                Vector3 oldPosition = transform.parent.transform.position;
                Vector3 targetPosition = Obtainer.GetGO().transform.position;
                Vector3 newPosition = Vector3.SmoothDamp(oldPosition, targetPosition, ref _tmpObtainSpeed, _obtainTime);
                _obtainTime -= Time.fixedDeltaTime;
                if ((transform.parent.transform.position - targetPosition).magnitude < 0.01)
                {
                    Owner = Obtainer;
                    Obtainer.GetSeed(this);
                    CurrentState = SeedState.Obtained;
                }
                transform.parent.transform.position = newPosition;
                break;
            
            case SeedState.Discarding:
                _discardTime += Time.fixedDeltaTime;
                if (_discardTime >= 0.3f)
                {
                    CurrentState = SeedState.OnGround;
                }
                break;
            case SeedState.Obtained:
                transform.parent.transform.position = Owner.GetGO().transform.position;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Obtain(ICanObtainSeed obtainer)
    {
        switch (CurrentState)
        {
            case SeedState.OnGround:
                
                Obtainer = obtainer;
                _obtainTime = 0.5f;
                CurrentState = SeedState.Obtaining;
                break;
            case SeedState.Obtained:
                if (obtainer == Obtainer)
                    break;
                Obtainer = obtainer;
                _obtainTime = 0.5f;
                CurrentState = SeedState.Obtaining;
                break;
        }
    }
    public void Discard(ICanObtainSeed owner, Vector3 discardDirection)
    {
        GetComponentInParent<Rigidbody>().AddForce(discardDirection * 1, ForceMode.VelocityChange);
        switch (CurrentState)
        {
            case SeedState.Obtained:
                this.discardDirection = discardDirection;
                Obtainer = null;
                Owner = null;
                CurrentState = SeedState.Discarding;
                _discardTime = 0f;
                break;
        }

    }

    void Update () {
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
