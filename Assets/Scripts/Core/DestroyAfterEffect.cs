using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        ParticleSystem ps;
        private void Awake()
        {
            ps = GetComponent<ParticleSystem>();
        }

        void Update()
        {
            if(!ps.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }

}