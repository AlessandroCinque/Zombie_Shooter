using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 3;
    public int _damage;
    public float lifeTime = 2.0f;

    public void Fire(Vector2 direction, int damage)
    {
        _damage = damage;
        var rigidBody = GetComponent<Rigidbody2D>();
        if (rigidBody)
        {
            direction.Normalize();
            transform.right = direction;
            rigidBody.velocity = direction *speed;
        }
        Destroy(gameObject, lifeTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ideally Enemy get a reference from Hitpoints and then Hitpoints does the things when it gets to 0.
        if (collision.CompareTag("Enemy") || collision.CompareTag("Player"))
        {
            var hitPoints = collision.gameObject.GetComponent<HitPoints>();
            if (hitPoints)
            {
                hitPoints.AdjustHealth(-_damage);
                Destroy(gameObject);
            }
        }
        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
