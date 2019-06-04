using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCoinUIController : MonoBehaviour {

	Text text;

	private void Awake()
	{
		text = GetComponent<Text>();
	}
	private void Update()
	{
		text.text = GameManager.Instance.Coins.ToString();
	}
	
}
