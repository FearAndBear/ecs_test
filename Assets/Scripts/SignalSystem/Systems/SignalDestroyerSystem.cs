using Leopotam.EcsLite;
using Signals.Components;

namespace Signals.Systems
{
    public class SignalDestroyerSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            var filter = world.Filter<SignalComponent>().End();
            
            foreach (var entityID in filter)
            {
                world.DelEntity(entityID);
            }
        }
    }
}