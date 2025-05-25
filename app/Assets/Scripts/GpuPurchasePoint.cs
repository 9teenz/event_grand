using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GpuPurchasePoint : MonoBehaviour
{
    [Header("Префаб видеокарты (без GpuMiner)")]
    public GameObject gpuPrefab;

    [Header("Точки, куда ставить видеокарты (массив слотов)")]
    public Transform[] spawnSlots;

    [Header("Цена базовая для 2-й, 3-й и т.д.")]
    public int baseCost = 1000;

    private int boughtCount = 0;
    private bool inTrigger = false;

    void Awake()
    {
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            inTrigger = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            inTrigger = false;
    }

    void Update()
    {
        if (!inTrigger) return;
        if (Input.GetKeyDown(KeyCode.E))
            TryBuyGpu();
    }

    void TryBuyGpu()
    {
        int cost = boughtCount == 0 ? 0 : baseCost * boughtCount;
        if (!CurrencyManager.Instance.SpendCoins(cost))
        {
            Debug.Log("Недостаточно монет для покупки видеокарты");
            return;
        }

        if (boughtCount >= spawnSlots.Length)
        {
            Debug.Log("Все слоты уже заняты");
            return;
        }
        Transform slot = spawnSlots[boughtCount];

        var gpu = Instantiate(gpuPrefab, slot.position, slot.rotation, slot);
        gpu.AddComponent<GpuMiner>();

        boughtCount++;
        Debug.Log($"Куплена видеокарта #{boughtCount}, потрачено {cost} монет");
    }
}
