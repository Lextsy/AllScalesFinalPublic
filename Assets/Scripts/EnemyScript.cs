using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyScript : MonoBehaviour
{
    private Transform theTransform;
    public GameDirector theGameDirector;
    private Vector3 thePosition;
    public float enemySpeed;
    private float timeThisIsAlive;
    public float enemyLifetimeInSeconds = 30.0f;
    public float damageIntensityToPlayer = 0.25f;
    public float damageIntensityToPlatform = 0.25f;
    public float damageDuration = 1.0f;
    private PlayerMovement thePlayer;
    private PlatformControllerScript thePlatform;
    private IObjectPool<EnemyScript> csEnemyPool;
    public GameObject[] modelArray;

    private void Awake()
    {
        theGameDirector = GameObject.Find("GameDirectorObject").GetComponent<GameDirector>();
    }

    void Start()
    {
        damageDuration = damageDuration * theGameDirector.GetDifficulty();
        enemySpeed = enemySpeed * theGameDirector.GetDifficulty();
        damageIntensityToPlatform = damageIntensityToPlatform * theGameDirector.GetDifficulty();
        thePlayer = GameObject.Find("Player").GetComponent<PlayerMovement>();
        thePlatform = GameObject.Find("PlatformMain").GetComponent<PlatformControllerScript>();
    }

    void OnEnable()
    {
        theTransform = GetComponent<Transform>();
        timeThisIsAlive = 0.0f;

    }

    void Update()
    {
        theTransform.Translate(0,0,enemySpeed * Time.deltaTime * -1);
        if (isActiveAndEnabled)
        {
            timeThisIsAlive += Time.deltaTime;
        
            if (timeThisIsAlive > enemyLifetimeInSeconds)
            {
                csEnemyPool.Release(this);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            thePlayer.PlayerHasBeenHit(damageIntensityToPlayer,damageDuration);
        }
        if (other.CompareTag("PlatformMain"))
        {
            thePlatform.PlatformTakeDamage(damageIntensityToPlatform);
            theGameDirector.PlatformHasBeenHit();
        }
    }

    public void ChangeModel(int modelIndex)
    {
        for(int i = 0; i < modelArray.Length; i++)
        {
            modelArray[i].SetActive(false);
        }
        modelArray[modelIndex].SetActive(true);
    }
    public void SetPool(IObjectPool<EnemyScript> pool)
    {
        csEnemyPool = pool;
    }
}
