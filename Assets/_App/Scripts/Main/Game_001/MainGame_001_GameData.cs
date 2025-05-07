using Fusion;
using UnityEngine;

namespace MainGame_001
{

/// <summary>
/// 同期するゲームデータ
/// </summary>
public class MainGame_001_GameData: NetworkBehaviour
{
    [Networked]
    [HideInInspector]
    public int Score { get; private set; } = 0;

    /// <summary>
    /// アイテムを獲得
    /// </summary>
    public void CollectItem(IItem item)
    {
        Score += item.GetValue();
    }
}

}

