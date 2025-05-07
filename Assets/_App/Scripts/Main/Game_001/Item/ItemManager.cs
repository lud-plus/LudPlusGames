using MainGame_001;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムを管理するClass
/// </summary>
public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] itemBaseTable = null;

    private List<IItem> itemList = new List<IItem>();

    /// <summary>
    /// アイテムを同期オブジェクトとしてSpawnする
    /// </summary>
    public void SpawnItem(IItem item)
    {
        var transform = item.GetNetWorkObject().transform;
        var spawn = NetworkManager.Instance.NetworkRunner.Spawn(itemBaseTable[(int)item.GetItemType()], transform.localPosition, transform.rotation);
        if(spawn.TryGetComponent<IItem>(out var spawnItem))
        {
            spawnItem.Copy(item);
            itemList.Add(spawnItem);
        }
    }

    private void Awake()
    {
        if(Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public static ItemManager Instance { get; private set; } = null;
}

