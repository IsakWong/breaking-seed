using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    private Vector3 _offset = Vector3.zero;
    private Vector3 _startPosition = Vector3.zero;

    public float y = 0.1f;
    public float x = 0f;

    private Vector3 _targetOffset;

    public float SmoothTime = 1f;
    public float MaxSpeed = 1.5f;
   
    private bool up = true;
	// Use this for initialization
	void Start ()
	{
        _startPosition = transform.localPosition;
        _targetOffset = _offset;
	    _targetOffset.y += y;

	}

    public void SetFloatingEnable(bool value)
    {
        if(value)
        {
            _targetOffset = _offset;
            _targetOffset.y += y;
        }
        else
        {
            _offset = Vector3.zero;
            _targetOffset = Vector3.zero;
            y = 0;
        }
    }
    private Vector3 _upSpeed;

    private Vector3 _downSpeed;
	// Update is called once per frame
	void Update () {
	    if (up)
	    {
            _offset = Vector3.SmoothDamp(_offset, _targetOffset, ref _upSpeed, SmoothTime, MaxSpeed);
            Vector3 pos = transform.localPosition;
            pos.y = _startPosition.y;
            pos += _offset;
            transform.localPosition = pos;
            if (Vector3.Magnitude(_offset - _targetOffset) < 0.01)
	        {
                _targetOffset.y = -2 * y;
                _targetOffset.x = -2 *x;
                up = false;
	        }
        }
	    else
	    {
	        _offset = Vector3.SmoothDamp(_offset, _targetOffset, ref _downSpeed, SmoothTime, MaxSpeed);
            Vector3 pos = transform.localPosition;
            pos.y = _startPosition.y;
            pos += _offset;
            transform.localPosition = pos;
            if (Vector3.Magnitude(_offset - _targetOffset) < 0.01)
	        {
                _targetOffset.y = 2 * y;
                _targetOffset.x = 2 * x;
                up = true;
            }
        }
	}
}
