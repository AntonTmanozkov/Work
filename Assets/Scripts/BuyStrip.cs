using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyStrip : MonoBehaviour
{
    public Text BuyStripText;
    public GameObject ItemStrip;
    public Transform parentItemStrip;
    private GameManager gameManager;
    private int PriseMoney;
    private byte numName;
 
    void Start()
    {
        PriseMoney = PlayerPrefs.GetInt("PriseMoney", 500);
        gameManager = GameObject.Find("EventSystem").GetComponent<GameManager>();
        PriseMoney = 500;
    }

    public void BuyStrips() 
    {
        if (gameManager.Money_ >= PriseMoney)
        {
            CreateStrips();
            gameManager.Money_ = -PriseMoney;
        }
    
        SaveDate();
        UpdateTexts();
    }

    private void SaveDate() 
    {
        PlayerPrefs.SetInt("PriseMoney", PriseMoney);
    }

    private void UpdateTexts()
    {
        BuyStripText.text = $"{PriseMoney}";
    }

    public void CreateStrips() {
        GameObject newObject = Instantiate(ItemStrip, parentItemStrip);
        newObject.transform.SetAsFirstSibling();
        numName += 1;
        newObject.name += $"{numName}";
    }
}
