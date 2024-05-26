using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace Gameplay
{
    public class Bubble : BlowableObject
    {
        [SerializeField] float veloCouter;
        protected virtual void Pop()
        {
            Debug.Log(gameObject.name + " Pop!");
        }
        protected virtual void OnCollisionStay2D(Collision2D collision)
        {
            rb.velocity = new Vector3(Mathf.Clamp(collision.contacts[0].normal.x * 1000, -veloCouter, veloCouter), Mathf.Clamp(collision.contacts[0].normal.y * 1000, -veloCouter, veloCouter), 0);
        }
        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Enemy"))
            {
                Pop();
            }
        }
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Obstacle") || collision.CompareTag("Enemy"))
            {
                Pop();
            }
        }
    }
}
