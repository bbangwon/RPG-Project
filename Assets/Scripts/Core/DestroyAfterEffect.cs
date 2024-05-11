using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        [SerializeField] GameObject targetToDestroy = null;

        ParticleSystem ps;
        private void Awake()
        {
            ps = GetComponent<ParticleSystem>();
        }

        void Update()
        {
            if(!ps.IsAlive())
            {
                if(targetToDestroy != null)
                    Destroy(targetToDestroy);
                else
                    Destroy(gameObject);
            }
        }
    }

}