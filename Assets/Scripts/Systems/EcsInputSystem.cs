using Components;
using Leopotam.EcsLite;
using Signals;
using UnityEngine;

namespace Systems
{
    public class EcsInputSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var signalRequestPool = world.GetPool<SignalRequestComponent>();

            if (Input.GetMouseButtonDown(0))
            {
                var entityID = world.NewEntity();
                
                ref var signalRequest = ref signalRequestPool.Add(entityID);
                var signal = new MouseClickSignal {Position = Input.mousePosition};

                signalRequest.Signal = signal;
            }
        }
    }
}