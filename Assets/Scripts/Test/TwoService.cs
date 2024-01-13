using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoService : MonoBehaviour, IService
{
    public void Test()
    {
        Debug.Log("TwoService");
    }
}