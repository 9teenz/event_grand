using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    [Header("������� ������")]
    public int Coins = 1000;

    [Header("������� ����")]
    public GameObject housePrefab;         // ������ ����
    public Transform houseSpawnPoint;      // ����� ������ ����

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool SpendCoins(int amount)
    {
        if (Coins < amount) return false;
        Coins -= amount;
        return true;
    }

    public void AddCoins(int amount)
    {
        Coins += amount;
    }

    /// <summary>
    /// ���������� �� HousePurchasePoint
    /// </summary>
    public void BuyHouse()
    {
        if (housePrefab == null || houseSpawnPoint == null) return;
        Instantiate(housePrefab, houseSpawnPoint.position, houseSpawnPoint.rotation);
    }
}
