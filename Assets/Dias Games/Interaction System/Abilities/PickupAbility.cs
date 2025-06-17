using UnityEngine;
using DiasGames.AbilitySystem.Core;

namespace DiasGames.AbilitySystem.Abilities
{
    [CreateAssetMenu(fileName = "PickupAbility", menuName = "Dias Games/Abilities/PickupAbility", order = 0)]
    public class PickupAbility : Ability
    {
        protected override void OnStartAbility(GameObject instigator)
        {
        }

        public override void UpdateAbility(float deltaTime)
        {
        }

        protected override void OnStopAbility(GameObject instigator)
        {
        }
    }
}