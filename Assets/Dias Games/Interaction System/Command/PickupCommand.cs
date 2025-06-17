public class PickupCommand : DiasGames.Command.IActionCommand
{
    public void Execute(DiasGames.AbilitySystem.IAbilitySystem sys)
    {
        if (sys.HasTag("Carrying"))
             sys.StartAbilityByName("Throw");
        else
             sys.StartAbilityByName("Pick Up", sys.Target); // Target кладёт InteractionComponent
    }
}
