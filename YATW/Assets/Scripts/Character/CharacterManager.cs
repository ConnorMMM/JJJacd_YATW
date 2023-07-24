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

		[SerializeField] private float m_maxMoveSpeed = 10f;
		[SerializeField] private float m_moveSpeedChange = .1f;
		[SerializeField] private float m_turnSpeed = 30f;

		[SerializeField] private float m_moveSpeed;
		[SerializeField] private float m_rotationSpeed;
		private Vector2 m_moveInput;
		private bool m_hasInput = false;


		private Rigidbody m_rb;

		[Space(20), Header("Testing")]
		[SerializeField] private GameObject m_spinningTop;

		private void Awake()
		{
			m_rotationSpeed = m_startingRotationSpeed;
			m_moveSpeed = m_maxMoveSpeed;
			m_rb = GetComponent<Rigidbody>();
		}

		private void Update()
		{
			/*float inputX = Input.GetAxis("Horizontal");
			if(inputX - m_moveInput.x > 0)
			{
				m_moveInput.x += m_moveSpeedChange;
				if(m_moveInput.x > inputX)
					m_moveInput.x = inputX;
			}
			else if(inputX - m_moveInput.x < 0)
			{
				m_moveInput.x -= m_moveSpeedChange;
				if(m_moveInput.x < inputX)
					m_moveInput.x = inputX;
			}
			
			float inputY = Input.GetAxis("Vertical");
			if(inputY - m_moveInput.y > 0)
			{
				m_moveInput.y += m_moveSpeedChange;
				if(m_moveInput.y > inputY)
					m_moveInput.y = inputY;
			}
			else if(inputY - m_moveInput.y < 0)
			{
				m_moveInput.y -= m_moveSpeedChange;
				if(m_moveInput.y < inputY)
					m_moveInput.y = inputY;
			}*/
			
			m_moveInput.x = Input.GetAxis("Horizontal");
			m_moveInput.y = Input.GetAxis("Vertical");
		}

		private void FixedUpdate()
		{
			/*transform.position += transform.forward * (m_moveInput.y * m_moveSpeed * Time.fixedDeltaTime) + 
			                      transform.right   * (m_moveInput.x * m_moveSpeed * Time.fixedDeltaTime);*/
			
			m_rb.AddForce(new Vector3(m_moveInput.x, 0, m_moveInput.y) * m_moveSpeed, ForceMode.Impulse);

			
			m_spinningTop.transform.eulerAngles += new Vector3(0, m_rotationSpeed * Time.fixedDeltaTime, 0);
			
			m_rotationSpeed -= m_rotationSpeedLoss * Time.fixedDeltaTime;
			if(m_rotationSpeed <= 0)
			{
				m_rotationSpeed = m_maxRotationSpeed;
			}
		}

		public void Move(InputAction.CallbackContext _context)
		{
			//Vector2 inputDiff = _context.ReadValue<Vector2>() - m_moveInput;
			//m_moveInput += inputDiff * .1f;
			//m_hasInput = true;
			//Debug.Log(m_moveInput);
		}
		
		public void Dash(InputAction.CallbackContext _context)
		{
			Debug.Log("Dash True");
		}

		public void HitObject(Vector3 _force)
		{
			m_rb.AddForce(_force, ForceMode.Impulse);
		}
	}
}