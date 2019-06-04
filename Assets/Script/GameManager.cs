using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int Coins {get; protected set;}
    private GameBlockController[] blocks;
    private GameCoinController[] coins;
    private EnemyController[] enemies;

    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        this.blocks = (GameBlockController[])GameObject.FindObjectsOfType(typeof(GameBlockController));
        this.coins = (GameCoinController[])GameObject.FindObjectsOfType(typeof(GameCoinController));
        this.enemies = (EnemyController[])GameObject.FindObjectsOfType(typeof(EnemyController));
    }


    public void GameInitialize()
    {
        GameObject playerObj =    GameObject.FindGameObjectWithTag("Player");
        PlayerController player = playerObj.GetComponent<PlayerController>();
        if (player)   player.Initialize(); 
        Camera mainCamera = Camera.main;
        GameCameraController cameraController =   mainCamera.GetComponent<GameCameraController>();
        if (cameraController)   cameraController.Initialize();
        Coins = 0;
        foreach (GameBlockController block in this.blocks)  block.Initialize();
        foreach (GameCoinController coin in this.coins)     coin.Initialize();
        foreach (EnemyController enemy in this.enemies)     enemy.Initialize();
    } 

    public void GetCoins(int value)
    {
        Coins += value;
    }

}
