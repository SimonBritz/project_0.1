using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterClimb : MonoBehaviour
{
    [Header("Climb Settings")]
    public LayerMask climbableLayers;         // На каких слоях можно хвататься за стены
    public float climbCheckDistance = 1.1f;   // Дистанция до стены для начала climbing
    public float minClimbHeight = 1.2f;       // Минимальная высота для зацепа (от земли до точки захвата)
    public float maxClimbHeight = 3.0f;       // Максимальная высота (например, стена выше 3м нельзя зацепить)
    public KeyCode climbKey = KeyCode.E;      // Клавиша для зацепа
    public KeyCode dropKey = KeyCode.X;       // Клавиша для спуска

    [Header("Debug")]
    public bool drawDebugRay = true;

    private Rigidbody rb;
    private bool isClimbing = false;
    private Vector3 climbNormal;
    private Vector3 climbPoint;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isClimbing)
        {
            if (Input.GetKeyDown(climbKey) && CanStartClimb(out climbPoint, out climbNormal))
            {
                StartClimb(climbPoint, climbNormal);
            }
        }
        else
        {
            // Можно добавить тут лазание по стене (вверх/вниз)
            if (Input.GetKeyDown(dropKey))
            {
                EndClimb();
            }
            else
            {
                HoldOnWall();
            }
        }
    }

    private void StartClimb(Vector3 point, Vector3 normal)
    {
        isClimbing = true;
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Поворачиваем персонажа лицом к стене (опционально)
        Vector3 forward = -normal;
        forward.y = 0;
        if (forward != Vector3.zero)
            transform.forward = forward;

        // Перемещаем персонажа чуть ближе к стене (без залипания)
        Vector3 targetPos = point + normal * 0.5f; // 0.5f — можно подогнать под размер CapsuleCollider
        targetPos.y = Mathf.Clamp(transform.position.y, point.y + minClimbHeight, point.y + maxClimbHeight);

        transform.position = new Vector3(transform.position.x, targetPos.y, transform.position.z);

        // Отключить CharacterRun/Jump (если нужно)
        // Например: GetComponent<CharacterRun>().enabled = false;
        // Animator.SetBool("Climbing", true); // Если используешь анимации
    }

    private void HoldOnWall()
    {
        // Персонаж "залипает" на стене, никакой velocity
        rb.linearVelocity = Vector3.zero;
        rb.useGravity = false;
        // Можно добавить: Animator.SetFloat("ClimbSpeed", 0);
    }

    private void EndClimb()
    {
        isClimbing = false;
        rb.useGravity = true;
        // Включить CharacterRun/Jump обратно, если отключал
        // Animator.SetBool("Climbing", false);
    }

    // Можно начать climbing?
    private bool CanStartClimb(out Vector3 point, out Vector3 normal)
    {
        point = Vector3.zero;
        normal = Vector3.zero;

        // Отправляем Raycast вперёд (чуть выше центра)
        Vector3 origin = transform.position + Vector3.up * 1.1f;
        RaycastHit hit;

        if (Physics.Raycast(origin, transform.forward, out hit, climbCheckDistance, climbableLayers))
        {
            float wallHeight = hit.point.y - GetGroundY();
            if (wallHeight > minClimbHeight && wallHeight < maxClimbHeight)
            {
                point = hit.point;
                normal = hit.normal;
                return true;
            }
        }

        return false;
    }

    // Возвращает уровень земли под персонажем (по Raycast вниз)
    private float GetGroundY()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 0.2f, Vector3.down, out hit, 3f, climbableLayers | LayerMask.GetMask("Default")))
        {
            return hit.point.y;
        }
        return transform.position.y;
    }

    void OnDrawGizmosSelected()
    {
        if (drawDebugRay)
        {
            Vector3 origin = transform.position + Vector3.up * 1.1f;
            Gizmos.color = Color.green;
            Gizmos.DrawRay(origin, transform.forward * climbCheckDistance);
        }
    }
}
