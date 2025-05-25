using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinBuy : MonoBehaviour
{
    public int price = 100;
    public int amount = 0;

    public TextMeshProUGUI priceText;
    public TextMeshProUGUI amountText;
    public Button buyButton;
    public Button sellButton;

    private ShopManager shop;

    void Start()
    {
        priceText.text = "Цена: $" + price;
        UpdateAmountText();

        shop = FindObjectOfType<ShopManager>();

        buyButton.onClick.AddListener(() =>
        {
            if (shop.TryBuyCoin(price))
            {
                amount++;
                UpdateAmountText();
                Debug.Log("Куплена 1 монета");
            }
            else
            {
                
            }
        });
        sellButton.onClick.AddListener(() =>
        {
            if (amount > 0)
            {
                amount--;
                UpdateAmountText();
                shop.SellCoin(price);
                Debug.Log("Продана 1 монета");
            }
            else
            {
                
            }
        });
    }

    private void UpdateAmountText()
    {
        if (amountText != null)
            amountText.text = "Монет: " + amount;
    }
}
