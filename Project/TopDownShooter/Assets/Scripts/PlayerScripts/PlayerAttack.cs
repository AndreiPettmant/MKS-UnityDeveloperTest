using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] private Transform singleFirePoint;
    [SerializeField] private Transform[] burstFirePoint;
    [Header("Shot prefab")]
    [SerializeField] private GameObject bulletPrefab;
    [Header("Shot configurations")]
    [SerializeField][Range(0f, 5f)] private float bulletSpeed;
    [SerializeField][Range(0f, 5f)] private float fireRate;
    [Header("SFX")]
    [SerializeField] private AudioSource shotSFX;

    private PlayerHealthBar healthBar;
    private GameHandler gameHandler;
    private float nexFire = 0f;

    private void Awake()
    {
        healthBar = GameObject.FindGameObjectWithTag("PlayerHealthBar").GetComponent<PlayerHealthBar>();
        gameHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();
    }

    private void Update()
    {
        if (!gameHandler.isOver)
        {
            if (healthBar.health > 0)
            {
                if (Input.GetButtonDown("Fire1") && Time.time > nexFire)
                {
                    SingleShot();
                    nexFire = Time.time + fireRate;
                }

                if (Input.GetButtonDown("Fire2") && Time.time > nexFire)
                {
                    MultipleShot();
                    nexFire = Time.time + fireRate;
                }
            }
        }
    }

    private void SingleShot()
    { 
        GameObject cannonBall = Instantiate(bulletPrefab, singleFirePoint.position, singleFirePoint.rotation);
        Rigidbody2D cannonBallRB = cannonBall.GetComponent<Rigidbody2D>();

        cannonBallRB.AddForce(singleFirePoint.up * bulletSpeed, ForceMode2D.Impulse);
        shotSFX.Play();
    }

    private void MultipleShot()
    {
       
        foreach (Transform cannon in burstFirePoint)
        {
            GameObject cannonBall = Instantiate(bulletPrefab, cannon.position, cannon.rotation);
            Rigidbody2D cannonBallRB = cannonBall.GetComponent<Rigidbody2D>();

            cannonBallRB.AddForce(cannon.up * bulletSpeed, ForceMode2D.Impulse);
            shotSFX.Play();
        }
    }
}
