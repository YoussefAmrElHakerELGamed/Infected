using UnityEngine;

public class Buildsystem : MonoBehaviour
{
    [SerializeField] private GameObject[] ObjectsToBuild;

    void Start()
    {
        GameEventBus.Instance.OnBuildObject += ActivateBuildSystem;
    }

    private void ActivateBuildSystem(BuildSystemEventMassage massage)
    {
        print(massage.GameObjectToBuild);
    }
}
