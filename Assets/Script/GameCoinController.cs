using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCoinController : MonoBehaviour {

    private BoxCollider2D m_boxCollider;
    private Vector3 m_initialPosition;

    private void Awake()
    {
        this.m_boxCollider = this.GetComponent<BoxCollider2D>();
        this.m_initialPosition = this.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManager.Instance.GetCoins(1);
            this.gameObject.SetActive(false);
        }
    }

    public void Initialize()
    {
        this.transform.position = this.m_initialPosition;
        this.gameObject.SetActive(true);
    }
}
