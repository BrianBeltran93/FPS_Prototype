using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int projectileDeactivationTimer;
    [SerializeField] private Rigidbody thisRigidbody;
    [SerializeField] private List<Collider> targetColliders;

    private GameObject _thisGameObject;
    private Vector3 _bulletLastPosition;
    private RaycastHit _objectHit;


    private void Start()
    {
        targetColliders = TargetPool.SharedInstance.GetPooledObject();
    }

    private void OnEnable()
    {
        _thisGameObject = gameObject;
        _bulletLastPosition = transform.position;
        StartCoroutine(DeactivateProjectile());
    }

    private void OnDisable()
    {
        thisRigidbody.velocity = Vector3.zero;
        thisRigidbody.angularVelocity = Vector3.zero;
        thisRigidbody.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        var currentPosition = transform.position;

        if (HasCollidedWithAnotherObject(currentPosition))
        {
            CheckIfTargetHit();
            DeactivateObjectImmediately();
        }
        
        _bulletLastPosition = currentPosition;
    }

    private bool HasCollidedWithAnotherObject(Vector3 currentPosition)
    {
        return Physics.Raycast(new Ray(_bulletLastPosition, (currentPosition - _bulletLastPosition).normalized),
            out _objectHit, (currentPosition - _bulletLastPosition).magnitude);
    }

    private void CheckIfTargetHit()
    {
        if (targetColliders.Contains(_objectHit.collider))
        {
            _objectHit.transform.gameObject.GetComponent<Target>().IncreaseHitCount();
        }
    }
    
    private void DeactivateObjectImmediately()
    {
        StopCoroutine(DeactivateProjectile());
        _thisGameObject.SetActive(false);
    }

    private IEnumerator DeactivateProjectile()
    {
        yield return new WaitForSeconds(projectileDeactivationTimer);
        _thisGameObject.SetActive(false);
    }
}
