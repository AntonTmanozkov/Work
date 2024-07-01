using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    private BuyStrip buyStrip;
    public Text MoneyText;
    private GameObject Content;
    private int BarStripSumm;
    private int Money;
    public int Money_
    {
        get 
        {
            return Money;
        }
        set
        {
            Money += value;
            SaveDate();
            UpdateTexts();
        }
        
    }
    void Start()
    {
        DateTime lastSaveTime = Utils.GetDateTime("LastSaveTime", DateTime.UtcNow);
        TimeSpan timePassed = DateTime.UtcNow - lastSaveTime;
        int secondPassed = (int)timePassed.TotalSeconds;
        secondPassed = Math.Clamp(secondPassed, 0, 7 * 24 * 60 * 60);

        ProgressBar[] progressBars = FindObjectsOfType<ProgressBar>();
        foreach (ProgressBar progressBar in progressBars)
        {
            progressBar.fill = secondPassed / 5 * progressBar.BarNum;
        }

        buyStrip = GameObject.Find("BuyStrip").GetComponent<BuyStrip>();

        Money = PlayerPrefs.GetInt("Money", 0);
        BarStripSumm = PlayerPrefs.GetInt("BarStripSumm", 0);

        UpdateTexts();
        CreateStrips();
        SaveDate();
    }

    private void SaveDate() 
    {
        Content = GameObject.Find("Content");

        PlayerPrefs.SetInt("Money", Money);
        PlayerPrefs.SetInt("BarStripSumm", Content.transform.childCount - 2);

        Utils.SetDatetime("LastSaveTime", DateTime.Now);
    }

    private void UpdateTexts() 
    {
        MoneyText.text = $"{Money}";
        Debug.Log($"Money: {Money}");
    }

    private void CreateStrips() {
        for (var i = 0; i < BarStripSumm; i++)
        {
            buyStrip.CreateStrips();
        }

    }

    

    
}
