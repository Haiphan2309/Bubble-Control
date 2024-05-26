using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class BlowableObject : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D rb;
        public virtual void Blow(Vector3 mousePos, float blowForce)
        {
            Vector3 forceDir = (transform.position - mousePos).normalized;
            float forceValue = 1 / (transform.position - mousePos).magnitude;
            rb.AddForce(forceDir * forceValue * blowForce);
        }
    }
}