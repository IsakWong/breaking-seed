using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class SlimeController : MonoBehaviour, ICanObtainItem
{


    public float WalkSpeed;
    public float TurnRate;
    public SlimeTrigger slimeTrigger;

    private Rigidbody _cachedRigidbody;
    private AudioSource _cachedAudioSource;
    private BoxCollider _cachedCollider;

    private float time = 0.0f;
    private Vector3 moveDirection;
    

    public ItemBehaviour OwningItem;

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
                if (OwningItem != null)
                {
                    BeginDropItem();
                }
                else
                {
                    BeginObtainItem();
                }
                break;
            default:
                break;
        }
    }

    private void OnKeyboardReleased(KeyCode keyCode)
    {
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
    
    public void ObtainItem(ItemBehaviour Item)
    {
        if (Item != null)
        {
            OwningItem = Item;
        }
    }
    public void DropItem(ItemBehaviour Item)
    {
        if (OwningItem != null)
        {
            OwningItem = null;
        }
    }

    public GameObject GetGO()
    {
        return gameObject;
    }

    public void BeginObtainItem()
    {
        if(slimeTrigger.TriggerItem != null)
        {
            slimeTrigger.TriggerItem.CurrentState = ItemBehaviour.ItemState.Obtaining;
            slimeTrigger.TriggerItem.Obtainer = this;
        }
    }
    public Vector3 GetObtainLocation()
    {
        return transform.GetChild(0).transform.position;
    }
    public void BeginDropItem()
    {
        if (OwningItem != null)
        {
            OwningItem.CurrentState = ItemBehaviour.ItemState.Discarding;
            OwningItem.Owner = null;
        }
    }
}
