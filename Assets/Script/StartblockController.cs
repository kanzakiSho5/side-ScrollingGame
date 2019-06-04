using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartblockController : MonoBehaviour {

	private BoxCollider2D m_boxCollider;

	public LayerMask whatIsPlayer;

	private void Awake()
	{
		this.m_boxCollider = this.GetComponent<BoxCollider2D>();
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag == "Player")
		{
			Vector2 pos = this.transform.position;
			Vector2 groundCheck = new Vector2(
				pos.x, 
				pos.y - (this.m_boxCollider.size.y * 0.5f) * this.transform.lossyScale.y
			);
			Vector2 groundArea = new Vector2(
				this.m_boxCollider.size.x * this.transform.lossyScale.x * 0.45f,
				.1f
			);
			
			Collider2D col2D = Physics2D.OverlapArea(
				groundCheck + groundArea,
				groundCheck - groundArea,
				this.whatIsPlayer
			);
			if(col2D)
			{
				SceneManager.LoadSceneAsync("Game");
			}
		}
	}
}
