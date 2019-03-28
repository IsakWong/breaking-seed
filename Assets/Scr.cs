using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr : MonoBehaviour
{

    public float TimeFactor = 1f;
    public float WaveTime = 2f;
    public float WaveMax = 2;

    private float _max = 0;
    private float _passedTime = 0;

    private void Awake()
    {
        _cachedMaterial = GetComponent<MeshRenderer>().material;
    }
    // Use this for initialization
    private void OnEnable()
    {
        _cachedMaterial.SetFloat("_StartTime", Time.time);
        _cachedMaterial.SetFloat("_TimeFactor", TimeFactor);
        _passedTime = WaveTime;

    }

    private Material _cachedMaterial;

    public float t = 2;
    // Update is called once per frame
    void Update()
    {
        _max = WaveMax * _passedTime / WaveTime;
        _cachedMaterial.SetFloat("_Max", _max);
        _passedTime -= Time.deltaTime;
        if (_passedTime < 0)
            enabled = false;

    }
}
