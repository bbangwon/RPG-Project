using UnityEngine;
using TMPro;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        TextMeshProUGUI tmpText;

        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            tmpText = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            tmpText.text = $"{health.GetPercentage():0}%";
        }
    }

}