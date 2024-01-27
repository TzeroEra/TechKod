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
}

public class MouseInput : MonoBehaviour
{
    [Inject]
    private WeaponFactory weaponFactory;

    private IWeapon currentWeapon;

    [SerializeField] private Shotgun shotgun;
    [SerializeField] private LongRangeGun longRangeGun;
    [SerializeField] private LaserGun laserGun;

    private UIManager uiManager;

    private float LastTimeShot = 0f; 

    private void Start()
    {
        currentWeapon = shotgun;
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
            SwitchToShotgun();
            UpdateActiveWeaponText();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchToLongRangeGun();
            UpdateActiveWeaponText();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchToLaserGun();
            UpdateActiveWeaponText();
        }
    }

    private void SwitchToShotgun()
    {
        currentWeapon = shotgun;
    }

    private void SwitchToLongRangeGun()
    {
        currentWeapon = longRangeGun;
    }

    private void SwitchToLaserGun()
    {
        currentWeapon = laserGun;
    }

    private void SwitchToNextWeapon()
    {
        if (currentWeapon == shotgun)
        {
            currentWeapon = longRangeGun;
        }
        else if (currentWeapon == longRangeGun)
        {
            currentWeapon = laserGun;
        }
        else if (currentWeapon == laserGun)
        {
            currentWeapon = shotgun;
        }

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