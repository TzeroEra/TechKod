using UnityEngine;
using Zenject;

public interface IHealingZoneFactory
{
    GameObject CreateHealingZone(Vector3 position);
}
