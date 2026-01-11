using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public PlayerScript[] characters;
    public int currentIndex = 0;

    void Start()
    {
        SwitchCharacter(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchCharacter(0);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchCharacter(1);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            SwitchCharacter(2);
    }

    public void SwitchCharacter(int index)
    {
        if (index < 0 || index >= characters.Length) return;

        characters[currentIndex].isActive = false;

        currentIndex = index;
        characters[currentIndex].isActive = true;
    }
}