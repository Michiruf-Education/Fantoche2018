using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollisionHandler : MonoBehaviour
{
    [Header("References...")] // 
    public CollisionCollectPointHandler CollisionCollectPointHandler;
    public CollisionObstacleHandler CollisionObstacleHandler;

    private void OnCollisionEnter2D(Collision2D other)
    {
        OnCollision(other.collider);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        OnCollision(other.collider);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        OnCollision(other);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        OnCollision(other);
    }

    private void OnCollision(Collider2D other)
    {
        Debug.Log("OnCollision :: Hit other object " + other.gameObject.name);
        var layer = other.gameObject.layer;
        if (layer == LayerMask.NameToLayer(Layer.Collectable))
            CollisionCollectPointHandler.RemoveAndCount(other.transform.parent.gameObject);
        else if (layer == LayerMask.NameToLayer(Layer.Obstacle))
            CollisionObstacleHandler.OnCollision();
    }
}