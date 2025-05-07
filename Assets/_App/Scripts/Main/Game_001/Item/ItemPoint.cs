using Fusion;
using UnityEngine;

namespace MainGame_001
{

/// <summary>
/// Pointアイテム
/// </summary>
public class ItemPoint : MonoBehaviour, IItem
{
    [SerializeField]
    private NetworkObject networkObject = null;

    [SerializeField]
    private ItemType itemType = ItemType.Point1;
    [SerializeField]
    private int value = 10;

    /// <summary>
    /// NetworkObjectを取得
    /// </summary>
    /// <returns></returns>
    public NetworkObject GetNetWorkObject()
    {
        return networkObject;
    }

    /// <summary>
    /// アイテムの種類を取得
    /// </summary>
    /// <returns></returns>
    public ItemType GetItemType()
    {
        return itemType;
    }

    /// <summary>
    /// アイテムの値を取得
    /// </summary>
    /// <returns></returns>
    public int GetValue()
    {
        return value;
    }

    /// <summary>
    /// 渡されたデータの内容をコピー
    /// </summary>
    /// <param name="src"></param>
    public void Copy(IItem src)
    {
        itemType = src.GetItemType();
        value = src.GetValue();
    }
}

}
