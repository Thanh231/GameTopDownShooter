
using UnityEngine;

public class Enemy : Actor
{
    private Player m_player;
    private EnemyStats enemyStats;
    private float m_damage;
    private float m_xpBonus;

    public float CurrentDamage { get => m_damage; private set => m_damage = value; }
    private void Update()
    {
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
    private void LoadStats()
    {
        if (statsData == null) return;
        enemyStats = (EnemyStats)statsData;
        enemyStats.Load();
        CurrentHP = enemyStats.hp;
    }
    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        m_player = GameManager.Ins.Player;
        if (statsData == null || m_player == null) return;
        //enemyStats = (EnemyStats)statsData;
        //enemyStats.Load();

        enemyStats = (EnemyStats)statsData;
        enemyStats.Load();
        CurrentHP = enemyStats.hp;

        StateCaculate();

        onDead.AddListener(() => OnspawnCollectable());
        onDead.AddListener(() => OnAddXpToPlayer());
    }
    public override void Die()
    {
        base.Die();
        m_anim.SetBool(AniimationConstant.dead, true);
        CineController.Ins.ShakeTrigger();
    }
    private void StateCaculate()
    {
        var playerStats = m_player.PlayerStates;
        if (playerStats == null) return;

        float hpUpdage = enemyStats.hpUp + Helper.GetQualityLevelUp(playerStats.currentlevel);
        float damageUpgrade = enemyStats.damage + Helper.GetQualityLevelUp(playerStats.currentlevel);
        float randomXp = Random.Range(enemyStats.minXp, enemyStats.maxXp);

        CurrentHP = enemyStats.hp + hpUpdage;
        CurrentDamage = enemyStats.damage + damageUpgrade;
        m_xpBonus = randomXp * Helper.GetQualityLevelUp(playerStats.currentlevel);
    }
    private void OnspawnCollectable()
    {
        CollectableManager.Ins.Spawn(transform.position);
    }
    private void OnAddXpToPlayer()
    {
        GameManager.Ins.Player.AddXp(m_xpBonus);
    }
    private void OnDisable()
    {
        onDead.RemoveListener(OnspawnCollectable);
        onDead.RemoveListener(OnAddXpToPlayer);
    }
    private void FixedUpdate()
    {
        Move();
    }
    protected override void Move()
    {
        if (m_player == null || IsDead)
        {
            m_rd.velocity = Vector3.zero;
        }
        else
        {
            Vector2 directionPlayer = m_player.transform.position - transform.position;
            directionPlayer.Normalize();
            if (!m_isKnockBack)
            {
                Filp(directionPlayer);

                m_rd.velocity = directionPlayer * enemyStats.moveSpeed * Time.deltaTime;
                return;
            }
            m_rd.velocity = directionPlayer * -enemyStats.knockBackForce * Time.deltaTime;
        }
    }

    private void Filp(Vector2 directionPlayer)
    {
        if (directionPlayer.x > 0)
        {
            if (transform.localScale.x > 0) return;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else if (directionPlayer.x < 0)
        {
            if(transform.localScale.x < 0) return;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

        }
    
    }
}

