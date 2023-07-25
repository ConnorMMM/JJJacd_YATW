using TMPro;

using UnityEngine;

namespace BladeWaltz.UI
{
	public class LetterCycle : MonoBehaviour
	{
		[SerializeField] private TMP_Text m_letter;
		public char[] m_letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
		private int m_current = 0;

		private void Start()
		{
			m_letter.text = m_letters[m_current].ToString();
		}

		public void UpArrow()
		{
			m_current++;
			if(m_current >= m_letters.Length)
			{
				m_current = 0;
			}

			m_letter.text = m_letters[m_current].ToString();
		}
		
		public void DownArrow()
		{
			m_current--;
			if(m_current < 0)
			{
				m_current = m_letters.Length - 1;
			}

			m_letter.text = m_letters[m_current].ToString();
		}
	}
}