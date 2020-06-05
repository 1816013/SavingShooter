using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType    // アイテムが増えたときの生成時用
{
    EnergyPack
}

public class itemStatas : MonoBehaviour
{
    [SerializeField]
    private ItemType _itemType = new ItemType();
    [SerializeField]
    private float _destroyTime = 10.0f;

    // Start is called before the first frame update
    private void OnEnable()
    {
        _itemType = ItemType.EnergyPack;
        StartCoroutine(DelayObject(_destroyTime));
    }

    IEnumerator DelayObject(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    public ItemType GetItemType()
    {
        return _itemType;
    }
}
