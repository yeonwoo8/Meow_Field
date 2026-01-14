using UnityEngine;
using UnityEngine.UI;

public class NExchangeShop : MonoBehaviour
{
    public UIcoinController coinController;
    public NInventoryController NinventoryController;
    public Text messageText;
    public Text CoinText;
    public GameObject ExchangePanel;

    private bool isExchangeOpen = false;

    public void BUY(Item item)
    {
        var itemData = GameManager.instance.itemData;

        // 코인 개수 조회
        int coinCount = GameManager.coin;

        int cost = 0;
        if (item.itemName == "sugar" || item.itemName == "salt")
        {
            cost = 1;
        }
        else if (item.itemName == "soy" || item.itemName == "pepper")
        {
            cost = 2;
        }
        else
        {
            ShowMessage("알 수 없는 아이템입니다.");
            return;
        }

        if (coinCount < cost)
        {
            ShowMessage(coinCount+"코인이 부족합니다.");
            return;
        }

        // 구매처리
        coinCount -= cost;
        GameManager.coin = coinCount;

        Debug.Log(item.itemName + "를 구매하였습니다.");

        CoinText.text = coinCount.ToString();
        NinventoryController.AddItem(item);
        NinventoryController.UpdateInventoryUI();
    }

    void ShowMessage(string message)
    {
        messageText.gameObject.SetActive(true);
        messageText.text = message;
        Invoke("HideMessage", 2f);
    }

    public void ToggleExchange()
    {
        isExchangeOpen = !isExchangeOpen;
        ExchangePanel.SetActive(isExchangeOpen);
    }

    void HideMessage()
    {
        messageText.gameObject.SetActive(false);
    }
}
