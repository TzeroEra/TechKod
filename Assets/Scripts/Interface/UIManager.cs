using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI activeWeaponText;

    public void UpdateActiveWeaponText(IWeapon currentWeapon)
    {
        if (activeWeaponText != null)
        {
            activeWeaponText.text = "Активна зброя: " + currentWeapon.GetType().Name;
        }
    }
}