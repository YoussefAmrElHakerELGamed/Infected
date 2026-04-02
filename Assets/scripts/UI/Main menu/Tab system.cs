using UnityEngine;
using UnityEngine.UI;

public class TabSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] tabs;
    [SerializeField] private GameObject[] tabsContent;

    [SerializeField] private Color ActiveColor, InactiveColor;

    void Start()
    {
        OnTabClick(0);
    }

    public void OnTabClick(int idx)
    {
        for (int i = 0; i < tabsContent.Length; i++)
        {
            tabsContent[i].SetActive(i == idx);
            tabs[i].GetComponent<Image>().color = i == idx ? ActiveColor : InactiveColor;
        }
    }

}
