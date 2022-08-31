using System;
using Components;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Ecs
{
    public class EntityMonoBehaviour : MonoBehaviour
    {
        [InjectOptional] private EcsWorld _world;

        public int EntityID { get; private set; }
        
        private void Awake()
        {
            EntityID = _world.NewEntity();
            InitializeComponents();
        }

        protected void AddComponentOnEntity<T>(T component) where T : struct
        {
            ref var newComponent = ref _world.GetPool<T>().Add(EntityID);
            newComponent = component;
        }
        
        protected void AddComponentOnEntity<T>() where T : struct
        {
            _world.GetPool<T>().Add(EntityID);
        }

        protected virtual void InitializeComponents()
        {
            AddComponentOnEntity(new GameObjectComponent {GameObject = gameObject});
        }
    }
}