using UnityEngine;

public class CoinsChanger : MonoBehaviour
{
    public GameObject panel;

    public void Toggle()
    {
        if (panel != null)
        {
            panel.SetActive(!panel.activeSelf);
        }
    }
}
