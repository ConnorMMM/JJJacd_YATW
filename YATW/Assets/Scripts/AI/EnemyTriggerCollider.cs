using UnityEngine;

namespace BladeWaltz.AI
{
    public class EnemyTriggerCollider : MonoBehaviour
    {
        [SerializeField] private BaseEnemy m_parent;

        private void OnTriggerEnter(Collider _other)
        {
            if(_other.gameObject.CompareTag("Player"))
            {
                m_parent.TriggerHit();
            }
        }
    }
}