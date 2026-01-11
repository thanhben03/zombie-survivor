using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public Transform weaponHolder;
    public GameObject currentWeapon;
    private PlayerWeaponAnimation weaponAnimation;

    void Awake()
    {
        weaponAnimation = GetComponent<PlayerWeaponAnimation>();
    }

    public void EquipWeapon(WeaponData data)
    {
        if (currentWeapon != null)
            Destroy(currentWeapon);
        Debug.Log("okk");
        GameObject weaponObj = Instantiate(data.weaponPrefab, weaponHolder);

        weaponObj.transform.localPosition = data.holdOffsetPos;
        weaponObj.transform.localRotation = Quaternion.Euler(data.holdOffsetRot);

        currentWeapon = weaponObj;

        Weapon weapon = weaponObj.GetComponent<Weapon>();
        //GameObject player = GameManager.Instance.GetCurrentPlayer();
        //if (player != null)
        //{
        //    //player.GetComponent<Animator>().runtimeAnimatorController = player.GetComponent<PlayerWeaponAnimation>(
        //}
        weapon.Init(data);
        weaponAnimation.Apply(data.weaponType);
        ObjectiveManager.Instance.CompleteObjective(1);
    }
}
