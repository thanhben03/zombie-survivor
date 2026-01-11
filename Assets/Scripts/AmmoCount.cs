using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class AmmoCount : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI magText;


    public static AmmoCount ammoCount;

    private void Awake()
    {
        ammoCount = this;
    }

    public void UpdateAmmoText(int amount)
    {
        ammoText.text = "Ammo: " + amount;
    }

    public void UpdateMagText (int amount)
    {
        magText.text = "Mag: " + amount;
    }
}
