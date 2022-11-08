using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Speed")]
    [SerializeField][Range(1f, 5f)] private int movementSpeed;
    [SerializeField][Range(0f, 1f)] private float amountDamageTaken;
    
    [Header("SFX")]
    [SerializeField] private AudioSource hitSFX;
    
    private Camera cam;
    private Vector2 movement;
    private Vector2 mousePosition;
    private PlayerHealthBar healthBar;
    private GameHandler gameHandler;
    private Rigidbody2D PlayerRB;

    private void Awake()
    {
        PlayerRB = GetComponent<Rigidbody2D>();
        healthBar = GameObject.FindGameObjectWithTag("PlayerHealthBar").GetComponent<PlayerHealthBar>();
        gameHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    { 
        if (!gameHandler.isOver)
        {
            if (healthBar.health > 0f)
            {
                movement.x = Input.GetAxis("Horizontal");
                movement.y = Input.GetAxis("Vertical");

                mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }

    private void FixedUpdate()
    {
        PlayerRB.MovePosition(PlayerRB.position + movement * movementSpeed * Time.fixedDeltaTime);

        Vector2 lookDirection = mousePosition - PlayerRB.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + 90f;

        PlayerRB.rotation = angle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "CannonBall")
        {
            hitSFX.Play();
            healthBar.health -= amountDamageTaken;
            healthBar.CheckBoatStatus(healthBar.health);
        }
    }
}
