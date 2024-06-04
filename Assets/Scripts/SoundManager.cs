using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    [SerializeField] private AudioSource shootingChanel;
    [SerializeField] private AudioClip M249shootingClip;
    [SerializeField] private AudioClip ReloadClip;

    [SerializeField] private AudioClip M1911shootingClip;


    [SerializeField] public AudioSource emptyClip;

    //Throwable
    [SerializeField] private AudioSource throwableChanel;
    [SerializeField] AudioClip grenadeClip;

    [SerializeField] AudioClip smokeGrenadeClip;

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

    public void PlayShootingSound(Weapon.WeaponModel weapon)
    {
        switch (weapon)
        {
            case Weapon.WeaponModel.M249:
                shootingChanel.PlayOneShot(M249shootingClip);
                break;

            case Weapon.WeaponModel.M1911:
                shootingChanel.PlayOneShot(M1911shootingClip);
                break;
        }
    }

    public void PlayReloadSound(Weapon.WeaponModel weapon)
    {
        switch (weapon)
        {
            case Weapon.WeaponModel.M249:
                shootingChanel.PlayOneShot(ReloadClip);
                break;

            case Weapon.WeaponModel.M1911:
                shootingChanel.PlayOneShot(ReloadClip);
                break;
        }
    }

    public void PlayThrowableSound(Throwable.ThrowableType throwable)
    {
        switch (throwable)
        {
            case Throwable.ThrowableType.Grenade:
                throwableChanel.PlayOneShot(grenadeClip);
                break;

            case Throwable.ThrowableType.Smoke_Grenade:
                throwableChanel.PlayOneShot(smokeGrenadeClip);
                break;
        }
    }
}
