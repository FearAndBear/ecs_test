using Leopotam.EcsLite;
using Signals.Components;

namespace Signals.Systems
{
    public class SignalSpawnerSystem : IEcsInitSystem, IEcsRunSystem
    {
        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            
            var entity = world.NewEntity();
            world.GetPool<SignalContainerComponent>().Add(entity);
        }
        
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            var filter = world.Filter<SignalRequest>().End();
            var pool = world.GetPool<SignalRequest>();
            
            foreach (var entityID in filter)
            {
                pool.Del(entityID);
            }
        }
    }
}