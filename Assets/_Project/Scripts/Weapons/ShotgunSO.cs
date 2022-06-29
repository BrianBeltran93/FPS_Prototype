using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="_ShotgunSO",menuName ="ScriptableObjects/FireType/SemiAutomaticSO/ShotgunSO")]

public class ShotgunSO : SemiAutomaticSO
{
    public float spread;
    public int amountOfPellets;
}
