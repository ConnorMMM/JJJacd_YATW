using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BladeWaltz.Character;

using UnityEngine.Serialization;

public class DestructibleObstacle : MonoBehaviour
{
    [SerializeField] private float m_velocityLoss = 0.8f;
    [SerializeField] private float m_rotationIncrease = 100f;

    private void OnTriggerEnter(Collider _other)
    {
        if(_other.CompareTag("Player"))
        {
            CharacterManager characterManager = _other.GetComponent<SpinningTop>().GetCharacterManager();
            characterManager.ModifyVelocity(m_velocityLoss);
            characterManager.AddRotationSpeed(m_rotationIncrease);

            Destroy(gameObject);
        }
    }
}
