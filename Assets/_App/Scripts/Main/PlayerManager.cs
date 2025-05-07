using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Playerを管理するclass
/// </summary>
public class PlayerManager : MonoBehaviour
{
    private List<Player> playerList = new List<Player>();

    public event Action<Player> OnAddPlayerEvent = null;

    /// <summary>
    /// Playerを追加
    /// </summary>
    public void AddPlayer(Player player)
    {
        playerList.Add(player);
        OnAddPlayerEvent?.Invoke(player);
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

    private void OnDestroy()
    {
        OnAddPlayerEvent = null;
    }

    public static PlayerManager Instance { get; private set; } = null;
}

