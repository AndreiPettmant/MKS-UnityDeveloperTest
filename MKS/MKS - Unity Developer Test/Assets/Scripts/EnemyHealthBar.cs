using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    [Header("Object transforms")]
    [SerializeField] private Transform barParent;
    [SerializeField] private Transform healthBar;
    
    [Header("Enemy object")]
    [SerializeField] private GameObject boat;
    
    [Header("Enemy sprites")]
    [SerializeField] private SpriteRenderer boatSprite;
    [SerializeField] private Sprite boatDamageLow;
    [SerializeField] private Sprite boatDamageHigh;
    [SerializeField] private Sprite boatDestroyed;
    
    [Header("Enemy health configurations")]
    [SerializeField][Range(0f, 1f)] private float healthAmountToLowDeterioration;
    [SerializeField][Range(0f, 1f)] private float healthAmountToHighDeterioration;
    [SerializeField][Range(0f, 1f)] private float healthAmountToTotalDeterioration;
    [Range(0f, 1f)] public float health;

    [Header("Fade value")]
    [SerializeField] private float fadeTime;
    

    public void Start()
    {
        this.SetSize(health);
        InvokeRepeating("ChangeBoatSprite", 0, 0.1f);
    }

    public void SetSize(float sizeNormalized)
    {
        if (health > healthAmountToTotalDeterioration)
            this.barParent.localScale = new Vector3(sizeNormalized, 1f);
    }

    public void CheckBoatStatus(float damage)
    {
        if (health > healthAmountToTotalDeterioration)
            this.healthBar.localScale = new Vector3(damage, 1f);
    }

    private void ChangeBoatSprite()
    {

        if (health < healthAmountToLowDeterioration)
        {
            boatSprite.sprite = boatDamageLow;
        }
        if (health < healthAmountToHighDeterioration)
        {
            boatSprite.sprite = boatDamageHigh;

        }
        if (health <= healthAmountToTotalDeterioration)
        {
            boatSprite.sprite = boatDestroyed;
            FadeBoat();
        }

    }

    private void FadeBoat()
    {
        iTween.FadeTo(boat, 0f, fadeTime);
        iTween.ScaleTo(boat, new Vector2(0f, 0f), 10f);
        Destroy(boat, 0.5f);
    }
}
