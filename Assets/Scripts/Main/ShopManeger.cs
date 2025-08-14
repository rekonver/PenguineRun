using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Button BuyButton;
    public MainGameMode gameMode;
    public DancedCharecterMenu danceMenu;

    [Header("Arrays")]
    public List<string> PlayerNameArray = new List<string>();
    public List<int> PlayerCostArray = new List<int>();
    public List<Sprite> PlayerSpriteArray = new List<Sprite>();

    private int ChoosedPlayer = 0;

    [Header("Changed Element")]
    public TextMeshProUGUI PlayerNameText;
    public TextMeshProUGUI PlayerCostText;
    public TextMeshProUGUI PlayerMoneyText;
    public Image PlayerImage;

    [Header("Canvases")]
    public GameObject canvasShop;

    void Start()
    {
        for (int i = 0; i < PlayerCostArray.Count; i++)
        {
            if (!PlayerPrefs.HasKey("Skin_" + i))
            {
                int result = (PlayerCostArray[i] == 0) ? 0 : 1;
                PlayerPrefs.SetInt("Skin_" + i, result);
                PlayerPrefs.Save();
            }
        }
        ChoosedPlayer = PlayerPrefs.GetInt("CharecterInt", 0); // Значення за замовчуванням 0
        UpdateUi();
    }

    public void NextPlayerShow()
    {
        if (ChoosedPlayer < PlayerNameArray.Count - 1)
        {
            ChoosedPlayer++;
        }
        else
        {
            ChoosedPlayer = 0;
        }
        UpdateUi();
    }

    public void PreviousPlayerShow()
    {
        if (ChoosedPlayer > 0)
        {
            ChoosedPlayer--;
        }
        else
        {
            ChoosedPlayer = PlayerNameArray.Count - 1;
        }
        UpdateUi();
    }

    public void UpdateUi()
    {
        PlayerNameText.text = PlayerNameArray[ChoosedPlayer];

        PlayerMoneyText.text = "Money: " + PlayerPrefs.GetInt("Coins").ToString();
        PlayerImage.sprite = PlayerSpriteArray[ChoosedPlayer];
        bool HaveOrNot = IsSkinHave(ChoosedPlayer);
        if (HaveOrNot)
        {
            PlayerCostText.text = "Have";
            BuyButton.gameObject.SetActive(false);
        }
        else
        {
            PlayerCostText.text = PlayerCostArray[ChoosedPlayer].ToString();
            BuyButton.gameObject.SetActive(true);
        }
    }

    public void BuySkin()
    {
        int skinIndex = ChoosedPlayer;

        if (PlayerPrefs.GetInt("Coins") >= PlayerCostArray[skinIndex])
        {
            int totalCoins = PlayerPrefs.GetInt("Coins");
            PlayerPrefs.SetInt("Coins", totalCoins - PlayerCostArray[skinIndex]);

            PlayerPrefs.SetInt("Skin_" + skinIndex, 0);
            PlayerPrefs.Save();
            UpdateUi();
        }
    }
    public void BackButton()
    {
        bool HaveOrNot = IsSkinHave(ChoosedPlayer);
        if (HaveOrNot)
        {
            PlayerPrefs.SetInt("CharecterInt", ChoosedPlayer);
            danceMenu.ChooseCharacter();
        }
        gameMode.setActiveOne(0);
    }
    public bool IsSkinHave(int skinIndex)
    {
        return PlayerPrefs.GetInt("Skin_" + skinIndex) == 0;
    }
}
