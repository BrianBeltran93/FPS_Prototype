using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] protected Transform bulletSpawn;
    [SerializeField] private MeshMaterial meshMaterial;
    [SerializeField] protected FireTypeSO fireTypeSO;
    [SerializeField] private FireType fireType;
    
    protected Rigidbody _projectileRigidbody;
    
    private bool _isOnCooldown;
    private bool _isReloading;
    private int _currentAmmoInWeapon;
    private OVRGrabbable _ovrGrabbable;

    private const int OUTOFAMMO = 0;

    enum FireType
    {
        SemiAuto,
        Automatic
    }
    
    private bool IsWeaponUnableToFire => !_ovrGrabbable.isGrabbed || _isOnCooldown || _isReloading;
    
    
    private void Start()
    {
        SetGunTypeSettings();
        
        _ovrGrabbable = GetComponent<OVRGrabbable>();
    }

    private void SetGunTypeSettings()
    {
        switch (fireType)
        {
            case FireType.SemiAuto:
                InputEventManager.RightTriggerPressed += FireWeapon;
                break;
            case FireType.Automatic:
                InputEventManager.RightTriggerHeld += FireWeapon;
                break;
        }

        RestoreAmmoCount();
    }

    private void RestoreAmmoCount()
    {
        _currentAmmoInWeapon = fireTypeSO.maxAmmo;
    }
    
    private void OnDestroy()
    {
        switch (fireType)
        {
            case FireType.SemiAuto:
                InputEventManager.RightTriggerPressed -= FireWeapon;
                break;
            case FireType.Automatic:
                InputEventManager.RightTriggerHeld -= FireWeapon;
                break;
        }
    }
    
    private void FireWeapon()
    {
        if(IsWeaponUnableToFire)
            return;
        
        if (_currentAmmoInWeapon == OUTOFAMMO )
        {
            Reload();
            return;
        }

        LaunchProjectile();
    }
        
    private void Reload()
    {
        _isReloading = true;
        RestoreAmmoCount();
        StartCoroutine(Reloading());
    }

    private void LaunchProjectile()
    {
        SpawnProjectile();
        ShootProjectile();
        _currentAmmoInWeapon--;
        StartCoroutine(Cooldown());
    }

    protected virtual void SpawnProjectile()
    {
        _projectileRigidbody = BulletPool.SharedInstance.GetPooledObject();
        _projectileRigidbody.transform.SetPositionAndRotation(bulletSpawn.position, bulletSpawn.rotation);
    }
  
    protected virtual void ShootProjectile()
    {
        _projectileRigidbody.gameObject.SetActive(true);
        _projectileRigidbody.AddForce(bulletSpawn.forward * fireTypeSO.launchVelocity, ForceMode.Impulse);
        _projectileRigidbody = null;
    }

    private IEnumerator Reloading()
    {
        meshMaterial.ChangeToReloadingMaterial();

        yield return new WaitForSeconds(fireTypeSO.reloadTime);
        
        meshMaterial.ChangeToGunReadyMaterial();
        
        _isReloading = false;
    }
    
    private IEnumerator Cooldown()
    {
        _isOnCooldown = true;
        yield return new WaitForSeconds(fireTypeSO.timeBetweenConsecutiveShots);
        _isOnCooldown = false;
    }
}
