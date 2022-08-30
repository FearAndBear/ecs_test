using InputSystem.Signals;
using Leopotam.EcsLite;
using UnityEngine;

namespace CharacterController.Systems
{
    public class CharacterMoveControllerSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var clickSignals = world.Filter<MouseClickSignalComponent>().End();
            var pool = world.GetPool<MouseClickSignalComponent>();

            foreach (var clickSignal in clickSignals)
            {
                Debug.Log($"Click signal: {pool.Get(clickSignal).MousePosition}");
            }
        }
    }
}