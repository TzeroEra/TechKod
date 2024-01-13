using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperService : MonoBehaviour, IService
{
    public void Test()
    {
        Debug.Log("SuperService");
    }
}