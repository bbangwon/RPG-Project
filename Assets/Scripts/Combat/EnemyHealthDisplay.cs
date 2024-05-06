using UnityEngine;
using TMPro;
using RPG.Attributes;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;
        TextMeshProUGUI tmpText;

        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
            tmpText = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            if(fighter.GetTarget() == null)
            {
                tmpText.text = "N/A";
                return;
            }
            Health health = fighter.GetTarget().GetHealth();
            tmpText.text = $"{health.GetPercentage():0}%";
        }
    }

}