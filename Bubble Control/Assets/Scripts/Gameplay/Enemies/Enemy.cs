using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] protected int hp;

        public virtual void Hurt()
        {
            hp--;
            if (hp <= 0) Dead();
        }
        protected virtual void Dead()
        {
            Debug.Log(gameObject.name + " Dead!");
            Destroy(gameObject);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Obstacle"))
            {
                Hurt();
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                Hurt();
            }
        }
    }
}
