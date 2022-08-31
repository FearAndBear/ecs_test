using System;
using Components;
using Leopotam.EcsLite;

namespace Systems
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
                
                signalRequestsPool.Del(entityID);

                object[] components = Array.Empty<object>();
                world.GetComponents(entityID, ref components);
                
                if (components.Length == 0)
                    world.DelEntity(entityID);
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