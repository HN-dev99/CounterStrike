
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class IconManger : MonoBehaviour
{

    public static IconManger Instance { get; set; }

    public static Sprite M1911_Weapon;
    public static Sprite M249_Weapon;
    public static Sprite M1911_Ammo;
    public static Sprite M249_Ammo;

    public static Sprite grenade;
    public static Sprite smoke;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        M1911_Weapon = Resources.Load<GameObject>("M1911_Weapon").GetComponent<SpriteRenderer>().sprite;
        M249_Weapon = Resources.Load<GameObject>("M249_Weapon").GetComponent<SpriteRenderer>().sprite;
        M1911_Ammo = Resources.Load<GameObject>("M1911_Ammo").GetComponent<SpriteRenderer>().sprite;
        M249_Ammo = Resources.Load<GameObject>("Rfile_Ammo").GetComponent<SpriteRenderer>().sprite;

        grenade = Resources.Load<GameObject>("Grenade").GetComponent<SpriteRenderer>().sprite;
        smoke = Resources.Load<GameObject>("Smoke").GetComponent<SpriteRenderer>().sprite;
    }
}
