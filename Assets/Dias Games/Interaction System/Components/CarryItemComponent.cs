public class CarryItemComponent : MonoBehaviour, ICarryable
{
    [SerializeField] Transform gripPoint;
    [SerializeField] bool      twoHanded;
    [SerializeField] float     weight   = 1f;
    [SerializeField] bool      fragile;

    public Transform GripPoint => gripPoint;
    public bool      TwoHanded => twoHanded;
    public float     Weight    => weight;
    public bool      IsFragile => fragile;

    public void Interact(GameObject instigator)
    {
        // запустим способность PickUp
        var sys = instigator.GetComponent<DiasGames.AbilitySystem.IAbilitySystem>();
        sys.StartAbilityByName("Pick Up", gameObject);
    }

    public void InteractionCallback(GameObject inst) { }

    public void OnDetach()
    {
        // логика после броска (звук, частицы, разрушение fragile-предмета и т.д.)
    }
}
