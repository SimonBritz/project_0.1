namespace DiasGames.InteractionSystem
{
    public interface ICarryable : DiasGames.IInteractable
    {
        Transform GripPoint   { get; }
        bool      TwoHanded   { get; }
        float     Weight      { get; }
        bool      IsFragile   { get; }
        void      OnDetach();              // вызовем при броске/падении
    }
}
