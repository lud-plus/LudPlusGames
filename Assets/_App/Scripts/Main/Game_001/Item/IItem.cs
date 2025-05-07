using UnityEngine;

namespace MainGame_001
{

/// <summary>
/// アイテムで継承するインターフェース
/// </summary>
public interface IItem : INetworkObject
{
    ItemType GetItemType();
    int GetValue();

    void Copy(IItem src);
}

}
