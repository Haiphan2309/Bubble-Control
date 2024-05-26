using GDC.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

namespace Gameplay
{
    public class PlayerBubble : Bubble
    {
        public static PlayerBubble Instance { get; private set; }

        [SerializeField] SpriteRenderer sprRen;
        [SerializeField] float attackTime, attackVelo;
        Coroutine attackCor;

        public PlayerState playerState;
        private void Awake()
        {
            Instance = this;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Attack();
            }
        }
        protected override void Pop()
        {
            base.Pop();
            playerState = PlayerState.DIE;
        }
        void Attack()
        {
            if (playerState != PlayerState.NORMAL) return;

            rb.velocity = rb.velocity.normalized * attackVelo;
            playerState = PlayerState.ATTACK;
            sprRen.color = Color.red;
            attackCor = StartCoroutine(CorAttack(attackTime));
        }
        IEnumerator CorAttack(float sec)
        {
            yield return new WaitForSeconds(sec);
            playerState = PlayerState.NORMAL;
            sprRen.color = Color.white;
        }
        void Win()
        {
            Debug.Log("Win");
            playerState = PlayerState.WIN;
        }
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);
            if (collision.CompareTag("Goal"))
            {
                Win();
            }
        }
        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                Pop();
            }
            if (collision.gameObject.CompareTag("Enemy"))
            {
                if (playerState == PlayerState.ATTACK)
                {
                    collision.gameObject.GetComponent<Enemy>().Hurt();
                }
                else
                {
                    Pop();
                }
            }
        }
        protected override void OnCollisionStay2D(Collision2D collision)
        {
            if (!(collision.gameObject.CompareTag("Enemy") && playerState == PlayerState.ATTACK))
            {
                base.OnCollisionStay2D(collision);
            }
        }
    }
}
