using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDC.Minigame1;
using Gameplay;
using DG.Tweening;

namespace GDC.Minigame1
{
    public class SpikeMon : MonoBehaviour
    {
        [SerializeField] Transform eye;
        [SerializeField] float eyeRadius;

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
    }
}