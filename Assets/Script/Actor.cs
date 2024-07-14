using System.Collections;
using UDEV;
using UnityEngine;
using UnityEngine.Events;

public class Actor : MonoBehaviour
{
    public ActorStats statsData;

    [LayerList]
    public int m_invicibleLayer;
    [LayerList]
    public int m_normalLayer;

    public Weapon weapon;

    protected bool m_isKnockBack;
    protected bool m_isInvicible;
    protected bool m_isDead;
    private float currentHP;

    protected Rigidbody2D m_rd;
    protected Animator m_anim;

    [Header("Event")]
    public UnityEvent onInit;
    public UnityEvent onTakeDamage;
    public UnityEvent onDead;

    public bool IsDead { get => m_isDead; set => m_isDead = value; }
    public float CurrentHP { get => currentHP; set => currentHP = value; }


    protected virtual void Awake()
    {
        m_rd = GetComponent<Rigidbody2D>();
        m_anim = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        Init();

        onInit?.Invoke();
    }
    public virtual void Init()
    {

    }
    public virtual void TakeDamage(float damage)
    {
        if (m_isInvicible) return;
        CurrentHP -= damage;
         KnockBack();
        if (CurrentHP < 0)
        {
            CurrentHP = 0;
            Die();
        }
        onTakeDamage?.Invoke();
    }

    public virtual void Die()
    {
        m_isDead = true;
        //m_rd.velocity = Vector2.zero;

        onDead?.Invoke();
        Destroy(gameObject, 0.5f);
    }

    
    protected virtual void Move()
    {
  
    }
    public void KnockBack()
    {
        if (m_isInvicible || m_isKnockBack || m_isDead) return;
        m_isKnockBack = true;
        StartCoroutine(StopKnockBack());
    }
    IEnumerator StopKnockBack()
    {
        yield return new WaitForSeconds(statsData.knockBackTime);

        m_isKnockBack = false;
        m_isInvicible = true;

        gameObject.layer = m_invicibleLayer;
        StartCoroutine(StopInvicible());
    }

    IEnumerator StopInvicible()
    {
        yield return new WaitForSeconds(statsData.invicibleTime);
        gameObject.layer = m_normalLayer;
        m_isInvicible = false;
    }
}
