using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item1",menuName ="Add_Item/Item")]
public class Item : ScriptableObject
{
    [SerializeField] public GameObject itemPrefab;
    [SerializeField] public Texture itemImage;
}
