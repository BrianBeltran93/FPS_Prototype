using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPool : MonoBehaviour
{
    public static TargetPool SharedInstance;
    public List<Collider> targets;
    public GameObject target;
    public int amountOfTargets;
    public List<Transform> targetSpawnPoints;


    private void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
        }

        InstantiateTargets();
    }

    private void InstantiateTargets()
    {
        targets = new List<Collider>();
        GameObject tmp;
        for (int i = 0; i < amountOfTargets; i++)
        {
            if (i >= targetSpawnPoints.Count)
                return;
            
            tmp = Instantiate(target, targetSpawnPoints[i]);
            tmp.gameObject.SetActive(true);
            targets.Add(tmp.GetComponentInChildren<Collider>());
        }
    }
    
    public List<Collider> GetPooledObject()
    {
        return targets;
    }
}
