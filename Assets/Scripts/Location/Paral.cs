using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paral : MonoBehaviour
{
    public Transform player;        

    public float parallaxEffect = 1f;   

    private float startPosX;      

    void Start()
    {
        startPosX = transform.position.x;
    }

    void Update()
    {
        float distanceX = (player.position.x - startPosX) * parallaxEffect;
        transform.position = new Vector3(startPosX + distanceX, transform.position.y, transform.position.z);
    }
}
