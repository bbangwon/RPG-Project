﻿using UnityEngine;

namespace RPG.Combat
{
    public class Weapon : MonoBehaviour
    {
        public void OnHit()
        {
            Debug.Log("Weapon Hit" + gameObject.name);
        }
    }

}