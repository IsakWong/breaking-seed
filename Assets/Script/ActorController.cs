using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class ActorController : MonoBehaviour, ICanObtainSeed
{


    public float walk_speed;
    public float rotation_speed;

    private Rigidbody _cachedRigidbody;
    private AudioSource _cachedAudioSource;
    private BoxCollider _cachedCollider;

    private float time = 0.0f;
    private Vector3 moveDirection;

    public enum State
    {
        Get,
        Put,
        HaveSeed,
        HaveWater,
        Null
    }

    public State state = State.Null;

    public SeedBehaviour OwnedSeed;
    public SeedBehaviour TriggerSeed;

    public AudioClip WarningSFX;
    public AudioClip GulpSFX;

    public GameObject joint;



    Vector3 _dampTemp;
    private void AxisInputHandle(Vector3 move)
    {
        var v = Vector3.Slerp(-transform.forward, move, 0.1f);
        transform.rotation = Quaternion.FromToRotation(-Vector3.forward, v);
        move.x *= walk_speed;
        move.z *= walk_speed;
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
                    DiscardSeed(OwnedSeed, -transform.forward);
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
                    DiscardSeed(OwnedSeed, -transform.forward);
                    OwnedSeed = null;
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


    private void OnTriggerEnter(Collider trigger)
    {
        TriggerSeed = trigger.GetComponent<SeedBehaviour>();
        if (TriggerSeed != null)
        {
            ObtainSeed(TriggerSeed);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        TriggerSeed = other.GetComponent<SeedBehaviour>();
        if (TriggerSeed != null)
        {
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (TriggerSeed == other.GetComponent<SeedBehaviour>())
            TriggerSeed = null;
    }


    public GameObject GetGO()
    {
        return gameObject;
    }

    public void ObtainSeed(SeedBehaviour seed)
    {
        seed.Obtain(this);
    }

    public void DiscardSeed(SeedBehaviour seed, Vector3 discardDirection)
    {
        seed.Discard(this, discardDirection);
    }

    public void GetSeed(SeedBehaviour seed)
    {
        this.OwnedSeed = seed;
    }

    public void OwningSeed(SeedBehaviour seed)
    {

    }
}
