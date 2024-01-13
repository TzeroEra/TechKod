using UnityEngine;
using UnityEngine.UI;
using TMPro;

public interface IWeapon
{
    void Shoot(Vector3 targetPosition);
}

public class MouseInput : MonoBehaviour
{
    private IWeapon currentWeapon;

    [SerializeField] private Shotgun shotgun;
    [SerializeField] private LongRangeGun longRangeGun;

    private UIManager uiManager;

    private float shotgunReloadTime = 1.5f; 
    private float longRangeGunReloadTime = 0.5f; 

    private float shotgunTimeSinceLastShot = 0f; 
    private float longRangeGunTimeSinceLastShot = 0f; 

    private void Start()
    {
        currentWeapon = shotgun;
        uiManager = FindObjectOfType<UIManager>(); 
        UpdateActiveWeaponText();
    }

    void Update()
    {
        UpdateReloadTimers();

        if (Input.GetMouseButtonDown(0) && CanShoot())
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentWeapon.Shoot(mousePosition);
            ResetTimer();
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
    }

    private void SwitchToShotgun()
    {
        currentWeapon = shotgun;
    }

    private void SwitchToLongRangeGun()
    {
        currentWeapon = longRangeGun;
    }

    private void SwitchToNextWeapon()
    {
        if (currentWeapon == shotgun)
        {
            currentWeapon = longRangeGun;
        }
        else if (currentWeapon == longRangeGun)
        {
            currentWeapon = shotgun;
        }

        UpdateActiveWeaponText();
    }

    private bool CanShoot()
    {
        if (currentWeapon == shotgun)
        {
            return shotgunTimeSinceLastShot >= shotgunReloadTime;
        }
        else if (currentWeapon == longRangeGun)
        {
            return longRangeGunTimeSinceLastShot >= longRangeGunReloadTime;
        }

        return false;
    }

    private void ResetTimer()
    {
        if (currentWeapon == shotgun)
        {
            shotgunTimeSinceLastShot = 0f;
        }
        else if (currentWeapon == longRangeGun)
        {
            longRangeGunTimeSinceLastShot = 0f;
        }
    }

    private void UpdateReloadTimers()
    {
        shotgunTimeSinceLastShot += Time.deltaTime;
        longRangeGunTimeSinceLastShot += Time.deltaTime;
    }

    private void UpdateActiveWeaponText()
    {
        if (uiManager != null)
        {
            uiManager.UpdateActiveWeaponText(currentWeapon);
        }
    }
}