using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(PlayerWorldReferences))]
public class Player : MonoBehaviour
{
    [Header("General...")] //
    public int MovesLeft;
    public int Points;

    [Header("Movement...")] //
    public float Speed = 1f;
    public bool UseVariant2 = true;
    [FormerlySerializedAs("Movement variant v2")]
    public AnimationCurve MovementCurve;
    public float AccelerationDistance;
    public AnimationCurve AccelerationCurve;
    public float DecelerationDistance;
    public AnimationCurve DecelerationCurve;
    public float MinDistance = 0.1f;
    public float SpaceToObstacles = 0.1f;
    public float IndicatorSpaceFromPlayerToArrowStart;

    [Header("Grid movement...")] //
    public bool GridEnabled;
    public float GridSize = 1f;

    [Header("Sounds...")] //
    public AudioClip CollectObjectSound;
    public AudioClip HitEnemySound;
    public AudioClip FinishLevelSound;
    public AudioClip MoveSound;

    // General
    public PlayerWorldReferences References { get; private set; }
    public BoxCollider2D Collider { get; private set; }
    public PlayerController Controller { get; private set; }
    // Movement
    public LineRenderer MovementIndicator { get; private set; }
    public MovementPositionCalculator MovementPositionCalculator { get; private set; }
    public MovementIndicatorHandler MovementIndicatorHandler { get; private set; }
    public MovementHandler MovementHandler { get; private set; }
    // Collision
    public CollisionHandler CollisionHandler { get; private set; }
    public CollectPointHandler CollectPointHandler { get; private set; }
    public EnemyHandler EnemyHandler { get; private set; }
    public GoalHandler GoalHandler { get; private set; }
    // Draw path
    public DrawPathHandler DrawPathHandler { get; private set; }

    void Start()
    {
        References = GetComponentInChildren<PlayerWorldReferences>();
        Collider = GetComponentInChildren<BoxCollider2D>();
        Controller = gameObject.AddComponent<PlayerController>();

        MovementIndicator = GetComponentInChildren<LineRenderer>();
        MovementPositionCalculator = gameObject.AddComponent<MovementPositionCalculator>();
        MovementIndicatorHandler = gameObject.AddComponent<MovementIndicatorHandler>();
        MovementHandler = gameObject.AddComponent<MovementHandler>();

        CollisionHandler = gameObject.AddComponent<CollisionHandler>();
        CollectPointHandler = gameObject.AddComponent<CollectPointHandler>();
        EnemyHandler = gameObject.AddComponent<EnemyHandler>();
        GoalHandler = gameObject.AddComponent<GoalHandler>();

        DrawPathHandler = gameObject.AddComponent<DrawPathHandler>();

        // Assign the player
        foreach (var handler in GetComponentsInChildren<AbstractPlayerHandler>())
        {
            handler.Player = this;
        }
    }

    public void Reset()
    {
        MovementHandler.StopMove();
    }
}