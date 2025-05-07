using Fusion;
using UnityEngine;

/// <summary>
/// Playerの同期を処理するclass
/// 主に移動
/// </summary>
public class PlayerNetwork: NetworkBehaviour
{
    [SerializeField]
    private Rigidbody rigidbody = null;

    private static readonly float Speed = 5.0f;

    private Transform cameraTransform = null;

    public override void Spawned()
    {
        cameraTransform = Camera.main.transform;

    }

    public override void FixedUpdateNetwork()
    {
        if(!HasInputAuthority)
        {
            return;
        }

        if(GetInput(out NetworkInputData inputData))
        {
            var cameraForward = cameraTransform.forward;
            var cameraRight = cameraTransform.right;

            cameraForward.y = 0.0f;
            cameraRight.y = 0.0f;

            cameraForward.Normalize();
            cameraRight.Normalize();

            var move = (inputData.move.x * cameraRight + inputData.move.y * cameraForward) * Speed;

            rigidbody.MovePosition(rigidbody.position + (move * Runner.DeltaTime));

            if(move.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(move);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Runner.DeltaTime * 10f);
            }
        }
    }
}

