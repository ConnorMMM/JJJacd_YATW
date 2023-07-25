using System;

using UnityEngine;

namespace BladeWaltz.UI
{
	[Serializable]
	public class HighScore : MonoBehaviour
	{
		[SerializeField] private GameObject m_placementText;
		[SerializeField] private GameObject m_nameText;
		[SerializeField] private GameObject m_scoreText;
		
		public int m_placement;
		public string m_name;
		public int m_score;

		public void InputEntry(string _name, int _score)
		{
			m_name = _name;
			m_score = _score;
		}
	}
}