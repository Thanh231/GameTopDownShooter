using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector2 direction;
    [SerializeField]private float speed;
    public float damage;
    RaycastHit2D hit;
    public GameObject bodyhit;
    public LayerMask enemyLayer;

    private void Awake()
    {
        Destroy(gameObject, 5f);
        direction = transform.up.normalized;
    }
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime,Space.World);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(TagConstant.Enemy_Tag))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            if(bodyhit != null)
            {
                Instantiate(bodyhit,collision.transform.position,Quaternion.identity);
            }
            Destroy(gameObject);
        }    
    }

}
