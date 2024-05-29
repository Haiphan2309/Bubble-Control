using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDC.Minigame1;
using Gameplay;
using DG.Tweening;

namespace GDC.Minigame1
{
    public enum MoveDirect
    {
        UP,
        UP_RIGHT,
        RIGHT,
        DOWN_RIGHT,
        DOWN,
        DOWN_LEFT,
        LEFT,
        UP_LEFT,
    }
    public class SpikeMon : MonoBehaviour
    {
        [SerializeField] Rigidbody rb;
        [SerializeField] Transform eye;
        [SerializeField] float eyeRadius;

        [SerializeField] MoveDirect direct; //cac huong di theo chieu kim dong ho tu 0 den 7
        [SerializeField] float speed;

        // Update is called once per frame
        void Update()
        {
            if (PlayerBubble.Instance != null)
            {
                Vector3 lookDir = (PlayerBubble.Instance.transform.position - transform.position).normalized;
                eye.localPosition = lookDir * eyeRadius;
            }
            else eye.DOLocalMove(Vector3.zero, 0.5f);
        }

        void FixedUpdate()
        {
            if (direct == MoveDirect.UP) rb.velocity = new Vector3(0, speed * Time.fixedDeltaTime, 0);
            if (direct == MoveDirect.UP_RIGHT) rb.velocity = new Vector3(speed * Time.fixedDeltaTime, speed * Time.fixedDeltaTime, 0);
            if (direct == MoveDirect.RIGHT) rb.velocity = new Vector3(speed * Time.fixedDeltaTime, 0, 0);
            if (direct == MoveDirect.DOWN_RIGHT) rb.velocity = new Vector3(speed * Time.fixedDeltaTime, -speed * Time.fixedDeltaTime, 0);
            if (direct == MoveDirect.DOWN) rb.velocity = new Vector3(0, -speed * Time.fixedDeltaTime, 0);
            if (direct == MoveDirect.DOWN_LEFT) rb.velocity = new Vector3(-speed * Time.fixedDeltaTime, -speed * Time.fixedDeltaTime, 0);
            if (direct == MoveDirect.LEFT) rb.velocity = new Vector3(-speed * Time.fixedDeltaTime, 0, 0);
            if (direct == MoveDirect.UP_LEFT) rb.velocity = new Vector3(-speed * Time.fixedDeltaTime, speed * Time.fixedDeltaTime, 0);
        }
    }
}