using UnityEngine;

namespace RPG.UI.DamageText
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] DamageText damageTextPrefab;

        public void Spawn(float damageAmount)
        {
            DamageText damageText = Instantiate(damageTextPrefab, transform);
        }
    }

}