using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BladeWaltz.Character
{
	[RequireComponent(typeof(Rigidbody))]
	public class CharacterManager : MonoBehaviour
	{
		[SerializeField] private float m_startingRotationSpeed = 10f;
		[SerializeField] private float m_maxRotationSpeed = 30f;
		[SerializeField] private float m_rotationSpeedLoss = 2f;

		[SerializeField] private float m_damageModifier = 3f;

		[SerializeField] private float m_moveSpeed = 70;
		[SerializeField] private float m_rotationSpeed;
		[SerializeField] private bool m_reverseRotation;
		private Vector2 m_moveInput;
		private bool m_hasInput = false;


		private Rigidbody m_rb;

		[Space(20), Header("Testing")]
		[SerializeField] private GameObject m_spinningTop;

		private void Awake()
		{
			m_rotationSpeed = m_startingRotationSpeed;
			m_reverseRotation = false;
			m_rb = GetComponent<Rigidbody>();
		}

		private void Update()
		{
			m_moveInput.x = Input.GetAxis("Horizontal");
			m_moveInput.y = Input.GetAxis("Vertical");
		}

		private void FixedUpdate()
		{
			m_rb.AddForce(new Vector3(m_moveInput.x, 0, m_moveInput.y) * m_moveSpeed, ForceMode.Force);

			Vector3 rotationChange = new Vector3(0, m_rotationSpeed * Time.fixedDeltaTime, 0);
			m_spinningTop.transform.eulerAngles += m_reverseRotation ? -rotationChange : rotationChange;
			
			//m_rotationSpeed -= m_rotationSpeedLoss * Time.fixedDeltaTime;
			if(m_rotationSpeed <= 0)
			{
				m_rotationSpeed = m_maxRotationSpeed;
			}
		}

		public void Move(InputAction.CallbackContext _context)
		{
			
		}
		
		public void Dash(InputAction.CallbackContext _context)
		{
			Debug.Log("Dash True");
		}

		public void ApplyForce(Vector3 _force)
		{
			m_rb.AddForce(_force, ForceMode.Impulse);
		}

		public void ModifyVelocity(float _percentage)
		{
			m_rb.velocity -= m_rb.velocity * _percentage;
		}

		public void AddRotationSpeed(float _increase)
		{
			m_rotationSpeed += _increase;
		}

		public void HitWall(Vector3 _force, float _rotationDecrease)
		{
			ApplyForce(_force);
			AddRotationSpeed(_rotationDecrease);
			m_reverseRotation = !m_reverseRotation;
		}
	}
}