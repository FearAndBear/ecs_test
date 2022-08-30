using System;
using Leopotam.EcsLite;
using Signals.Systems;
using UnityEngine;

namespace Ecs
{
    public class EcsWorldInstaller : MonoBehaviour
    {
        private EcsWorld _world;
        private IEcsSystems _systems;

        private void Awake()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            RegisterSystems();
            
            _systems.Init();
        }

        private void Update()
        {
            _systems.Run();
        }

        private void OnDestroy()
        {
            _systems?.Destroy ();
            _world?.Destroy ();
        }

        private void RegisterSystems()
        {
            // First systems.
            _systems.Add(new SignalSpawnerSystem());

            // Second systems.
            
            // Last systems.
            _systems.Add(new SignalDestroyerSystem());
        }
    }
}