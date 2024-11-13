using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weaponHolder;
    private Weapon weapon;

    private void Awake()
    {
        weapon = weaponHolder;
    }

    private void Start()
    {
        if (weapon != null)
        {
            weapon.gameObject.SetActive(false);
            TurnVisual(false, weapon);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player.Instance.hasWeapon = true;
            Debug.Log("Objek Player Memasuki trigger");
            if (weaponHolder.name == "Weapon3")
            {
                weapon.transform.position = new Vector3(other.transform.position.x,
                                                      other.transform.position.y - 0.2f,
                                                      other.transform.position.z);
            }
            else
            {
                weapon.transform.position = other.transform.position;
            }
            weapon.transform.SetParent(other.transform);
            weapon.gameObject.SetActive(true);
            TurnVisual(true, weapon);
            Destroy(gameObject);
        }
    }


    private void TurnVisual(bool on)
    {
        foreach (Component component in GetComponents<Component>())
        {
            component.gameObject.SetActive(on);
        }
    }

    private void TurnVisual(bool on, Weapon weapon)
    {
        foreach (Component component in weapon.GetComponents<Component>())
        {
            component.gameObject.SetActive(on);
        }
    }

}
