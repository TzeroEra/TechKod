using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Texture2D crosshairTexture;
    public float crosshairSize = 25f;

    void Start()
    {
        Cursor.visible = false;
    }

    void OnGUI()
    {
        Vector3 mousePos = Input.mousePosition;

        Rect crosshairRect = new Rect(mousePos.x - (crosshairSize / 2), Screen.height - mousePos.y - (crosshairSize / 2), crosshairSize, crosshairSize);

        GUI.DrawTexture(crosshairRect, crosshairTexture);
    }

    void OnDestroy()
    {
        Cursor.visible = true;
    }
}