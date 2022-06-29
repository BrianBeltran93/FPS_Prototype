using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun_Gun : Gun
{
    [SerializeField] private List<Rigidbody> _pelletRigidbodies = new List<Rigidbody>();
    [SerializeField] private ShotgunSO shotgunSO;
    
    

    
    protected override void SpawnProjectile()   
    {
        for (int i = 0; i < shotgunSO.amountOfPellets; i++)
        {
            _pelletRigidbodies.Add(PelletPool.SharedInstance.GetPooledObject());
            _pelletRigidbodies[i].transform.SetPositionAndRotation(bulletSpawn.position, bulletSpawn.rotation);
        }
    }
    
    protected override void ShootProjectile()
    {
        for (int i = 0; i < shotgunSO.amountOfPellets; i++)
        {
            
            Vector3 direction = bulletSpawn.forward;
            Vector3 spread = Vector3.zero;
    
            spread+= bulletSpawn.transform.up * Random.Range(-1f, 1f); 
            spread+= bulletSpawn.transform.right * Random.Range(-1f, 1f);
        
            direction += spread.normalized * Random.Range(0f, shotgunSO.spread);
            
            _pelletRigidbodies[i].gameObject.SetActive(true);
            _pelletRigidbodies[i].AddForce(direction * fireTypeSO.launchVelocity, ForceMode.Impulse);
            
        }
        _pelletRigidbodies.Clear();
    }
}
