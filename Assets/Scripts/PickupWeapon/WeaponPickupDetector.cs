using UnityEngine;

public class WeaponPickupDetector : MonoBehaviour
{
    private WeaponPickup currentPickup;
    public PlayerWeaponController weaponController;

    void Update()
    {
        if (currentPickup != null && Input.GetKeyDown(KeyCode.F))
        {
            weaponController.EquipWeapon(currentPickup.weaponData);
            Destroy(currentPickup.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out WeaponPickup pickup))
        {
            currentPickup = pickup;
            Debug.Log("Press F to pick up " + pickup.weaponData.weaponName);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out WeaponPickup pickup))
        {
            if (pickup == currentPickup)
                currentPickup = null;
        }
    }
}
