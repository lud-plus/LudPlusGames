using Fusion;
using System;
using UnityEngine;

/// <summary>
/// Playerクラス
/// 全ゲームで共通
/// ゲーム依存の処理は入れない
/// </summary>
public class Player : NetworkBehaviour
{
    public PlayerRef NetworkPlayerRef { get; private set; }

    public event Action<Player, Collider> OnTriggerEnterEvent = null;

    /// <summary>
    /// Spawnした時に呼ばれる
    /// </summary>
    public override void Spawned()
    {
        NetworkPlayerRef = Object.InputAuthority;
        PlayerManager.Instance.AddPlayer(this);
    }

    /// <summary>
    /// Despawnした時に呼ばれる
    /// </summary>
    /// <param name="runner"></param>
    /// <param name="hasState"></param>
    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        OnTriggerEnterEvent = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterEvent?.Invoke(this, other);
    }
}

