using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector2 direction;
    [SerializeField]private float speed = 10;
    [SerializeField]private float damage;
    RaycastHit2D hit;
    public GameObject bodyhit;
    

    void Update()
    {
        transform.Translate(direction *  speed * Time.deltaTime);
        hit = Physics2D.Raycast(transform.position, direction * 0.5f);
        if (!hit || hit.collider == null) return;
        
        Collider2D col = hit.collider;
        if(col.CompareTag(TagConstant.Enemy_Tag))
        {
            TakeDameToEnemy(col);
        }
    }

    private void TakeDameToEnemy(Collider2D col)
    {
        Actor enemyActor = col.GetComponent<Actor>();

        if(enemyActor != null )
        {
            enemyActor.TakeDamage(damage);
            Instantiate(bodyhit,hit.point, Quaternion.identity);
        }

    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }
}
