using R3;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public AppInputActions InputActions => inputActions;

    private AppInputActions inputActions = null;

    public event Action<Vector2> OnMoveEvent = null;

    public void ResetEvent()
    {
        OnMoveEvent = null;
    }

    private void Awake()
    {
        if(Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        inputActions = new AppInputActions();

        Observable.EveryUpdate().Select(_ => inputActions.Player.Move.ReadValue<Vector2>())
                                .Where(dir => dir != Vector2.zero)
                                .Subscribe(dir => {
                                    OnMoveEvent?.Invoke(dir);
//                                    Debug.Log("1:" + dir);
                                })
                                .AddTo(this);

        inputActions.Enable();
    }

    private void OnDestroy()
    {
        ResetEvent();
    }

    public static InputManager Instance { get; private set; } = null;
}
