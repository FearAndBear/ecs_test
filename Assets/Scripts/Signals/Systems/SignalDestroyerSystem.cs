using Leopotam.EcsLite;
using Signals.Components;

namespace Signals.Systems
{
    public class SignalDestroyerSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            var filter = world.Filter<BaseSignal>().End();
            var pool = world.GetPool<BaseSignal>();
            
            foreach (var entityID in filter)
            {
                pool.Del(entityID);
            }
        }
    }
}