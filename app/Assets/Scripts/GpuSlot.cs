using UnityEngine;

public class GpuSlot : MonoBehaviour
{
    public GpuSelectionUI selectionUI; // ������ �� ������ � UI

    void OnMouseDown()
    {
        if (selectionUI != null)
        {
            selectionUI.OpenSelection(gameObject); // ��������� ���� � ������� ���� ����
        }
        else
        {
            Debug.LogWarning("GpuSelectionUI �� ��������� � GpuSlot");
        }
    }
}
