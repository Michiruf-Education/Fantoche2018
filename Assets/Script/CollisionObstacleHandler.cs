using UnityEngine;

public class CollisionObstacleHandler : MonoBehaviour
{
    [Header("References...")] // 
    public MovementHandler MovementHandler;

    public void OnCollision()
    {
        MovementHandler.Stop();
    }
}