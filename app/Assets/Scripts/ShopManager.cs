using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopManager : MonoBehaviour
{
    public int playerBalance = 1000;
    public TextMeshProUGUI balanceText;
    public NotificationLog notificationLog;

    void Start()
    {
        UpdateBalanceUI();
    }

    public void TryBuy(int price, Button buyButton)
    {
        if (playerBalance >= price)
        {
            playerBalance -= price;
            UpdateBalanceUI();

            notificationLog.ShowAddedToInventory();
            Debug.Log("��������� � ��������� 1�");
        }
        else
        {
            Debug.Log("������������ �������!");
            buyButton.interactable = false; // msg
            notificationLog.ShowInsufficientFunds();
        }
    }

    public bool TryBuyCoin(int coinPrice)
    {
        if (playerBalance >= coinPrice)
        {
            playerBalance -= coinPrice;
            UpdateBalanceUI();
            return true;
        }
        else
        {
            Debug.Log("������������ �������");
            return false;
        }
    }

    public void SellCoin(int sellPrice)
    {
        playerBalance += sellPrice;
        UpdateBalanceUI();
    }


    private void UpdateBalanceUI()
    {
        balanceText.text = "������: $" + playerBalance.ToString();
    }
}
