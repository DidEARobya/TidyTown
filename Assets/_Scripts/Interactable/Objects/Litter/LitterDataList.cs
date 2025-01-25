using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/LitterDataList")]
public class LitterDataList : ScriptableObject
{
    //Only Create One
    [SerializeField]
    public List<LitterData> litterData;
}
