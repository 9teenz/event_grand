using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    [Header("Текущий баланс")]
    public int Coins = 1000;

    [Header("Покупка дома")]
    public GameObject housePrefab;         // префаб дома
    public Transform houseSpawnPoint;      // точка спавна дома

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
    /// Вызывается из HousePurchasePoint
    /// </summary>
    public void BuyHouse()
    {
        if (housePrefab == null || houseSpawnPoint == null) return;
        Instantiate(housePrefab, houseSpawnPoint.position, houseSpawnPoint.rotation);
    }
}
