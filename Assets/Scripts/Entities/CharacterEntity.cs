using Components;
using Ecs;

namespace Entities
{
    public class CharacterEntity : EntityMonoBehaviour
    {
        protected override void InitializeComponents()
        {
            base.InitializeComponents();
            AddComponentOnEntity<CharacterComponent>();
        }
    }
}