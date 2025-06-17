using UnityEngine;
using DiasGames.AbilitySystem.Core;

namespace DiasGames.AbilitySystem.Abilities
{
    [CreateAssetMenu(fileName = "HoldTwoHandsAbility", menuName = "Dias Games/Abilities/HoldTwoHandsAbility", order = 0)]
    public class HoldTwoHandsAbility : Ability
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