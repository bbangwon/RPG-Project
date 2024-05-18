using UnityEngine;

namespace RPG.UI.DamageText
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] DamageText damageTextPrefab;

        private void Start()
        {
            Spawn(10);            
        }

        public void Spawn(float damageAmount)
        {
            DamageText damageText = Instantiate(damageTextPrefab, transform);
        }
    }

}