using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BladeWaltz.Character;

using UnityEngine.Serialization;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private bool m_isDestructible = true;
    [SerializeField] private float m_speedLostOnHit = 0.1f;

    private void OnCollisionEnter(Collision _collision)
    {
        if(_collision.gameObject.CompareTag("Player"))
        {
            Vector3 collisionNormal = _collision.GetContact(0).normal;
            
            Debug.Log("Collision");
            CharacterManager controller;
            if(_collision.gameObject.TryGetComponent(out controller))
                controller.HitObject(collisionNormal * m_speedLostOnHit);
            else if(_collision.transform.parent.TryGetComponent(out controller))
                controller.HitObject(collisionNormal * m_speedLostOnHit);
            
            if(m_isDestructible)
                Destroy(gameObject);
        }
    }
}
