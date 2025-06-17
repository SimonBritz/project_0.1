using UnityEngine;
using DiasGames.AbilitySystem.Core;

namespace DiasGames.AbilitySystem.Abilities
{
    [CreateAssetMenu(fileName = "AutoDropOnFallAbility", menuName = "Dias Games/Abilities/AutoDropOnFallAbility", order = 0)]
    public class AutoDropOnFallAbility : Ability
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