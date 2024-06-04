using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{

    public static InteractionManager Instance { get; set; }
    public Weapon hoveredWeapon;
    public AmmoBox hoveredAmmoBox;
    public Throwable hoveredThrowable;
    [SerializeField] private Transform player;
    public float distanceToPickupItem = 5f;
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
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectWeHit = hit.transform.gameObject;

            if (objectWeHit.GetComponent<Weapon>() && objectWeHit.GetComponent<Weapon>().isActiveWeapon == false)
            {
                hoveredWeapon = objectWeHit.GetComponent<Weapon>();
                hoveredWeapon.GetComponent<Outline>().enabled = true;

                float distancePlayerWithPickupItem = Vector3.Distance(player.position, hoveredWeapon.transform.position);
                if (Input.GetKeyDown(KeyCode.F) && distancePlayerWithPickupItem < distanceToPickupItem)
                {
                    WeaponManager.Instance.PickupWeapon(objectWeHit);
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                }
            }
            else
            {
                if (hoveredWeapon)
                {
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                }
            }

            //Ammo Box

            if (objectWeHit.GetComponent<AmmoBox>())
            {
                hoveredAmmoBox = objectWeHit.GetComponent<AmmoBox>();
                hoveredAmmoBox.GetComponent<Outline>().enabled = true;

                float distancePlayerWithPickupItem = Vector3.Distance(player.position, hoveredAmmoBox.transform.position);
                if (Input.GetKeyDown(KeyCode.F) && distancePlayerWithPickupItem < distanceToPickupItem)
                {
                    WeaponManager.Instance.PickupAmmo(hoveredAmmoBox);
                    Destroy(objectWeHit.gameObject);
                }
            }
            else
            {
                if (hoveredAmmoBox)
                {
                    hoveredAmmoBox.GetComponent<Outline>().enabled = false;
                }
            }


            //Throwable

            if (objectWeHit.GetComponent<Throwable>())
            {
                hoveredThrowable = objectWeHit.GetComponent<Throwable>();
                hoveredThrowable.GetComponent<Outline>().enabled = true;

                float distancePlayerWithPickupItem = Vector3.Distance(player.position, hoveredThrowable.transform.position);
                if (Input.GetKeyDown(KeyCode.F) && distancePlayerWithPickupItem < distanceToPickupItem)
                {
                    WeaponManager.Instance.PickupThrowable(hoveredThrowable);
                }
            }
            else
            {
                if (hoveredThrowable)
                {
                    hoveredThrowable.GetComponent<Outline>().enabled = false;
                }
            }
        }
    }
}
