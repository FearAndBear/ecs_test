using Components;
using Ecs;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Entities
{
    public class ButtonEntity : EntityMonoBehaviour
    {
        [SerializeField] private string pairID;

        [Inject] private EcsWorld _world;
        
        protected override void InitializeComponents()
        {
            base.InitializeComponents();
            AddComponentOnEntity(new ButtonComponent{PairID = pairID});
        }

        private void OnTriggerEnter(Collider other)
        {
            ref var buttonComponent = ref _world.GetPool<ButtonComponent>().Get(EntityID);

            if (other.GetComponent<CharacterEntity>())
                buttonComponent.IsPressed = true;
            
            Debug.Log(buttonComponent.IsPressed);
        }
        
        private void OnTriggerExit(Collider other)
        {
            ref var buttonComponent = ref _world.GetPool<ButtonComponent>().Get(EntityID);
            
            if (other.GetComponent<CharacterEntity>())
                buttonComponent.IsPressed = false;
            
            Debug.Log(buttonComponent.IsPressed);
        }
    }
}