using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarObject",menuName = "ScriptableObject/Cars")]
public class CarObject : ScriptableObject
{
    [SerializeField] public CarCheckPointHelper carCheckPointHelper;
    public int RacePositon;
}
