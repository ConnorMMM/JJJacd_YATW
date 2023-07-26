using System;

using TMPro;

using UnityEngine;

namespace BladeWaltz.UI
{
	[Serializable]
	public class HighScore : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI m_placementText;
		[SerializeField] private TextMeshProUGUI m_nameText;
		[SerializeField] private TextMeshProUGUI m_scoreText;
		
		public int m_placement;
		public string m_name;
		public int m_score;

		private void Start()
		{
			m_placementText.text = m_placement.ToString();
			m_nameText.text = m_name;
			m_scoreText.text = m_score.ToString();
		}

		public void InputEntry(string _name, int _score)
		{
			m_name = _name;
			m_score = _score;
		}
	}
}