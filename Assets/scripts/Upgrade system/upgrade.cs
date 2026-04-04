using UnityEngine;

[CreateAssetMenu(fileName = "upgrade", menuName = "Scriptable Objects/upgrade")]
public class upgrade : ScriptableObject
{
    public new string name;
    public Sprite upgradeSprite;
    [TextArea] public string upgradeDescription;
    public enum upgradeType
    {
        multiplicative, additive
    }
    public upgradeType type;
    public float PowerMultiplier, PowerAdder;
    public UpgradeLogic logic;
}
