using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshMaterial : MonoBehaviour
{
    [SerializeField] private List<MeshRenderer> weaponMeshes = new List<MeshRenderer>();
    [SerializeField] private Material gunReady;
    [SerializeField] private Material gunReloading;

    public void ChangeToReloadingMaterial()
    {
        foreach (var gunMesh in weaponMeshes)
        {
            gunMesh.material = gunReloading;
        }
    }

    public void ChangeToGunReadyMaterial()
    {
        foreach (var gunMesh in weaponMeshes)
        {
            gunMesh.material = gunReady;
        }
    }
}
