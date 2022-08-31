using Systems;
using Leopotam.EcsLite;
using Zenject;

namespace Ecs
{
    public class EcsWorldInstaller : MonoInstaller
    {
        private EcsWorld _world;
        private IEcsSystems _systems;

        public override void InstallBindings()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            RegisterSystems();
            
            _systems.Init();
            Container.Bind<EcsWorld>().FromInstance(_world);
        }
        
        private void Update()
        {
            _systems.Run();
        }

        private void OnDestroy()
        {
            _systems?.Destroy ();
            _world?.Destroy ();
        }

        private void RegisterSystems()
        {
            // First systems.
            _systems.Add(new SignalSpawnerSystem());

            // Second systems.
            _systems.Add(new EcsInputSystem());
            _systems.Add(new CharacterMovingSystem());
            _systems.Add(new TargetMoveSystem());
            _systems.Add(new ButtonPressingSystem());
            _systems.Add(new DoorsMovingSystem());
            
            // Last systems.
            _systems.Add(new SignalDestroyerSystem());
        }
    }
}