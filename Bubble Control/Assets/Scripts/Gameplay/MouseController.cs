using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.UI;

namespace Gameplay
{
    public class MouseController : MonoBehaviour
    {
        public static MouseController Instance { get; private set; }
        [SerializeField] Collider2D blowColl;
        [SerializeField] LayerMask blowLayerMask;

        [SerializeField] float blowForce;
        private void Awake()
        {
            Instance = this;
        }
        // Update is called once per frame
        void Update()
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;

            if (Input.GetMouseButtonDown(0))
            {
                Blow();
            }
            if (Input.GetMouseButtonUp(0))
            {
                StopBlow();
            }
        }
        void Blow()
        {
            blowColl.gameObject.SetActive(true);
        }
        void StopBlow()
        {
            blowColl.gameObject.SetActive(false);
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if ((blowLayerMask & (1 << collision.gameObject.layer)) != 0)
            {
                BlowableObject blowScript = collision.GetComponent<BlowableObject>();
                if (blowScript != null)
                {
                    blowScript.Blow(transform.position, blowForce);
                }
                else
                {
                    Debug.LogError(collision.name + " don't have BlowableObject script!");
                }
            }
        }
    }
}