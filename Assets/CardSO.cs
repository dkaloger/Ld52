using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Card", order = 1)]
public class CardSO : ScriptableObject
{
    public GameObject Prefab;
    public Sprite Sprite;
    public bool ispickable;
    public Restriction[] restrictions;
    [System.Serializable]
    public class Restriction
    {
       public Vector3Int direction;
       public CardSO card;
       public bool anydirection;

    }
}
