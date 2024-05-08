using UnityEngine;
using TMPro;

namespace RPG.Attributes
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience experience;
        TextMeshProUGUI tmpText;

        private void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
            tmpText = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            tmpText.text = $"{experience.GetPoints():0}";
        }
    }

}