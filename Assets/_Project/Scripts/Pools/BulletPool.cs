using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool SharedInstance;
    public List<Rigidbody> bullets;
    public Rigidbody bullet;
    public int amountOfBullets;
    
    private int _bulletPoolIterator;


    private void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
        }

        InstantiateBullets();
    }

    private void InstantiateBullets()
    {
        bullets = new List<Rigidbody>();
        Rigidbody tmp;
        for (int i = 0; i < amountOfBullets; i++)
        {
            tmp = Instantiate(bullet);
            tmp.gameObject.SetActive(false);
            bullets.Add(tmp);
        }
    }

    public Rigidbody GetPooledObject()
    {
        for (; ; _bulletPoolIterator++)
        {
            if (_bulletPoolIterator >= amountOfBullets)
            {
                _bulletPoolIterator = 0;
            }
            
            if (!bullets[_bulletPoolIterator].gameObject.activeInHierarchy)
            {
                return bullets[_bulletPoolIterator];
            }
        }
    }
}
