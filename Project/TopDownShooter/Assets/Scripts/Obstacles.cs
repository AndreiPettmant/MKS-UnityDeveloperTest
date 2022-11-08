using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [Header("SFX")]
    [SerializeField] private AudioSource cannonBallExplosionSFX;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "CannonBall")
        {
            cannonBallExplosionSFX.Play();
        }
    }
}
