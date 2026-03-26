using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovementModule[] playerMovementModules;
    private Rigidbody2D _rb;
    #region inputs
    private NewInputSystem _inputActions;
    void Awake()
    {
        _inputActions = new();
    }

    void OnEnable()
    {
        _inputActions.Enable();
    }

    void OnDisable()
    {
        _inputActions.Disable();
    }
    #endregion

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        for (int moduleIdx = 0; moduleIdx < playerMovementModules.Length; moduleIdx++)
        {
            (Vector2 forceToApply, float TargetAngle) = playerMovementModules[moduleIdx].ApplyModule(_rb.rotation, _rb.linearVelocity, _inputActions);
            _rb.AddForce(forceToApply, ForceMode2D.Force);
            _rb.SetRotation(TargetAngle);
        }
    }
}
