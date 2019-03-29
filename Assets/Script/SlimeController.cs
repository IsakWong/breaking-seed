using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class SlimeController : MonoBehaviour, ICanObtainItem
{


    public float WalkSpeed;
    public float TurnRate;

    private Rigidbody _cachedRigidbody;
    private AudioSource _cachedAudioSource;
    private BoxCollider _cachedCollider;

    private float time = 0.0f;
    private Vector3 moveDirection;
    

    public ItemBehaviour OwnedSeed;
    public ItemBehaviour TriggerSeed;

    public AudioClip WarningSFX;
    public AudioClip GulpSFX;

    public GameObject joint;



    Vector3 _dampTemp;
    private void AxisInputHandle(Vector3 move)
    {
        var v = Vector3.Slerp(-transform.forward, move, 0.1f);
        transform.rotation = Quaternion.FromToRotation(-Vector3.forward, v);
        move.x *= WalkSpeed;
        move.z *= WalkSpeed;
        move.y = _cachedRigidbody.velocity.y;
        _cachedRigidbody.velocity = move;

    }

    private void OnKeyboardPressed(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.E:
                if (OwnedSeed != null)
                {
                    OwnedSeed = null;
                }
                break;
            default:
                break;
        }
    }

    private void OnKeyboardReleased(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.E:
                if (OwnedSeed != null)
                {
                    DropItem(OwnedSeed, -transform.forward);
                    OwnedSeed = null;
                }
                else
                {
                    
                }
                break;
            default:
                break;
        }
    }

    void Start()
    {
        _cachedRigidbody = GetComponent<Rigidbody>();
        _cachedCollider = GetComponent<BoxCollider>();
        _cachedAudioSource = GetComponent<AudioSource>();
        _cachedAudioSource.loop = false;
        _cachedAudioSource.playOnAwake = false;
        InputManager.Current.OnKeyPressed += OnKeyboardPressed;
        InputManager.Current.OnKeyReleased += OnKeyboardReleased;
        InputManager.Current.OnAxisChanged += AxisInputHandle;
    }

    public void ObtainItem()
    {
        if(TriggerSeed != null)
        {
            TriggerSeed.CurrentState = ItemBehaviour.ItemState.Obtaining;
            TriggerSeed.Owner = this;
        }
    }

    public void DropItem()
    {
        if (OwnedSeed != null)
        {
            TriggerSeed.CurrentState = ItemBehaviour.ItemState.Discarding;
            TriggerSeed.Owner = this;
        }
    }

    private void OnTriggerEnter(Collider trigger)
    {
        TriggerSeed = trigger.GetComponent<ItemBehaviour>();
    }

    private void OnTriggerStay(Collider other)
    {

    }
    private void OnTriggerExit(Collider other)
    {
        if (TriggerSeed == other.GetComponent<ItemBehaviour>())
            TriggerSeed = null;
    }

    public GameObject GetGO()
    {
        return gameObject;
    }
    
}
