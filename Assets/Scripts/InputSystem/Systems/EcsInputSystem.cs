using InputSystem.Signals;
using Leopotam.EcsLite;
using Signals.Components;
using UnityEngine;

namespace InputSystem.Systems
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
                var signal = new MouseClickSignalComponent {MousePosition = Input.mousePosition};

                signalRequest.Signal = signal;
                Debug.Log("Send signal!");
            }
        }
    }
}