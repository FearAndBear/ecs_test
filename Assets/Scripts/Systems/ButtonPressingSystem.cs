using Components;
using Signals;
using Leopotam.EcsLite;

namespace Systems
{
    public class ButtonPressingSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var buttonsFilter = world.Filter<ButtonComponent>().Inc<GameObjectComponent>().End();
            
            var signalRequestsPool = world.GetPool<SignalRequestComponent>();
            var buttonsPool = world.GetPool<ButtonComponent>();

            foreach (var buttonEntityID in buttonsFilter)
            {
                ref var buttonComponent = ref buttonsPool.Get(buttonEntityID);
                string pairID = buttonComponent.PairID;

                if (buttonComponent.IsPressed)
                {
                    if (signalRequestsPool.Has(buttonEntityID))
                    {
                        ref var signalRequest = ref signalRequestsPool.Get(buttonEntityID);
                        signalRequest.Signal = new ButtonPressedSignal{PairID = pairID};
                    }
                    else
                    {
                        ref var signalRequest = ref signalRequestsPool.Add(buttonEntityID);
                        signalRequest.Signal = new ButtonPressedSignal{PairID = pairID};
                    }
                }
            }
        }
    }
}