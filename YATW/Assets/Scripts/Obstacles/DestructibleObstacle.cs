using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

using BladeWaltz.Character;

using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class DestructibleObstacle : MonoBehaviour
{
    [SerializeField] private float m_velocityLoss = 0.8f;
    [SerializeField] private float m_rotationIncrease = 100f;

    [SerializeField] private AudioSource destroySound;
    [SerializeField] private AudioClip[] soundList;

    private void OnTriggerEnter(Collider _other)
    {
        if(_other.CompareTag("Player"))
        {
            destroySound.PlayOneShot(soundList[Random.Range(0, soundList.Length)]);
            CharacterManager characterManager = _other.GetComponent<SpinningTop>().GetCharacterManager();
            characterManager.ModifyVelocity(m_velocityLoss);
            characterManager.AddRotationSpeed(m_rotationIncrease);
            
            Destroy(gameObject);
           

        }
    }
}
