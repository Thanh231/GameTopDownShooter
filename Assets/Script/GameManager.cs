using System;
using System.Collections;
using UnityEngine;


public enum GameState
{
    STARTING,
    PLAYING, 
    PAUSED,
    GAMEOVER
}

public class GameManager : Singleton<GameManager>
{
    public static GameState state;
    [SerializeField] private Map mapPrefabs;
    [SerializeField] private Player playerPrefabs;
    [SerializeField] private Enemy[] enemyPrefabs;
    [SerializeField] private GameObject aiSpawVfx;
    [SerializeField] float aiSpawnTime;
    [SerializeField] private int playerMaxLife;
    [SerializeField] private int playerStartLife;

    private Map currentMap;
    private int currentlife;
    private PlayerStates playerStates;

    private Player m_player;
    public Player Player { get => m_player; private set => m_player = value; }
    public int Currentlife
    { get => currentlife;

        set
        {
            currentlife = value;
            currentlife = Mathf.Clamp(currentlife, 0, playerMaxLife);
        }
    }
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        state = GameState.STARTING;
        currentlife = playerStartLife;
        SpawnPlayer();
        GUIManager.Ins.ShowGameGUI(false);
        
    }

    protected override void Awake()
    {
        MakeSingleton(false);
    }
    private void SpawnPlayer()
    {
        if (playerPrefabs == null || mapPrefabs == null) return;

        currentMap = Instantiate(mapPrefabs, Vector3.zero, Quaternion.identity);
        m_player = Instantiate(playerPrefabs,currentMap.playerSpawnPos.position, Quaternion.identity);
       
    }
    public void PlayGame()
    {
        state = GameState.PLAYING;
        playerStates = m_player.PlayerStates;

        SpawnEnemy();

        if(m_player == null || playerStates == null) return;
        GUIManager.Ins.ShowGameGUI(true);
        GUIManager.Ins.UpdateLifeInfo(currentlife);
        GUIManager.Ins.UpdateCoinCounting(Prefabs.coin);
        GUIManager.Ins.UpdateHpInfo(m_player.CurrentHP,playerStates.hp);
        GUIManager.Ins.UpdateLevelInfo(playerStates.currentlevel, playerStates.currentXp, playerStates.xpRequireToUgrade);
    }

    private void SpawnEnemy()
    {
        var randomEnemy = GetRanDomEnemy();
        if(randomEnemy == null || currentMap == null) return;
        StartCoroutine(SpawnEnemy_Coroutine(randomEnemy));
    }

    IEnumerator SpawnEnemy_Coroutine(Enemy enemy)
    {
        yield return new WaitForSeconds(3f);
        
        while(state == GameState.PLAYING)
        {
            if (currentMap.enemySpawnPosition == null) break;
            Vector3 spawnPos = currentMap.GetEnemyPos.position;
            if (aiSpawVfx)
            {
                Instantiate(aiSpawVfx, spawnPos, Quaternion.identity);
            }
            yield return new WaitForSeconds(0.2f);
            Instantiate(enemy, spawnPos, Quaternion.identity); 
            yield return new WaitForSeconds(aiSpawnTime);
        }
        yield return null;
    }

    private Enemy GetRanDomEnemy()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length < 0 || currentMap == null) return null;
        int random = UnityEngine.Random.Range(0, enemyPrefabs.Length);
        return enemyPrefabs[random];
    }
    public void GameOverChecking (Action OnlostLife = null , Action OnDead = null)
    {
        if (currentlife <= 0) return;

        currentlife--;
        OnlostLife?.Invoke();

        if(currentlife <= 0)
        {
            state = GameState.GAMEOVER;
            OnDead?.Invoke();
        }
    }
}
