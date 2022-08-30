using System;
using Leopotam.EcsLite;
using Signals.Components;
using UnityEngine;

namespace Signals.Systems
{
    public class SignalSpawnerSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            var filter = world.Filter<SignalRequestComponent>().End();
            
            var signalRequestsPool = world.GetPool<SignalRequestComponent>();
            var signalComponentsPool = world.GetPool<SignalComponent>();
        
            foreach (var entityID in filter)
            {
                ref var signalRequestComponent = ref signalRequestsPool.Get(entityID);
                var targetSignalComponentsPool = GetTargetSignalPool(world, signalRequestComponent.Signal.GetType());

                var newSignalEntity = world.NewEntity();
                signalComponentsPool.Add(newSignalEntity);
                targetSignalComponentsPool.AddRaw(newSignalEntity, signalRequestComponent.Signal);
                
                Debug.Log($"[{typeof(SignalSpawnerSystem)}] Create signal: {signalRequestComponent.Signal.GetType()}");
                signalRequestsPool.Del(entityID);
            }
        }

        private IEcsPool GetTargetSignalPool(EcsWorld world, Type targetSignalType)
        {
            var pool = world.GetPoolByType(targetSignalType);

            if (pool != null)
                return pool;

            var getPoolMethodRef = typeof(EcsWorld).GetMethod("GetPool").MakeGenericMethod(targetSignalType);
            return getPoolMethodRef.Invoke(world, null) as IEcsPool;
        }
    }
}