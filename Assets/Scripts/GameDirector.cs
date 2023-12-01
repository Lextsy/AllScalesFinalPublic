using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    private Volume theVolume;
    public float volumeWeight = 1.0f;
    private float durationOfHit = 1.0f;
    public float failStateTimeToKill = 2.0f;
    public float failStateRecordCurrentTime;
    public bool gameHasFailed;
    private Camera mainCamera;
    public Animator playerAnimator;
    public EnemySpawnMasterScript enemyController;
    public AnubisController anubis;
    private Animator planeLevelAnimator;
    public PlaneFailExtra thePlaneExtra;
    private float gameStartDelay = 2.0f;
    private float gameEndDelay = 3.0f;
    public float levelLengthInSeconds = 60.0f;
    public float levelLengthElapsed = 0.0f;
    public bool gameIsLiveGD = false;
    public bool gameWin = false;
    private float gameInitializeTime;
    public float gameWinStateRecordCurrentTime;
    public int sceneToTransitionToOnLoss = 6;
    public int sceneToTransitionToOnWin = 0;
    public SODifficulty soDifficultyValue;
    [SerializeField] private GameObject textToPopOnFail;
    [SerializeField] private AccessoryLevelControllerTheScales accLevelController;
    private RespawnTrigger respawner;
    
    private void Start()
    {
        Vector3 camScalesPos = new Vector3(0,8,-10);
        Vector3 camScalesRot = new Vector3(20,0,0);
        Vector3 camPlanePos = new Vector3(0,7,-10);
        Vector3 camPlaneRot = new Vector3(20,0,0);
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        theVolume = GameObject.Find("GlobalVolume").GetComponent<Volume>();
        playerAnimator = GameObject.Find("liz_man_animation").GetComponent<Animator>();
        enemyController = GameObject.Find("EnemySpawnerMaster").GetComponent<EnemySpawnMasterScript>();
        respawner = GameObject.Find("RespawnTrigger").GetComponent<RespawnTrigger>();
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            anubis = GameObject.Find("anubis_final").GetComponent<AnubisController>();
            anubis.IntroduceAnubis();
        }
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            thePlaneExtra = GameObject.Find("PlatformMain").GetComponent<PlaneFailExtra>();
            planeLevelAnimator = GameObject.Find("Decoration").GetComponent<Animator>();
        }
        gameInitializeTime = Time.time;
    }

    private void LateUpdate()
    {
        theVolume.weight = Mathf.MoveTowards(theVolume.weight,1,(1/durationOfHit)*Time.deltaTime);
        if(anubis)
        {
            var anubismovement = levelLengthElapsed / levelLengthInSeconds ;
            anubis.SlideAnubis(anubismovement);
        }
    }

    private void Update()
    {
        if (Time.time > gameInitializeTime + gameStartDelay)
        {
            gameIsLiveGD = true;
        }
        if (gameHasFailed)
        {
            gameIsLiveGD = false;
            if (Time.time > failStateRecordCurrentTime + failStateTimeToKill)
            {
                GameFullFail();
            }
        }
        if (gameIsLiveGD)
        {
            enemyController.GameIsLive(true);
            levelLengthElapsed += 1 * Time.deltaTime;
        }
        if (!gameWin && !gameHasFailed && levelLengthElapsed > levelLengthInSeconds)
        {
            GameWinState();
        }
        if (gameWin && Time.time > gameWinStateRecordCurrentTime + gameEndDelay)
        {
            GameFullWin();
        }
    }

    public void GameFullFail()
    {
        SceneManager.LoadScene(sceneToTransitionToOnLoss);
    }
    public void GameFailState()
    {
        respawner.gameObject.SetActive(false);
        enemyController.GameIsLive(false);
        failStateRecordCurrentTime = Time.time;
        if (textToPopOnFail)
        {
            textToPopOnFail.SetActive(true);
        }
        playerAnimator.SetBool("DeathFall", true);
        gameHasFailed = true;
        if(accLevelController)
        {
            accLevelController.DisableExtraColliders();
        }
        if (thePlaneExtra)
        {
            thePlaneExtra.PlaneFail();
        }
    }

    public void GameWinState()
    {
        gameWin = true;
        gameWinStateRecordCurrentTime = Time.time;
        if(anubis)
        {
            anubis.ExitAnubis();
        }
    }

    public void GameFullWin()
    {
        SceneManager.LoadScene(sceneToTransitionToOnWin);
    }
    public bool IsWon
    {
        get {return gameWin;}
        set
        {
            if (gameWin != value)
            {
                gameWin = value;
            }
        }
    }

    public float GetDifficulty()
    {
        return soDifficultyValue.difficulty;
    }
    public void PlatformHasBeenHit()
    {
        volumeWeight = 0.0f;
        if (durationOfHit < 0.0f)
        {
            durationOfHit = 0;
        }
        theVolume.weight = volumeWeight;
    }

    public float RemainingTime()
    {
        //Returns in float/percentage, like .05f remaining.
        return levelLengthElapsed / levelLengthInSeconds;
    }
}