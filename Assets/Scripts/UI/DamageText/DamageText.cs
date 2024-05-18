using System;
using TMPro;
using UnityEngine;

namespace RPG.UI.DamageText
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI damageText;

        public void DestroyText()
        {
            Destroy(gameObject);
        }

        public void SetValue(float damageAmount)
        {
            damageText.text = string.Format("{0:0}", damageAmount);
        }
    }
}