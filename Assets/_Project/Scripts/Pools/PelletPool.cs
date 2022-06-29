using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletPool : MonoBehaviour
{
    public static PelletPool SharedInstance;
    public List<Rigidbody> pellets;
    public Rigidbody pellet;
    public int amountOfPellets;
    
    private int _pelletPoolIterator;


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
        pellets = new List<Rigidbody>();
        Rigidbody tmp;
        for (int i = 0; i < amountOfPellets; i++)
        {
            tmp = Instantiate(pellet);
            tmp.gameObject.SetActive(false);
            pellets.Add(tmp);
        }
    }

    public Rigidbody GetPooledObject()
    {
        for (; ; _pelletPoolIterator++)
        {
            if (_pelletPoolIterator >= amountOfPellets)
            {
                _pelletPoolIterator = 0;
            }
            
            if (!pellets[_pelletPoolIterator].gameObject.activeInHierarchy)
            {
                var pelletToReturn = pellets[_pelletPoolIterator];
                _pelletPoolIterator++;
                return pelletToReturn;
            }
        }
    }
}
