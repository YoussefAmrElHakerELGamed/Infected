using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerMovementModule", menuName = "Scriptable Objects/PlayerMovementModule")]
public class PlayerMovementModule : ScriptableObject
{
    public virtual (Vector2 force, float rotation) ApplyModule(float angle, Vector2 velocity, NewInputSystem inputSystem)
    {

        return (Vector2.zero, 0);
    }
}
