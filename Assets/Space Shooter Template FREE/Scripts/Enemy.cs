using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script defines 'Enemy's' health and behavior. 
/// </summary>
public class Enemy : MonoBehaviour {

    #region FIELDS
    [Tooltip("Health points in integer")]
    public int health;

    [Tooltip("Enemy's projectile prefab")]
    public GameObject Projectile;

    [Tooltip("VFX prefab generating after destruction")]
    public GameObject destructionVFX;
    public GameObject hitEffect;
    
    [HideInInspector] public int shotChance; //probability of 'Enemy's' shooting during tha path
    [HideInInspector] public float shotTimeMin, shotTimeMax; //max and min time for shooting from the beginning of the path
    #endregion

    private void Start()
    {
        Invoke("ActivateShooting", Random.Range(shotTimeMin, shotTimeMax));
    }

    //coroutine making a shot
    void ActivateShooting() 
    {
        if (Random.value < (float)shotChance / 100)                             //if random value less than shot probability, making a shot
        {                         
            Instantiate(Projectile,  gameObject.transform.position, Quaternion.identity);             
        }
    }

    //method of getting damage for the 'Enemy'
    public void GetDamage(int damage) 
    {
        health -= damage;           //reducing health for damage value, if health is less than 0, starting destruction procedure
        if (health <= 0)
            Destruction();
        else
            Instantiate(hitEffect,transform.position,Quaternion.identity,transform);
    }

    // CHANGE 1: Use 'OnCollisionEnter2D' instead of 'OnTriggerEnter2D'
    // CHANGE 2: The parameter is 'Collision2D', not 'Collider2D'
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // CHANGE 3: We check 'collision.gameObject.tag' (or use CompareTag)
        if (collision.gameObject.CompareTag("Player"))
        {
            // Your original damage logic stays the same
            if (Projectile != null && Projectile.GetComponent<Projectile>() != null)
            {
                Player.instance.GetDamage(Projectile.GetComponent<Projectile>().damage);
            }
            else
            {
                Player.instance.GetDamage(1);
            }

            // OPTIONAL: Usually, when an enemy hits a player, the enemy should die too.
            // If you don't destroy the enemy, they will just bounce off the player.
            // Destroy(gameObject); 
        }
    }

    //method of destroying the 'Enemy'
    void Destruction()                           
    {        
        Instantiate(destructionVFX, transform.position, Quaternion.identity); 
        // 1. Find the GameManager in the scene
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();

        // 2. Add points (e.g., 10 points)
        if (scoreManager != null)
        {
            scoreManager.AddScore(10);
        }

        // 3. Then destroy the enemy
        Destroy(gameObject);
    }
}
