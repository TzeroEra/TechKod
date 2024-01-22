using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI activeWeaponText;

    public void UpdateActiveWeaponText(IWeapon currentWeapon)
    {
        if (activeWeaponText != null)
        {
            if (currentWeapon != null)
            {
                if (currentWeapon.GetType() == typeof(Shotgun))
                {
                    activeWeaponText.text = "������� �����: ��������";
                }
                else if (currentWeapon.GetType() == typeof(LongRangeGun))
                {
                    activeWeaponText.text = "������� �����: ��������";
                }
                else if (currentWeapon.GetType() == typeof(LaserGun))
                {
                    activeWeaponText.text = "������� �����: �����";
                }
            }
        }
    }
}