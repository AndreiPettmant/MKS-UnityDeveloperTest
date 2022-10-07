using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject target;
    [SerializeField] private Rigidbody2D bulletRB;
    [SerializeField] private GameObject hitEffect;

    [Header("Bullet speed")]
    [SerializeField][Range(0f, 1f)] private float bulletSpeed;


    void Start()
    {
        bulletRB = this.GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 direction = (target.transform.position - transform.position).normalized * bulletSpeed;
        bulletRB.velocity = new Vector2(direction.x, direction.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity) as GameObject;
        Destroy(effect, 0.2f);
        Destroy(gameObject);
    }
}
