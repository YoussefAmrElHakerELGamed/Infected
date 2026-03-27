using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static Transform PublicPlayerTransform { get; private set; }

    [SerializeField] private PlayerMovementModule[] playerMovementModules;
    private Rigidbody2D _rb;
    private Transform _t;
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
        _t = transform;
        _rb = GetComponent<Rigidbody2D>();
        PublicPlayerTransform = _t;
    }

    void FixedUpdate()
    {
        for (int moduleIdx = 0; moduleIdx < playerMovementModules.Length; moduleIdx++)
        {
            (Vector2 forceToApply, float TargetAngle) = playerMovementModules[moduleIdx].ApplyModule(_rb.rotation, _t, _inputActions);
            _rb.AddForce(forceToApply, ForceMode2D.Force);

            if (TargetAngle == float.MaxValue)
                continue;

            _rb.SetRotation(TargetAngle);
        }
    }
}
