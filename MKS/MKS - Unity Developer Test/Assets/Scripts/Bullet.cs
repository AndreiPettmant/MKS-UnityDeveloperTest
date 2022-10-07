using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject HitEffect;
    [SerializeField][Range(0f, 1f)] public float damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(HitEffect, transform.position, Quaternion.identity) as GameObject;
        
        Destroy(effect, 0.2f);
        Destroy(gameObject);
    }
}
