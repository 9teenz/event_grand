using UnityEngine;

public class UIPanelManager : MonoBehaviour
{
    public GameObject[] panels;

    private GameObject currentPanel;

    public void ShowPanel(GameObject panelToShow)
    {
        if (currentPanel != null && currentPanel != panelToShow)
        {
            currentPanel.SetActive(false);
        }

        if (panelToShow != null)
        {
            bool isActive = panelToShow.activeSelf;

            panelToShow.SetActive(!isActive);

            currentPanel = panelToShow.activeSelf ? panelToShow : null;
        }
    }
}
