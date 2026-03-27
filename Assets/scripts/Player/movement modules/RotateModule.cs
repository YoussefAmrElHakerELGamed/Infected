using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "RotateModule", menuName = "Scriptable Objects/RotateModule")]
public class RotateModule : PlayerMovementModule
{
    [SerializeField] private float RotatingSpeed;
    public override (Vector2 force, float rotation) ApplyModule(float angle, Transform transform, NewInputSystem inputSystem)
    {
        if (inputSystem.Player.Move.IsInProgress())
        {
            Vector2 m_pointToRotateTo = Camera.main.ScreenToWorldPoint(inputSystem.Player.Move.ReadValue<Vector2>());
            float m_targetAngle = Vector2.SignedAngle(Vector2.up, m_pointToRotateTo - (Vector2)transform.position);
            return (Vector2.zero, Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, m_targetAngle), RotatingSpeed).eulerAngles.z);
        }
        return (Vector2.zero, float.MaxValue);
    }
}
