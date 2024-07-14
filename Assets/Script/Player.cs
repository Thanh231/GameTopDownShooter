using UnityEngine;
using UnityEngine.Events;

public class Player : Actor
{
    internal enum Controller
    {
        moveByMouse,
        moveByKeyboard
    }
    [SerializeField]private Controller controller;

    public float increaseSpeed;
    public float maxDistance;

    Vector2 moveInput;
    private float currentSpeed;
    private PlayerStates playerStates;

    public float radiusCheckEnemy;
    public LayerMask enemyLayer;

    Vector2 mousePos;
    Vector2 movingDir;
    public Vector2 velocityLimit;

    public UnityEvent OnAddXp;
    public UnityEvent OnLevelUp;
    public PlayerStates PlayerStates { get => playerStates; private set => playerStates = value; }

    public override void Init()
    {
        LoadStats();
    }
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        movingDir = mousePos - (Vector2)transform.position;
        movingDir.Normalize();
        Move();

    }
    private void FixedUpdate()
    {
        float angle = Mathf.Atan2(movingDir.y, movingDir.x) * Mathf.Rad2Deg;
        if(weapon != null && controller == Controller.moveByKeyboard)
            weapon.SetRotate(angle);

        var enemies = Physics2D.OverlapCircleAll(transform.position,radiusCheckEnemy,enemyLayer);
        CheckEnemyAround(enemies);
    }

    private void CheckEnemyAround(Collider2D[] enemies)
    {
        if(enemies.Length <= 0 || enemies == null) return;

        float min = float.MaxValue;
        Actor enemyMin = null;
        foreach(Collider2D enemy in enemies)
        {
            var enemyTemp = enemy.GetComponent<Actor>();
            float minDistanceTemp = Vector2.Distance(transform.position,enemy.transform.position);
            if(minDistanceTemp < min && enemyTemp != enemyMin)
            {
                min = minDistanceTemp;
                enemyMin = enemyTemp;
                ProcessWeapon(enemyMin);
            }
        }
    }

    private void ProcessWeapon(Actor enemy)
    {

        Vector2 direction = enemy.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
        weapon.SetRotate(angle);
        weapon.Shoot(angle);

    }

    protected override void Move()
    {
        if (IsDead || m_isKnockBack) return;
        if (controller == Controller.moveByMouse)
        {
            MoveByMouse();
        }
        else if (controller == Controller.moveByKeyboard)
        {
            MoveByKeyBoard();
        }
    }
    
    private void MoveByKeyBoard()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        if (moveInput != Vector2.zero)
        {
            currentSpeed += increaseSpeed * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, playerStates.moveSpeed);
            m_rd.velocity = moveInput * currentSpeed * Time.deltaTime;
            m_anim.SetBool(AniimationConstant.player_Run, true);
        }
        else
        {
            BackToIdle();
        }
    }

    private void BackToIdle()
    {
        currentSpeed = 0;
        m_rd.velocity = Vector2.zero;
        m_anim.SetBool(AniimationConstant.player_Run, false);
    }

    private void MoveByMouse()
    {
        if(Input.GetMouseButton(0))
        {
            currentSpeed += increaseSpeed * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, playerStates.moveSpeed);

            float delta = currentSpeed * Time.deltaTime;
            float distanceToMousePos = Vector2.Distance(transform.position,mousePos);
            distanceToMousePos = Mathf.Clamp(distanceToMousePos, 0, maxDistance/3);
            
            delta *= distanceToMousePos;

            m_rd.velocity = movingDir * delta;
            float vecityLimitX = Mathf.Clamp(m_rd.velocityX, -velocityLimit.x, velocityLimit.x);
            float vecityLimitY = Mathf.Clamp(m_rd.velocityY, -velocityLimit.y, velocityLimit.y);

            m_rd.velocity = new Vector2(vecityLimitX,vecityLimitY);

            m_anim.SetBool(AniimationConstant.player_Run, true);
        }
        else
        {
            BackToIdle();
        }
    }

    private void LoadStats()
    {
        if(statsData == null) return;
        playerStates = (PlayerStates)statsData;
        playerStates.Load();
        CurrentHP = playerStates.hp;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color32(133, 250, 47, 50);
        Gizmos.DrawSphere(transform.position, radiusCheckEnemy);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(TagConstant.Enemy_Tag))
        {
            var damage  = collision.gameObject.GetComponent<Enemy>();
            Vector2 direction = collision.gameObject.transform.position - transform.position;
            TakeDamage(damage.statsData.damage);
            if(m_isKnockBack)
            {
                m_rd.velocity = direction * -statsData.knockBackForce * Time.deltaTime;
            }
        }
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
    public void AddXp(float xpBonus)
    {
        if(playerStates == null) return;

        playerStates.currentXp += xpBonus;

        playerStates.Upgrade(OnUpgradeState);

        OnAddXp?.Invoke();

        playerStates.Save();

    }    
    private void OnUpgradeState()
    {
        OnLevelUp?.Invoke();
    }
}
