
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public int ammoAmount = 40;


    public enum AmmoType
    {
        RifleAmmo,
        M1911Ammo,
    }
    public AmmoType ammoType;

}
