using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager CurrentLevel = null;

    public static LevelManager GetCurrentLevelManager()
    {
        return CurrentLevel;
    }

    public LevelUI levelUI;

    // Use this for initialization
    void Start()
    {
        CurrentLevel = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
