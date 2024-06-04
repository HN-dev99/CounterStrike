
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; set; }

    [Header("Ammo")]
    public TextMeshProUGUI magazineAmmoUI;
    public TextMeshProUGUI totalAmmoUI;
    public Image ammoTypeUI;

    [Header("Weapon")]
    public Image activeWeaponUI;
    public Image unActiveWeaponUI;

    [Header("Throwbles")]
    public Image lethalUI;
    public TextMeshProUGUI lethalAmountUI;

    public Image tacticalUI;
    public TextMeshProUGUI tacticalAmountUI;

    public Sprite emptySlot;

    public Image midPoint;

    public Sprite greySlot;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        Weapon activeWeapon = WeaponManager.Instance.weaponActiveSlot.GetComponentInChildren<Weapon>();
        Weapon unActiveWeapon = GetUnAcTiveWeaponSlot().GetComponentInChildren<Weapon>();

        if (activeWeapon)
        {
            magazineAmmoUI.text = $"{activeWeapon.bulletLefts / activeWeapon.bulletPerBurst}";
            totalAmmoUI.text = $"{WeaponManager.Instance.CheckAmmoLeftFor(activeWeapon.thisWeaponModel)}";

            Weapon.WeaponModel model = activeWeapon.thisWeaponModel;
            ammoTypeUI.sprite = GetAmmoSprite(model);

            activeWeaponUI.sprite = GetWeaponSprite(model);

            if (unActiveWeapon)
            {
                unActiveWeaponUI.sprite = GetWeaponSprite(unActiveWeapon.thisWeaponModel);
            }
        }
        else
        {
            magazineAmmoUI.text = "";
            totalAmmoUI.text = "";

            ammoTypeUI.sprite = emptySlot;

            activeWeaponUI.sprite = emptySlot;
            unActiveWeaponUI.sprite = emptySlot;

            if (WeaponManager.Instance.lethalCount <= 0)
            {
                lethalUI.sprite = greySlot;
            }

            if (WeaponManager.Instance.tacticalCount <= 0)
            {
                tacticalUI.sprite = greySlot;
            }
        }

    }

    private GameObject GetUnAcTiveWeaponSlot()
    {
        foreach (GameObject weaponSlot in WeaponManager.Instance.weaponSlots)
        {
            if (weaponSlot != WeaponManager.Instance.weaponActiveSlot)
            {
                return weaponSlot;
            }
        }
        return null;
    }

    private Sprite GetAmmoSprite(Weapon.WeaponModel model)
    {

        switch (model)
        {
            case Weapon.WeaponModel.M1911:
                return IconManger.M1911_Ammo;
            case Weapon.WeaponModel.M249:
                return IconManger.M249_Ammo;
            default:
                return null;
        }
    }


    private Sprite GetWeaponSprite(Weapon.WeaponModel model)
    {
        switch (model)
        {
            case Weapon.WeaponModel.M1911:
                return IconManger.M1911_Weapon;

            case Weapon.WeaponModel.M249:
                return IconManger.M249_Weapon;
            default:
                return null;
        }
    }

    public void UpdateThrowablesUI()
    {
        lethalAmountUI.text = $"{WeaponManager.Instance.lethalCount}";
        tacticalAmountUI.text = $"{WeaponManager.Instance.tacticalCount}";

        lethalUI.sprite = IconManger.grenade;
        tacticalUI.sprite = IconManger.smoke;

    }
}

