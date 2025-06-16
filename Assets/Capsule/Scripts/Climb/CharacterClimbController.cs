using System.Collections;
using UnityEngine;
using TraversalPro; // ваш namespace, где лежит ICharacterMotor

[RequireComponent(typeof(Rigidbody))]
public class CharacterClimbController : MonoBehaviour
{
    [Header("Input")]
    public KeyCode interactKey = KeyCode.E;

    [Header("Settings")]
    public float moveToPointDuration = 0.3f;

    private Rigidbody rb;
    private ICharacterMotor motor;

    private IClimbPoint nearestPoint;
    private bool inInteraction;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        motor = GetComponent<ICharacterMotor>();
    }

    void Update()
    {
        if (inInteraction) return;

        // 1) Ищем ближайшую точку IClimbPoint
        FindNearestPoint();

        // 2) Показываем подсказку (UI) — здесь просто Debug
        if (nearestPoint != null)
            DebugOverlay.Show(nearestPoint.InteractionText); 
        else
            DebugOverlay.Hide();

        // 3) Если нажали и есть точка — выполняем Execute
        if (nearestPoint != null && Input.GetKeyDown(interactKey))
        {
            nearestPoint.Execute(this);
        }
    }

    void FindNearestPoint()
    {
        IClimbPoint best = null;
        float bestDist = float.MaxValue;
        foreach (var p in GameObject.FindObjectsOfType<MonoBehaviour>())
        {
            if (p is IClimbPoint cp)
            {
                float d = Vector3.Distance(transform.position, cp.MountPoint.position);
                if (d < cp.InteractionRadius && d < bestDist)
                {
                    bestDist = d;
                    best = cp;
                }
            }
        }
        nearestPoint = best;
    }

    #region API для точек
    /// <summary>
    /// Переход в состояние "climb" — двигаем к точке и блокируем Traversal Pro
    /// </summary>
    public void StartClimb(Vector3 targetPos, Quaternion targetRot)
    {
        StartCoroutine(DoClimbRoutine(targetPos, targetRot));
    }

    IEnumerator DoClimbRoutine(Vector3 pos, Quaternion rot)
    {
        inInteraction = true;

        // Блокируем базовое движение
        motor.IsGrounded = true;          // чтобы CharacterMotor не считал freefall
        rb.useGravity = false;
        rb.velocity = Vector3.zero;

        // Отключаем ввод Traversal Pro (например, CharacterRun.enabled = false)
        var run = GetComponent<MonoBehaviour>("CharacterRun");
        if (run) run.enabled = false;

        Quaternion startRot = transform.rotation;
        Vector3 startPos = transform.position;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / moveToPointDuration;
            transform.position = Vector3.Lerp(startPos, pos, t);
            transform.rotation = Quaternion.Slerp(startRot, rot, t);
            yield return null;
        }

        // После подъёма — сразу спускаем управление обратно, или ждём drop
        EndInteraction();
    }

    /// <summary>
    /// Переход в состояние "drop" — спрыгиваем вниз
    /// </summary>
    public void StartDrop(Vector3 targetPos)
    {
        StartCoroutine(DoDropRoutine(targetPos));
    }

    IEnumerator DoDropRoutine(Vector3 pos)
    {
        inInteraction = true;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;

        // Телепорт чуть выше, чтобы не задеть коллайдер
        Vector3 above = pos + Vector3.up * 0.5f;
        transform.position = above;

        yield return new WaitForSeconds(0.1f);

        // Включаем физику
        rb.useGravity = true;
        yield return new WaitForSeconds(0.1f);

        EndInteraction();
    }

    void EndInteraction()
    {
        // Включаем Traversal Pro обратно
        motor.IsGrounded = false;
        rb.useGravity = true;
        var run = GetComponent<MonoBehaviour>("CharacterRun");
        if (run) run.enabled = true;
        inInteraction = false;
    }
    #endregion
}
