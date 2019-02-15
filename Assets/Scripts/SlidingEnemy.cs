using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingEnemy : MonoBehaviour
{
    #region movement_variables
    public float movespeed;
    #endregion

    #region physics_component
    Rigidbody2D enemyRB;
    Vector2 currDir;
    public float startPos;
    #endregion

    //#region targeting_variables
    //public Transform player;
    //#endregion

    #region attack_variables
    public float explosionDamage;
    public float explosionRadius;
    public GameObject explosionObj;
    #endregion


    #region health_variables
    public float maxHealth;
    float currHealth;
    #endregion

    #region Unity_functions
    //Runs once on creation
    private void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        currHealth = maxHealth;

        currDir = Vector2.right;
        startPos = transform.position.x;

    }

    //Runs every Frame
    private void Update()
    {

        //transform.position = new Vector2(Mathf.PingPong(Time.time, 3) - transform.position.x, transform.position.y);
        if(transform.position.x - startPos >= 3)
        {
            currDir = Vector2.left;
            Vector2 currRot = transform.eulerAngles;
            currRot.y += 180;
            transform.eulerAngles = currRot;
        }
        else if (transform.position.x - startPos <= -3)
        {
            currDir = Vector2.right;
            Vector2 currRot = transform.eulerAngles;
            currRot.y += 180;
            transform.eulerAngles = currRot;
        }
        enemyRB.velocity = currDir;
        Debug.Log(transform.position.y);
    }
    #endregion


    #region attack_functions
    //Raycasts box for player and causes damage, spawns explosion prefab
    private void Explode()
    {
        //Call audio manager for explosion sound
        FindObjectOfType<AudioManager>().Play("Explosion");

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, explosionRadius, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Player"))
            {
                //Cause damage
                hit.transform.GetComponent<PlayerController>().TakeDamage(explosionDamage);
                Debug.Log("Hit Player with explosion");

                //Spawn explosion prefab
                Instantiate(explosionObj, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Explode();
        }
    }
    #endregion

    #region health_functions
    //Enemy takes damage based on 'value' param
    public void TakeDamage(float value)
    {
        //Call audio manager for explosion sound
        FindObjectOfType<AudioManager>().Play("BatHurt");

        //Decrement Health
        currHealth -= value;
        Debug.Log("Health is now" + currHealth.ToString());

        //Check for death
        if (currHealth <= 0)
        {
            Die();
        }
    }

    //Destroys enemy object
    void Die()
    {
        //Destroy Gameobject
        Destroy(this.gameObject);
    }
    #endregion
}
