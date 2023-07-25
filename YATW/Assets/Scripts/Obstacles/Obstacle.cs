using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BladeWaltz.Character;

using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float m_force = 50;
    [SerializeField] private float m_rotationDecrease = -300f;

    [SerializeField] private AudioSource hitSound;
    [SerializeField] private AudioClip[] soundList;
    private void OnCollisionEnter(Collision _collision)
    {
        if(_collision.gameObject.CompareTag("Player"))
        {
            hitSound.PlayOneShot(soundList[Random.Range(0, soundList.Length)]);
            Vector3 collisionNormal = _collision.GetContact(0).normal;
            
            Debug.Log("Collision");
            CharacterManager controller;
            if(_collision.gameObject.TryGetComponent(out controller))
                controller.HitWall(collisionNormal * -m_force, m_rotationDecrease);
            else if(_collision.transform.parent.TryGetComponent(out controller))
                controller.HitWall(collisionNormal * -m_force, m_rotationDecrease);
        }
    }
}
