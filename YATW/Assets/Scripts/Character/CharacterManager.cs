using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

namespace BladeWaltz.Character
{
	[RequireComponent(typeof(Rigidbody))]
	public class CharacterManager : MonoBehaviour
	{
		[Header("Components")]
		[SerializeField] private GameObject m_arms;
		[SerializeField] private GameObject m_face;
		[SerializeField] private ParticleSystem m_wind;
		private Rigidbody m_rb;
		
		[Space(10), Header("Settings")]
		[SerializeField] private float m_damageModifier = 3f;

		[Space(2), Header("Rotation Settings")]
		[SerializeField] private float m_startingRotationSpeed = 500f;
		[SerializeField] private float m_maxRotationSpeed = 1500f;
		[SerializeField] private float m_rotationDrag = 10f;
		[SerializeField, Tooltip("Arm's rotation speed. (DON'T TOUCH)")] private float m_rotationSpeed;
		[SerializeField] private bool m_reverseRotation;
		
		[Space(2), Header("Move Settings")]
		[SerializeField] private float m_moveSpeed = 70;
		private Vector2 m_moveInput;

		[Space(2), Header("Dash Settings")]
		[SerializeField] private float m_dashCooldown = 1f;
		[SerializeField] private float m_dashStrength = 20f;
		[SerializeField] private float m_dashRotationIncrease = 200;
		private bool m_canDash;

        [Space(2), Header("Audio Settings")]
        [SerializeField] private AudioSource m_dash;
		[SerializeField] private AudioClip[] m_dashSounds;
		[SerializeField] private AudioMixer m_playerMixer;
		
		private void Awake()
		{
			m_rotationSpeed = m_startingRotationSpeed;
			m_reverseRotation = false;
			m_canDash = true;
			m_wind.Stop();
			m_rb = GetComponent<Rigidbody>();
		}

		private void FixedUpdate()
		{
			m_rb.AddForce(new Vector3(m_moveInput.x, 0, m_moveInput.y) * m_moveSpeed, ForceMode.Force);

			Vector3 rotationChange = new(0, m_rotationSpeed * Time.fixedDeltaTime, 0);
			m_arms.transform.eulerAngles += m_reverseRotation ? -rotationChange : rotationChange;

			AddRotationSpeed(-m_rotationDrag * Time.fixedDeltaTime);

			float degree = Mathf.Atan2(m_rb.velocity.normalized.x, m_rb.velocity.normalized.z) * Mathf.Rad2Deg;
			m_face.transform.eulerAngles = new Vector3(0, degree, 0);
			
			float pitch = (m_rotationSpeed / 1000) * 2.1f;
			m_playerMixer.SetFloat("RotationPitch", pitch);
		}

		public void Move(InputAction.CallbackContext _context)
		{
			m_moveInput = _context.ReadValue<Vector2>();
		}
		
		public void Dash(InputAction.CallbackContext _context)
		{
			if(m_canDash && (m_moveInput.x != 0 || m_moveInput.y != 0))
			{
				m_rb.AddForce(new Vector3(m_moveInput.x, 0, m_moveInput.y) * m_dashStrength, ForceMode.Impulse);
				
				float degree = Mathf.Atan2(m_moveInput.x, m_moveInput.y) * Mathf.Rad2Deg;
				m_wind.transform.eulerAngles = new Vector3(0, degree, 0);
				m_wind.Play();
				m_wind.transform.eulerAngles = new Vector3(0, degree, 0);
				
				m_dash.PlayOneShot(m_dashSounds[0]);
				StartCoroutine(DashCooldown());
			}
		}

		IEnumerator DashCooldown()
		{
			m_canDash = false;
			yield return new WaitForSeconds(m_dashCooldown);
			m_canDash = true;
			m_dash.PlayOneShot(m_dashSounds[1]);
		}
		

		private void ApplyForce(Vector3 _force)
		{
			m_rb.AddForce(_force, ForceMode.Impulse);
		}

		private void ModifyVelocity(float _percentage)
		{
			m_rb.velocity -= m_rb.velocity * _percentage;
		}

		private void AddRotationSpeed(float _increase)
		{
			m_rotationSpeed += _increase;
			
			if(m_rotationSpeed <= 0)
			{
				m_rotationSpeed = 0;
				//TODO: Add code for death
				Destroy(gameObject);
			}
			else if(m_rotationSpeed > m_maxRotationSpeed)
				m_rotationSpeed = m_maxRotationSpeed;
		}

		public void HitEnemy(float _changeInRotation)
		{
			AddRotationSpeed(_changeInRotation);
		}

		public void HitWall(Vector3 _force, float _rotationDecrease)
		{
			ApplyForce(_force);
			AddRotationSpeed(_rotationDecrease);

			ReverseArms();
		}

		public void HitPickUp(float _rotationIncrease)
		{
			ModifyVelocity(.5f);
			AddRotationSpeed(_rotationIncrease);
		}

		private void ReverseArms()
		{
			m_reverseRotation = !m_reverseRotation;
			m_arms.transform.eulerAngles = new Vector3(m_arms.transform.eulerAngles.x + 180f, m_arms.transform.eulerAngles.y, m_arms.transform.eulerAngles.z);
		}

		public float GetDamage()
		{
			return m_rotationSpeed * 0.01f * m_damageModifier;
		}

		public void TakeDamage(float _damage)
		{
			AddRotationSpeed(-_damage);
		}
	}
}