using Components;
using Signals;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
    public class DoorsMovingSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var buttonsPressedSignalsFilter = world.Filter<ButtonPressedSignal>().End();
            var doorsFilter = world.Filter<DoorComponent>().End();

            var doorsPool = world.GetPool<DoorComponent>();
            var gameObjectsPool = world.GetPool<GameObjectComponent>();
            var buttonsPressedSignalsPool = world.GetPool<ButtonPressedSignal>();
            var vector3Pool = world.GetPool<Vector3Component>();

            foreach (var buttonSignalEntityID in buttonsPressedSignalsFilter)
            {
                ref var signalComponent = ref buttonsPressedSignalsPool.Get(buttonSignalEntityID);

                foreach (var doorEntityID in doorsFilter)
                {
                    ref var doorComponent = ref doorsPool.Get(doorEntityID);

                    if (signalComponent.PairID == doorComponent.PairID)
                    {
                        ref var doorGameObjectComponent = ref gameObjectsPool.Get(doorEntityID);
                        var openDoorPosition = vector3Pool.Get(doorEntityID);

                        var doorTransform = doorGameObjectComponent.GameObject.transform;
                        doorTransform.position = Vector3.MoveTowards(doorTransform.position, openDoorPosition.Position, 2 * Time.deltaTime);
                    }
                } 
            }
        }
    }
}