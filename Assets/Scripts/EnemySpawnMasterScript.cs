using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class EnemySpawnMasterScript : MonoBehaviour
{

    //Initialize Spawner Locations
    [SerializeField] Transform spawner1;
    [SerializeField] Transform spawner2;
    [SerializeField] Transform spawner3;
    [SerializeField] Transform spawner4;
    [SerializeField] Transform spawner5;
    [SerializeField] Transform spawner6;

    //Pool Related Declarations

    public EnemyScript enemyToPool;
    IObjectPool<EnemyScript> enemy_Pool;

    //Game Declarations
    public bool gameIsLive = false;
    public int randomNumberGenerated;
    [SerializeField] int randomMin = 1;
    [SerializeField] int randomMax = 6;
    [SerializeField] Transform positionToSpawnEnemy;
    private float enemySpawnTimer = 5.0f;
    private float lastTimeEnemySpawned;
    public GameDirector gameDirector;
    public int enemyType = 0;

    public void SetPool(IObjectPool<EnemyScript> pool)
    {
        enemy_Pool = pool;
    }

    private void Awake()
    {
        gameDirector = GameObject.Find("GameDirectorObject").GetComponent<GameDirector>();
        enemy_Pool = new ObjectPool<EnemyScript>(CreateEnemy, OnTakeEnemyFromPool, OnReturnEnemyToPool);
        gameIsLive = false;
    }
    private void Start()
    {
        enemySpawnTimer /= gameDirector.GetDifficulty();
    }

    void Update()
    {
        if (gameIsLive && gameDirector.RemainingTime() >= .05)
        {
            if (Time.time > lastTimeEnemySpawned + enemySpawnTimer)
            {
                lastTimeEnemySpawned = Time.time;
                enemy_Pool.Get();
            }
        }
    }

    public void GameIsLive(bool value)
    {
        gameIsLive = value;
    }

    EnemyScript CreateEnemy()
    {
        RNG();
        switch (RNG())
        {
            default:
                break;
            case 1:
                positionToSpawnEnemy = spawner1;
                break;
            case 2:
                positionToSpawnEnemy = spawner2;
                break;
            case 3:
                positionToSpawnEnemy = spawner3;
                break;
            case 4:
                positionToSpawnEnemy = spawner4;
                break;
            case 5:
                positionToSpawnEnemy = spawner5;
                break;
            case 6:
                positionToSpawnEnemy = spawner6;
                break;
        }
        EnemyScript yomom = Instantiate(enemyToPool, positionToSpawnEnemy.position, Quaternion.identity);
        yomom.transform.position = positionToSpawnEnemy.localPosition;
        yomom.SetPool(enemy_Pool);
        return yomom;
    }

    public int RNG()
    {
        randomNumberGenerated = Random.Range(randomMin,randomMax);
        return randomNumberGenerated;
    }



    void OnTakeEnemyFromPool(EnemyScript theEnemy)
    {
        theEnemy.gameObject.SetActive(true);
        RNG();
        switch (RNG())
        {
            default:
                break;
            case 1:
                positionToSpawnEnemy = spawner1;
                break;
            case 2:
                positionToSpawnEnemy = spawner2;
                break;
            case 3:
                positionToSpawnEnemy = spawner3;
                break;
            case 4:
                positionToSpawnEnemy = spawner4;
                break;
            case 5:
                positionToSpawnEnemy = spawner5;
                break;
            case 6:
                positionToSpawnEnemy = spawner6;
                break;
        }
        theEnemy.transform.position = positionToSpawnEnemy.position;
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            theEnemy.ChangeModel(Random.Range(0,2));
        }
    }

    void OnReturnEnemyToPool(EnemyScript theEnemy)
    {
        theEnemy.gameObject.SetActive(false);
    }
}
