using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ProductCard : MonoBehaviour
{
    public int price;
    public TextMeshProUGUI priceText;


    public Button buyButton;

    private ShopManager shop;

    void Start()
    {
        priceText.text = "Стоимость: " + price + "$";
        shop = FindObjectOfType<ShopManager>();

        buyButton.onClick.AddListener(() =>
        {
            shop.TryBuy(price, buyButton);
        });
    }
}
