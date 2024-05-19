using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health healthComponent;
        [SerializeField] RectTransform foreground;

        private void Update()
        {
            foreground.localScale = new Vector3(healthComponent.GetFraction(), 1f, 1f);
        }
    }
}