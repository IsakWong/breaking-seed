using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{

    public GameObject LoadingMask;
    public String NextStringName;
    public float LoadingMinTime = 2f;


    private AsyncOperation _async;
    private float _loadingTime = 0;

	// Use this for initialization
	void Start ()
	{
	    StartCoroutine(Load());
	}

    IEnumerator Load()
    {
        _async = SceneManager.LoadSceneAsync(NextStringName,LoadSceneMode.Single);
        _async.allowSceneActivation = false;
        yield return _async;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    _loadingTime += Time.deltaTime;
	    if (_loadingTime > LoadingMinTime)
	    {
	        LoadingMask.SetActive(true);
            if (_async != null)
	        {
	            if (LoadingMask.GetComponent<Image>().color.a >=0.99f)
	            {
	                _async.allowSceneActivation = true;
	            }
            }
	    }
	}
}
