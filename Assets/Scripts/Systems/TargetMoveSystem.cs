using Components;
using Leopotam.EcsLite;
using Signals;
using UnityEngine;

namespace Systems
{
    public class TargetMoveSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var clickSignalsFilter = world.Filter<MouseClickSignal>().End();
            var charactersFilter = world.Filter<GameObjectComponent>().Inc<CharacterComponent>().End();

            var clickSignalsPool = world.GetPool<MouseClickSignal>();
            var charactersPool = world.GetPool<GameObjectComponent>();
            var targetPosPool = world.GetPool<Vector3Component>();
            
            foreach (var clickSignalEntityID in clickSignalsFilter)
            {
                foreach (var characterEntityID in charactersFilter)
                {
                    var characterGameObject = charactersPool.Get(characterEntityID).GameObject;

                    var clickSignal = clickSignalsPool.Get(clickSignalEntityID);
                    var ray = Camera.main.ScreenPointToRay(clickSignal.Position);

                    if (Physics.Raycast(ray, out var hit))
                    {
                        Vector3 pos = hit.point;
                        pos.y = characterGameObject.transform.position.y;

                        if (targetPosPool.Has(characterEntityID))
                        {
                            ref var component = ref targetPosPool.Get(characterEntityID);
                            component.Position = pos;
                        }
                        else
                        {
                            ref var component = ref targetPosPool.Add(characterEntityID);
                            component.Position = pos;
                        }
                    }
                }
            }
        }
    }
}