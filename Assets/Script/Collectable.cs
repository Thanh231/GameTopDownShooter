using System.Collections;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int minBonus;
    public int maxBonus;
    public int lifeTime;
    public int flyForce;
    private float countTime;

    protected int bonus;
    protected Player player;

    private Rigidbody2D rb;
    private FlashVfx flashVfx;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        flashVfx = GetComponent<FlashVfx>();
        countTime = (float)lifeTime;
        bonus = Random.Range(minBonus, maxBonus);
        player = GameManager.Ins.Player;
        FLy();
        StartCoroutine(CountDown());
        FlashVFXComplete();
    }
    private void FlashVFXComplete()
    {
        if(flashVfx != null)
        {
            flashVfx.OnCompleted.RemoveAllListeners();
            flashVfx.OnCompleted.AddListener(OnDestroyCollectable);
        }
    }
    private void OnDestroyCollectable()
    {
        Destroy(gameObject);
    }

    private void FLy()
    {
        if(rb != null)
        {
            float forceX = UnityEngine.Random.Range(-flyForce, flyForce);
            float forceY = UnityEngine.Random.Range(-flyForce, flyForce);
            rb.velocity = new Vector2(forceX,forceY) * Time.deltaTime;
            StartCoroutine(StopFly());
        }
    }

    private IEnumerator StopFly()
    {
        yield return new WaitForSeconds(0.08f);
        if(rb != null )
        {
            rb.velocity = Vector2.zero;
        }
    }

    private IEnumerator CountDown()
    {
        while(countTime > 0)
        {
            float timeValue = Mathf.Round(countTime / lifeTime);
            yield return new WaitForSeconds(1f);
            countTime--;
            if (timeValue <= 0.3f && flashVfx != null)
            {
                flashVfx.Flash(countTime);
            }
        }
        
    }

    public virtual void Trigger()
    {

    }
    public virtual void Init()
    {

    }
    
}

