using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class HousePurchasePoint : MonoBehaviour
{
    [Header("Цена дома")]
    public int houseCost = 10000;

    [Header("UI")]
    public Text promptText;

    private bool playerInRange = false;
    private bool purchased = false;

    void Start()
    {
        promptText.text = $"E — купить дом за {houseCost} монет";
        promptText.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || purchased) return;
        playerInRange = true;
        promptText.gameObject.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerInRange = false;
        promptText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
            TryPurchase();
    }

    private void TryPurchase()
    {
        if (!CurrencyManager.Instance.SpendCoins(houseCost))
            return;

        purchased = true;
        promptText.text = "Дом куплен!";

        // вместо GameManager.Instance.BuyHouse()
        CurrencyManager.Instance.BuyHouse();

        Invoke(nameof(HidePrompt), 2f);
    }

    private void HidePrompt()
    {
        promptText.gameObject.SetActive(false);
    }
}
