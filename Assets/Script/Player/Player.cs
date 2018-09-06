using UnityEngine;

[RequireComponent(typeof(PlayerWorldReferences))]
public class Player : MonoBehaviour
{
    [Header("General...")] //
    public int MovesLeft;
    public int Points;

    [Header("Movement...")] //
    public float Speed = 1f;
    public float MinDistance = 0.1f;
    public float SpaceToObstacles = 0.01f;

    // General
    public PlayerWorldReferences References { get; private set; }
    public BoxCollider2D Collider { get; private set; }
    public PlayerController Controller { get; private set; }
    // Movement
    public MovementHandler MovementHandler { get; private set; }
    // Collision
    public CollisionHandler CollisionHandler { get; private set; }
    public CollectPointHandler CollectPointHandler { get; private set; }
    public GoalHandler GoalHandler { get; private set; }
    public ObstacleHandler ObstacleHandler { get; private set; }
    // Draw path
    public DrawPathHandler DrawPathHandler { get; private set; }

    void Start()
    {
        References = GetComponentInChildren<PlayerWorldReferences>();
        Collider = GetComponentInChildren<BoxCollider2D>();
        Controller = gameObject.AddComponent<PlayerController>();

        MovementHandler = gameObject.AddComponent<MovementHandler>();

        CollisionHandler = gameObject.AddComponent<CollisionHandler>();
        CollectPointHandler = gameObject.AddComponent<CollectPointHandler>();
        GoalHandler = gameObject.AddComponent<GoalHandler>();
        ObstacleHandler = gameObject.AddComponent<ObstacleHandler>();

        DrawPathHandler = gameObject.AddComponent<DrawPathHandler>();

        // Assign the player
        Controller.Player = this;
        MovementHandler.Player = this;
        CollisionHandler.Player = this;
        CollectPointHandler.Player = this;
        GoalHandler.Player = this;
        ObstacleHandler.Player = this;
        DrawPathHandler.Player = this;
    }
}