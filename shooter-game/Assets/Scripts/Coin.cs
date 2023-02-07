using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float lifeTime;

    public GameEvent OnCollectingCoin;

    void Start()
    {
        Invoke(nameof(DestroyCoin), lifeTime);
    }


    void DestroyCoin()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            OnCollectingCoin.Raise();
            DestroyCoin();
        }
    }
}
