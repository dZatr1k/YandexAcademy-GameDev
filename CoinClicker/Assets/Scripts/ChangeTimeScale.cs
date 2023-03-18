using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTimeScale : MonoBehaviour
{
    public void ChangeTimescale(float scale) 
    {
        Time.timeScale = scale;
    }
}
