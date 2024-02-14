using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
using System.Collections;
using System.Collections.Generic;

public interface IWeapon
{
    void Shoot(Vector3 targetPosition);
    float ReloadTime { get; }

    void UpdateReloadTimers();

    void SetFirePoint(Transform tr);
}

public class MouseInput : MonoBehaviour
{
    [Inject]
    private WeaponFactory weaponFactory;

    private IWeapon currentWeapon;

    private UIManager uiManager;

    private float LastTimeShot = 0f;

    private List<IWeapon> weapons = new List<IWeapon> ();

    public List<string> weaponsName = new List<string> {"Shotgun", "LongRangeGun", "LaserGun" };

    private int index = 0;

    private void Start()
    {
        foreach (var weap in weaponsName)
        {
            var weapon = weaponFactory.Create (weap);
            weapon.SetFirePoint(transform);
            this.weapons.Add (weapon);
        }

        currentWeapon = weapons[index];
        uiManager = FindObjectOfType<UIManager>(); 
        UpdateActiveWeaponText();
    }

    void Update()
    {
        currentWeapon.UpdateReloadTimers();

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentWeapon.Shoot(mousePosition);
        }

        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollWheelInput > 0)
        {
            SwitchToNextWeapon();
        }
        else if (scrollWheelInput < 0)
        {
            SwitchToNextWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchToGun(0);
            UpdateActiveWeaponText();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchToGun(1);
            UpdateActiveWeaponText();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchToGun(2);
            UpdateActiveWeaponText();
        }
    }

    private void SwitchToGun(int Index)
    {
        index = Index;
        currentWeapon = weapons[Index];
    }


    private void SwitchToNextWeapon()
    {
        index++;
        index %= weapons.Count;
        SwitchToGun(index);
        UpdateActiveWeaponText();
    }

    private void UpdateActiveWeaponText()
    {
        if (uiManager != null)
        {
            uiManager.UpdateActiveWeaponText(currentWeapon);
        }
    }
}