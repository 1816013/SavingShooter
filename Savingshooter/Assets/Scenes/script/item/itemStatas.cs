using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType    // アイテムが増えたときの生成時用
{
    EnergyPack
}

public class itemStatas : MonoBehaviour
{
    public ItemType itemType;

    // Start is called before the first frame update
    void Start()
    {
        itemType = ItemType.EnergyPack;
    }
    public ItemType GetItemType()
    {
        return itemType;
    }
}
