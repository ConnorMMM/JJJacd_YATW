using UnityEngine;

using BladeWaltz.Character;

using Random = UnityEngine.Random;

public class DestructibleObstacle : MonoBehaviour
{
    [SerializeField] private float m_rotationIncrease = 100f;

    [SerializeField] private AudioSource destroySound;
    [SerializeField] private AudioClip[] soundList;

    private void OnTriggerEnter(Collider _other)
    {
        if(_other.CompareTag("Player"))
        {
            //destroySound.PlayOneShot(soundList[Random.Range(0, soundList.Length)]);
            
            
            SpinningTop spinningTop;
            if(_other.TryGetComponent(out spinningTop))
                spinningTop.GetCharacterManager().HitPickUp(m_rotationIncrease);
            else
                _other.GetComponent<CharacterManager>().HitPickUp(m_rotationIncrease);
            
            //TODO: Add code to play break sound
            Destroy(gameObject);
        }
    }
}
