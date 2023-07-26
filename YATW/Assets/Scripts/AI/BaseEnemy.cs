using BladeWaltz.Character;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BladeWaltz.Managers;

using System;

using Random = UnityEngine.Random;

namespace BladeWaltz.AI
{
	[RequireComponent(typeof(NavMeshAgent))]
	public abstract class BaseEnemy : MonoBehaviour
	{
    
		[Header("AI Stats")] 
		[SerializeField] public float m_health;
		[SerializeField] public float m_moveSpeed;
		[Tooltip("Keep low, does not change much.")]
		[SerializeField] public float m_turnSpeed;

		[SerializeField] protected float m_playerRotationIncrease = 50;

		[Header("Weapon Stats")]
		[SerializeField] public GameObject m_projectilePrefab;
		[SerializeField] public float m_damage;
		[SerializeField] public float m_projectileSpeed;
    
		protected NavMeshAgent m_agent;
		protected GameManager m_gameManager;
		protected CharacterManager m_characterManager;
		protected GameObject m_player;
		protected float m_distance;
		protected Animator m_animator;
		// Start is called before the first frame update
		private void Awake()
		{
			m_agent = GetComponent<NavMeshAgent>();
			m_animator = GetComponent<Animator>();
			m_gameManager = GameManager.Instance;
			m_player = m_gameManager.m_player;
			m_characterManager = m_player.GetComponent<CharacterManager>();

			m_agent.speed = m_moveSpeed;
			m_agent.angularSpeed = m_turnSpeed;

			// 1 in 20 chance for enemy to give speed on death
			int num = Random.Range(1, 20);
			if(num == 1)
			{
				IsSpecial();
			}
		}

		// Update is called once per frame
		private void FixedUpdate()
		{
			m_distance = Vector3.Distance(transform.position, m_player.transform.position);
			
			transform.position += transform.right * Time.fixedDeltaTime;

			// Custom behaviours for sub classes
			if(m_player != null)
			{
				Behaviour();
			}
		}

		protected void FleeTarget()
		{
			m_animator.SetBool("Attack", false);
			m_animator.SetTrigger("Move");
			Vector3 dirToPlayer = transform.position - m_player.transform.position;
			Vector3 newPos = transform.position + dirToPlayer;
			m_agent.SetDestination(newPos);
		}

		protected void ChaseTarget()
		{
			m_animator.SetBool("Attack", false);
			m_animator.SetTrigger("Move");
			m_agent.SetDestination(m_player.transform.position);
		}

		protected void AttackTarget()
		{
			m_animator.SetBool("Attack", true);
		}
		
		protected void FaceTarget()
		{
			Vector3 dirToPlayer = (transform.position - m_player.transform.position).normalized;
			Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dirToPlayer.x, 0f, dirToPlayer.z));
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * m_turnSpeed);
		}
		
		private void IsSpecial()
		{
			// Change enemy colour or something
		}

		private void OnCollisionEnter(Collision _col)
		{
			if(_col.gameObject.CompareTag("Player"))
			{
				m_characterManager.HitEnemy(m_playerRotationIncrease);
				DeathBehaviour();
				Destroy(gameObject);
			}
		}

		protected abstract void Behaviour();
		protected abstract void DeathBehaviour();
	}
}