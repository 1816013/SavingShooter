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
    public float destroyTime = 10.0f;

    // Start is called before the first frame update
    private void OnEnable()
    {
        itemType = ItemType.EnergyPack;
        StartCoroutine(DelayObject(destroyTime));
    }

    IEnumerator DelayObject(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    public ItemType GetItemType()
    {
        return itemType;
    }
}
