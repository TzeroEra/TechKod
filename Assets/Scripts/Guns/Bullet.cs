using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2.0f;
    public int damage = 10; // Додайте властивість damage

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Обробка подій при зіткненні патрона з іншим об'єктом, якщо потрібно
    }
}