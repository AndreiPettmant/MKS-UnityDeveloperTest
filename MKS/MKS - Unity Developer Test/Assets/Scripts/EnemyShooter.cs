using Unity.VisualScripting;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header("Object Components")]
    [SerializeField] private EnemyHealthBar enemyHealthBar;
    [SerializeField] private Transform Target;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletParent;

    [Header("Enemy Configurations")]
    [SerializeField][Range(0f, 1f)] private float shootDamage;
    [SerializeField][Range(0f, 1f)] public float fireRate;
    [SerializeField][Range(0f, 1f)] private float nextFireTime;
    [SerializeField][Range(0f, 1f)] private float moveSpeed;
    [SerializeField][Range(0f, 1f)] private float lineOfSight;
    [SerializeField][Range(0f, 1f)] private float shootingRange;

    [Header("Score value")]
    [SerializeField]private int enemyPoint;

    [Header("SFX")]
    [SerializeField] private AudioSource hitSFX;
    [SerializeField] private AudioSource shotSFX;

    private Rigidbody2D enemyRB;
    private Vector2 movement;
    private ScoreHandler scoreHandler;
    private GameHandler gameHandler;

    private void Awake()
    {
        FadeOut();
        LoadComponents();
    }

    private void Update()
    {
        FadeIn();
        EnemyBehaviour();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "CannonBall")
        {
            hitSFX.Play();
            enemyHealthBar.health -= 0.1f;
            enemyHealthBar.CheckBoatStatus(enemyHealthBar.health);
        }
    }

    private void OnDestroy()
    {
        if (!gameHandler.isOver)
        {
            scoreHandler.score += enemyPoint;
            scoreHandler.UpdateScore();
        }
    }

    private void FadeIn()
    {
        iTween.FadeUpdate(this.gameObject, 1, 2f);
    }

    private void FadeOut()
    {
        iTween.FadeUpdate(this.gameObject, 0, 0f);
    }

    private void LoadComponents()
    {
        enemyRB = this.GetComponent<Rigidbody2D>();
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        scoreHandler = GameObject.FindGameObjectWithTag("ScoreHandler").GetComponent<ScoreHandler>();
        gameHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();
    }

    private void EnemyBehaviour()
    {
        if (!gameHandler.isOver)
        {
            if (enemyHealthBar.health > 0)
            {
                Vector3 direction = Target.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
                enemyRB.rotation = angle;
                direction.Normalize();
                movement = direction;

                float distanceFromPlayer = Vector2.Distance(Target.position, transform.position);

                if (distanceFromPlayer < lineOfSight && distanceFromPlayer > shootingRange)
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, Target.position, moveSpeed * Time.deltaTime);
                }
                else if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time)
                {
                    shotSFX.Play();
                    GameObject cannonBallEnemy = Instantiate(bulletPrefab, bulletParent.transform.position, Quaternion.identity);
                    nextFireTime = Time.time + fireRate;
                }
            }
        }
    }
}
