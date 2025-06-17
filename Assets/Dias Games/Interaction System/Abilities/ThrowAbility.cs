using UnityEngine;
using DiasGames.AbilitySystem.Core;

namespace DiasGames.AbilitySystem.Abilities
{
    [CreateAssetMenu(fileName = "ThrowAbility", menuName = "Dias Games/Abilities/ThrowAbility", order = 0)]
    public class ThrowAbility : Ability
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