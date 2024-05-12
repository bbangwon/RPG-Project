using UnityEngine;
using TMPro;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats baseStats;
        TextMeshProUGUI tmpText;

        private void Awake()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
            tmpText = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            tmpText.text = $"{baseStats.GetLevel():0}";
        }
    }

}