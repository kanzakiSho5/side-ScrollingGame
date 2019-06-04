using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour {

	[SerializeField]
	GameObject[] LayerGroup;

	Vector3[] m_initialPositions;

	private Transform cameraPos;

	private void Awake()
	{
		m_initialPositions = new Vector3[LayerGroup.Length];
		for(int i = 0; i < LayerGroup.Length; i++)
		{
			m_initialPositions[i] = LayerGroup[i].transform.position;
		}
		cameraPos = Camera.main.transform;
	}

	private void Update()
	{
		for(var i = 0; i < LayerGroup.Length; i++)
		{
			if(LayerGroup[i] != null)
			{
				
				LayerGroup[i].transform.position = new Vector3(
					((cameraPos.position.x) - m_initialPositions[i].x) * (LayerGroup.Length - i) * 0.2f,
					m_initialPositions[i].y, 
					m_initialPositions[i].z
				);
				if(i == 0)
				{
					LayerGroup[0].transform.position = Vector3.right * cameraPos.position.x;
				}
				if(i == LayerGroup.Length - 1)
				{
					LayerGroup[i].transform.position = m_initialPositions[i];
				}
				
			}
		}
	}
}
