using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class Demon : Enemy
    {
        [SerializeField] Rigidbody2D rb;
        [SerializeField] float radiusDetect, speedChase;
        [SerializeField] LayerMask bubbleLayerMask;
        bool isChasing;

        private void Start()
        {
            isChasing = false;
            Idle();
        }
        void Update()
        {
            Collider2D targetColl = Physics2D.OverlapCircle(transform.position, radiusDetect, bubbleLayerMask);

            if (targetColl != null)
            {
                StopAllCoroutines();
                isChasing = true;
                Chase(targetColl.transform.position);
            }
            else
            {
                if (isChasing == true) Idle();
                isChasing = false;
            }
        }

        void Chase(Vector3 targetPos)
        {
            if (targetPos.x<transform.position.x)
                anim.SetBool("isTurnLeft", true);
            else
                anim.SetBool("isTurnLeft", false);

            Vector3 direct = (targetPos - transform.position).normalized;
            this.rb.AddForce(direct * this.speedChase);
        }
        void Idle()
        {
            StartCoroutine(IToggleDirectAnim(2));
        }

        IEnumerator IToggleDirectAnim(float sec)
        {
            yield return new WaitForSeconds(sec);
            this.anim.SetBool("isTurnLeft", !this.anim.GetBool("isTurnLeft"));
            StartCoroutine(IToggleDirectAnim(2));
        }
    }
}
