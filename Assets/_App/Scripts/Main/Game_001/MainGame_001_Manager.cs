using Fusion;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

namespace MainGame_001
{

/// <summary>
/// ゲーム全体を管理するclass
/// </summary>
public class MainGame_001_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerBase = null;

    [SerializeField]
    private Transform fieldRoot = null;

    [SerializeField]
    private Transform playerRoot = null;

    [SerializeField]
    private CinemachineCamera cinemachineCamera = null;

    [SerializeField]
    private Image dummyFilterImage = null;

    /// <summary>
    /// PlayerがSpawnされて、PlayerManagerに追加された時に呼ばれる
    /// </summary>
    private void OnAddPlayer(Player player)
    {
        player.OnTriggerEnterEvent += OnPlayerTriggerEnter;

        dummyFilterImage.gameObject.SetActive(false);
    }

    /// <summary>
    /// GameStart時に呼ばれる
    /// </summary>
    private void OnGameStart(NetworkRunner runner)
    {
    }

    /// <summary>
    /// プレイヤーがセッションした時に呼ばれる
    /// まだSpawnはしてない
    /// Spawnした時に呼ばれるのは、OnAddPlayer
    /// </summary>
    private void OnjoinedPlayer(NetworkRunner runner, PlayerRef playerRef)
    {
        Debug.Log($"[fusion join player]player id:{playerRef.PlayerId},is host:{playerRef.IsMasterClient}");

        if(playerRef == runner.LocalPlayer)
        {
            var spawnPlayer = runner.Spawn(playerBase, new Vector3(2.0f, 10.0f, 5.0f), Quaternion.identity, inputAuthority: playerRef);
            if(!spawnPlayer.TryGetComponent<Player>(out var player))
            {
                // Playerコンポーネントが見つからないのでエラー
                Debug.LogError("not found player component!!!");
                return;
            }
            cinemachineCamera.Follow = player.transform;


            runner.SetPlayerObject(playerRef, spawnPlayer);

            foreach(Transform t in fieldRoot)
            {
                if(t.TryGetComponent<IItem>(out var item))
                {
                    // マスタークライアントだけアイテムをSpawnする
                    if(runner.IsSharedModeMasterClient)
                    {
                        ItemManager.Instance.SpawnItem(item);
                    }
                    Destroy(t.gameObject);
                }
            }
        }
    }

    private void OnPlayerTriggerEnter(Player player, Collider otherCollider)
    {
        if(NetworkManager.Instance.NetworkRunner.IsSharedModeMasterClient)
        {
            if(otherCollider.TryGetComponent<IItem>(out var item))
            {
                Debug.Log($"[collect item]network player id:{player.NetworkPlayerRef.PlayerId} , item type:{item.GetType()} , item value{item.GetValue()}");

                NetworkManager.Instance.Despawn(item);
            }
        }
    }

    private async void Start()
    {
        PlayerManager.Instance.OnAddPlayerEvent += OnAddPlayer;

        NetworkManager.Instance.OnJoinedPlayerEvent += OnjoinedPlayer;
        NetworkManager.Instance.OnGameStartEvent += OnGameStart;
        await NetworkManager.Instance.GameStart();
    }
}

}
