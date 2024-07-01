using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private GameManager gameManager;
    public Image barImage;
    public float fill;

    public Text BarText;
    public float BarNum;
    private float BarLimit;

    public Text ButtonUpgradeText;
    private int ButtonUpgradePrise;

    public Text LvlText;
    private int Lvl;

    void Start()
    {   
        BarLimit = PlayerPrefs.GetFloat(gameObject.name + "BarLimit", 1000f);
        Lvl = PlayerPrefs.GetInt(gameObject.name + "Lvl", 1);
        ButtonUpgradePrise = PlayerPrefs.GetInt(gameObject.name + "ButtonUpgradePrise", 50);
        fill = PlayerPrefs.GetFloat(gameObject.name + "fill", 0f);
        BarNum = PlayerPrefs.GetFloat(gameObject.name + "BarNum", 0.001f);

        SaveDate();
        UpdateTexts();

        gameManager = GameObject.Find("EventSystem").GetComponent<GameManager>();

        InvokeRepeating(nameof(PayIncome), 0f, 5f);
    }

    private void PayIncome()
    {
        if(Math.Round(fill * BarLimit) < BarLimit) 
        {
            fill += BarNum;
        } 
        else
        {
            fill = 1f;
        }

        barImage.fillAmount = fill;
        SaveDate();
        UpdateTexts();
    }

    public void SaveDate() 
    {
        PlayerPrefs.SetFloat(gameObject.name + "BarLimit", BarLimit);
        PlayerPrefs.SetInt(gameObject.name + "Lvl", Lvl);
        PlayerPrefs.SetInt(gameObject.name + "ButtonUpgradePrise", ButtonUpgradePrise);
        PlayerPrefs.SetFloat(gameObject.name + "fill", fill);
        PlayerPrefs.SetFloat(gameObject.name + "BarNum", BarNum);
    }

    private void UpdateTexts() 
    {
        BarText.text = $"{Math.Round(fill * BarLimit)}/{BarLimit}";
        ButtonUpgradeText.text = $"{ButtonUpgradePrise}";
        LvlText.text = $"LVL {Lvl}";
    }

    public void Upgrade() {
        if(gameManager.Money_ >= ButtonUpgradePrise) {
            ButtonUpgradePrise = Convert.ToInt32(Math.Round(ButtonUpgradePrise * 1.1f, MidpointRounding.AwayFromZero));
            BarLimit = Convert.ToInt32(Math.Round(BarLimit * 1.05f, MidpointRounding.AwayFromZero));
            BarNum += BarNum * 0.05f;
            Lvl += 1;

            gameManager.Money_ = -ButtonUpgradePrise;
        }
        
        SaveDate();
        UpdateTexts();
    }

    public void Collect() {
        gameManager.Money_ = Convert.ToInt32(Math.Round(fill * BarLimit, MidpointRounding.AwayFromZero)); 
        fill = 0f;
        
        SaveDate();
        UpdateTexts();
    }
}
