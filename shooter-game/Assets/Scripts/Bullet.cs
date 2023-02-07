using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage;

    Rigidbody rb;
    Weapon weapon;
    UIDisplay uiDisplay;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        weapon = FindObjectOfType<Weapon>();
        uiDisplay = FindObjectOfType<UIDisplay>();

        rb.AddForce(transform.forward * weapon.shotForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Enemy>() != null)
        {
            Enemy currentEnemy = other.gameObject.GetComponent<Enemy>();
            currentEnemy.TakeDamage(bulletDamage);
        }

        Destroy(gameObject);
    }
}
