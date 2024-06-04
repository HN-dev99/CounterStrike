using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GobalReferences : MonoBehaviour
{
    public static GobalReferences Instance { get; set; }

    public GameObject bulletEffectPrefab;

    public GameObject grenadeExplosionEffect;
    public GameObject smokeGrenadeEffect;



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
}
