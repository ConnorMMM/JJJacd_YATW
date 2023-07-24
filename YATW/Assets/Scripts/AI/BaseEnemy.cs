using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BladeWaltz.Managers;

namespace BladeWaltz.AI
{
	[RequireComponent(typeof(NavMeshAgent))]
	public abstract class BaseEnemy : MonoBehaviour
	{
    
		[Header("AI Stats")] 
		[SerializeField] private float m_health;
		[SerializeField] private float m_moveSpeed;
		[SerializeField] public float m_turnSpeed;

		[Header("Weapon Stats")]
		[SerializeField] public GameObject m_projectilePrefab;
		[SerializeField] private float m_damage;
		[SerializeField] private float m_projectileSpeed;
    
		protected NavMeshAgent m_agent;
		protected GameManager m_gameManager;
		protected GameObject m_player;
		protected float m_distance;
		protected Animator m_animator;

		// Start is called before the first frame update
		private void Awake()
		{
			m_agent = GetComponent<NavMeshAgent>();
			m_animator = GetComponent<Animator>();
			m_gameManager = FindObjectOfType<GameManager>();
			
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
			m_player = m_gameManager.m_player;
			m_distance = Vector3.Distance(transform.position, m_player.transform.position);

			transform.position += transform.right * Time.fixedDeltaTime;
			
			// Custom behaviours for sub classes
			Behaviour();
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
    
		protected abstract void Behaviour();
	}
}