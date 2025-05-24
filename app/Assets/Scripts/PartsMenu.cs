using UnityEngine;

public class PartsMenu : MonoBehaviour
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
