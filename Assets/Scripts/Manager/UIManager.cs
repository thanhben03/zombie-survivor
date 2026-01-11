using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Weapon UI")]
    [SerializeField] private GameObject rifleUI;

    [Header("Player UI")]
    [SerializeField] private GameObject playerUI;

    void Awake()
    {
        Instance = this;
    }

    // ===== PLAYER UI =====
    public void ShowPlayerUI(bool show)
    {
        playerUI.SetActive(show);
    }

    // ===== WEAPON UI =====
    public void ShowWeaponUI(string weaponName)
    {
        rifleUI.SetActive(false);

        switch (weaponName)
        {
            case "Rifle":
                rifleUI.SetActive(true);
                break;
            //case "Pistol":
            //    pistolUI.SetActive(true);
            //    break;
        }
    }

    public void HideAllWeaponUI()
    {
        rifleUI.SetActive(false);
    }
}
