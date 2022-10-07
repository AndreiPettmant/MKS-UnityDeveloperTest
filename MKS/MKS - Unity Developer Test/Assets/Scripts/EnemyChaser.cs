using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    [Header("Object Components")]
    [SerializeField] private Transform Target;
    [SerializeField] private EnemyHealthBar enemyHealthBar;
    [SerializeField] private GameObject explosionEffect;

    [Header("Enemy Configurations")]
    [SerializeField][Range(0f, 1f)] private float explosionDamage;
    [SerializeField][Range(0f, 1f)] private float moveSpeed;
    [SerializeField][Range(0f, 1f)] private float lineOfSight;

    [Header("Score value")]
    [SerializeField][Range(0f, 1f)] private int enemyPoint;
    
    private Rigidbody2D enemyRB;
    private Vector2 movement;
    private PlayerHealthBar playerHealthBar;
    private ScoreHandler scoreHandler;
    private GameHandler gameHandler;
    private CapsuleCollider2D capsuleCollider2D;
    private bool canScore;

    private void Awake()
    {
        FadeOut();
        LoadComponents();
    }

    private void FixedUpdate()
    {
        FadeIn();
        EnemyBehaviour();
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

                if (distanceFromPlayer < lineOfSight)
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, Target.position, moveSpeed * Time.deltaTime);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.name == "Player")
        {
            canScore = false;
            capsuleCollider2D.enabled = false;

            playerHealthBar.health -= explosionDamage;
            playerHealthBar.CheckBoatStatus(playerHealthBar.health);

            GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity) as GameObject;

            Destroy(explosion, 0.2f);
            enemyHealthBar.health = 0f;
        }

        if(collision.gameObject.tag == "CannonBall")
        {
            canScore = true;

            enemyHealthBar.health -= 0.1f;
            enemyHealthBar.CheckBoatStatus(enemyHealthBar.health);
        }
    }

    private void OnDestroy()
    {
        capsuleCollider2D.enabled = false;

        if (!gameHandler.isOver)
        {
            if (canScore)
            {
                scoreHandler.score += enemyPoint;
                scoreHandler.UpdateScore();
            }
        }
    }

    private void FadeIn()
    {
        iTween.FadeUpdate(this.gameObject, 1, 2f) ;
    }
    private void FadeOut()
    {
        iTween.FadeUpdate(this.gameObject, 0, 0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
    }

    private void LoadComponents()
    {
        enemyRB = this.GetComponent<Rigidbody2D>();
        capsuleCollider2D = this.GetComponent<CapsuleCollider2D>();

        Target = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealthBar = GameObject.FindGameObjectWithTag("PlayerHealthBar").GetComponent<PlayerHealthBar>();
        scoreHandler = GameObject.FindGameObjectWithTag("ScoreHandler").GetComponent<ScoreHandler>();
        gameHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();
    }
}
