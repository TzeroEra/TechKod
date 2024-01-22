using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ScoreInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IScoreManager>().To<ScoreManager>().FromNewComponentOnNewGameObject().AsSingle();
    }
}