using UnityEngine;

public class General_AI : MonoBehaviour
{
    [SerializeField] protected float MovementSpeed, RotationSpeed;
    private Rigidbody2D _rb;
    private Transform _t;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _t = transform;

        AddRandomRotation();
    }
    void FixedUpdate()
    {
        ExecuteMovement();
    }

    protected virtual void AddRandomRotation()
    {
        _rb.AddTorque(Mathf.Sign(Random.value - 0.5f) * RotationSpeed, ForceMode2D.Impulse);
    }

    protected virtual void ExecuteMovement()
    {
        _rb.AddForce(MovementSpeed * (PlayerMovement.PublicPlayerTransform.position - _t.position), ForceMode2D.Force);
    }
}
