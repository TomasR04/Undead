using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public TMP_Text hpUI;
    public TMP_Text hungerUI;
    public TMP_Text thirstUI;

    public double strenght = 10;
    public double hunger = 100;
    public double thirst = 100;
    public double hp = 100;

    public void Die()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        NaturalyChangeStats();
        SetUI();
    }

    private void SetUI()
    {
        hpUI.text = "HP: " +Math.Floor( hp);
        hungerUI.text = "Hlad: " + Math.Floor(hunger);
        thirstUI.text = "Žízeò: " + Math.Floor(thirst);
    }
    private void NaturalyChangeStats()
    {
        hunger -= 1 * Time.deltaTime;
        thirst -= 1 * Time.deltaTime;
    }
}
