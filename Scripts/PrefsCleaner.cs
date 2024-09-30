using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefsCleaner : MonoBehaviour
{
    public bool Clean;
    void Start()
    {
        if (Clean)
        {
            PlayerPrefs.SetInt("TotalScore", 0);
            PlayerPrefs.SetInt("BestRounds", 0);
        }
    }
}
