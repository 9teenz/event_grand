using UnityEngine;

public class GpuSlot : MonoBehaviour
{
    public GpuSelectionUI selectionUI; // —сылка на объект с UI

    void OnMouseDown()
    {
        if (selectionUI != null)
        {
            selectionUI.OpenSelection(gameObject); // ќткрываем меню и передаЄм этот слот
        }
        else
        {
            Debug.LogWarning("GpuSelectionUI не подключен к GpuSlot");
        }
    }
}
