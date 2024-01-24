using UnityEngine;
using Zenject;

public class HealingZoneFactory : IHealingZoneFactory
{
    private readonly GameObject _healingZonePrefab;
    private readonly DiContainer _diContainer;

    public HealingZoneFactory(GameObject healingZonePrefab, DiContainer diContainer)
    {
        _diContainer = diContainer;
        _healingZonePrefab = healingZonePrefab;
    }

    public GameObject CreateHealingZone(Vector3 position)
    {
        GameObject newHealingZone = _diContainer.InstantiatePrefab(_healingZonePrefab);

        newHealingZone.transform.position = position;

        return newHealingZone;
    }
}