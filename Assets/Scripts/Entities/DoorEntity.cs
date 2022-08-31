using Components;
using Ecs;
using UnityEngine;

namespace Entities
{
    public class DoorEntity : EntityMonoBehaviour
    {
        [SerializeField] private string pairID;
        [SerializeField] private Vector3 openState;

        protected override void InitializeComponents()
        {
            base.InitializeComponents();
            AddComponentOnEntity(new DoorComponent{PairID = pairID});
            AddComponentOnEntity(new Vector3Component{Position = openState});
        }
    }
}