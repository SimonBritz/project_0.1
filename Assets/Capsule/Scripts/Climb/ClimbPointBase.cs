using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class ClimbPointBase : MonoBehaviour, IClimbPoint
{
    [Tooltip("Радиус, в котором персонаж может начать взаимодействовать")]
    public float interactionRadius = 1.5f;
    [Tooltip("Куда должен стать персонаж при взаимодействии")]
    public Transform mountPoint;
    [Tooltip("Текст подсказки на экране")]
    public string interactionText = "Press E";

    SphereCollider trigger;

    public float InteractionRadius => interactionRadius;
    public Transform MountPoint => mountPoint;
    public string InteractionText => interactionText;

    protected virtual void Awake()
    {
        trigger = GetComponent<SphereCollider>();
        trigger.isTrigger = true;
        trigger.radius = interactionRadius;
    }

    public abstract void Execute(CharacterClimbController controller);

    // Для отладки в сцене
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
        if (mountPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(mountPoint.position, 0.1f);
            Gizmos.DrawLine(transform.position, mountPoint.position);
        }
    }
}
