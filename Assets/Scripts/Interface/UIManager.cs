using UnityEngine;
using TMPro;

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
                    activeWeaponText.text = "Активна зброя: Дробовик";
                }
                else if (currentWeapon.GetType() == typeof(LongRangeGun))
                {
                    activeWeaponText.text = "Активна зброя: Гвинтівка";
                }
            }
        }
    }
}