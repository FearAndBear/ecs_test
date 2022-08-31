using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
    public class CharacterMovingSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var charactersFilter = world.Filter<GameObjectComponent>().Inc<CharacterComponent>().End();
            var targetPosFilter = world.Filter<Vector3Component>().Inc<CharacterComponent>().End();
            
            var charactersPool = world.GetPool<GameObjectComponent>();
            var targetPosPool = world.GetPool<Vector3Component>();
            
            foreach (var targetPosEntityID in targetPosFilter)
            {
                foreach (var characterEntityID in charactersFilter)
                {
                    var characterGameObject = charactersPool.Get(characterEntityID).GameObject;
                    var target = targetPosPool.Get(targetPosEntityID);
                    
                    characterGameObject.transform.position 
                        = Vector3.MoveTowards(characterGameObject.transform.position, target.Position, 10 * Time.deltaTime);
                }
            }
        }
    }
}