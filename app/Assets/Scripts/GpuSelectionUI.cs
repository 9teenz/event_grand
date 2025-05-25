using UnityEngine;
using UnityEngine.UI;

public class GpuSelectionUI : MonoBehaviour
{
    public GameObject panel;
    public Button installGpuButton;
    public GameObject gpuPrefab;

    private GameObject currentSlot;

    void Start()
    {
        panel.SetActive(false);
        installGpuButton.onClick.AddListener(InstallGPU);
    }

    public void OpenSelection(GameObject slot)
    {
        currentSlot = slot;
        panel.SetActive(true);
    }

    void InstallGPU()
    {
        if (currentSlot == null || gpuPrefab == null)
            return;

        Vector3 pos = currentSlot.transform.position;
        Quaternion rot = currentSlot.transform.rotation;

        Destroy(currentSlot); // удаляем фантомный слот
        GameObject gpu = Instantiate(gpuPrefab, pos, rot);
        gpu.AddComponent<GpuMiner>(); // добавляем добычу

        panel.SetActive(false);
    }
}
