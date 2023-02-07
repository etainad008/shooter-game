using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemySpeed;
    public int enemyHealth;
    public int enemyDamage;
    public int enemyScoreValue;

    Rigidbody rb;
    Player player;
    public GameEvent OnUpdateScoreDisplay;
    public GameObject coin;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<Player>();
        int enemyLayer = LayerMask.NameToLayer("Enemies");
        gameObject.layer = enemyLayer;
        gameObject.transform.GetChild(0).GetComponent<Transform>().gameObject.layer = enemyLayer;
    }

    void FixedUpdate()
    {
        rb.position = Vector3.MoveTowards(rb.position, player.transform.position, enemySpeed * Time.fixedDeltaTime);
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        if(enemyHealth <= 0)
        {
            OnUpdateScoreDisplay.Raise(enemyScoreValue);
            SpawnCoin();
            Destroy(gameObject);
        }
    }

    public void SpawnCoin()
    {
        // Spawn a coin in the place of the enemy's death
        Instantiate(coin, rb.position, Quaternion.identity);
    }

    //private void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.CompareTag("Bullet"))
    //    {

    //        Bullet currentBullet = other.gameObject.GetComponent<Bullet>();

    //        if (!currentBullet)
    //        {
    //            return;
    //        }

    //        enemyHealth -= currentBullet.bulletDamage;

    //        if (enemyHealth <= 0)
    //        {
    //            uiDisplay.UpdateScoreDisplay(enemyScoreValue);
    //            currentBullet.SpawnCoin();
    //            Destroy(gameObject);
    //        }
    //    }
    //}
}
