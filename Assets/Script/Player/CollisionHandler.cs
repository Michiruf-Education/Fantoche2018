using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollisionHandler : AbstractPlayerHandler
{
    void OnCollisionEnter2D(Collision2D other)
    {
        OnCollision(other.collider);
    }

    void OnCollisionStay2D(Collision2D other)
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
        var parent = other.transform.parent ? other.transform.parent.gameObject.name : "<none>";
        Debug.Log("OnCollision :: Hit other object \"" + other.gameObject.name + "\" with parent \"" + parent + "\"");
        var layer = other.gameObject.layer;
        if (layer == LayerMask.NameToLayer(Layer.Collectible))
            Player.CollectPointHandler.RemoveAndCount(other.transform.parent.gameObject);
        else if (layer == LayerMask.NameToLayer(Layer.Obstacle))
            Player.ObstacleHandler.OnCollision();
        else if (layer == LayerMask.NameToLayer(Layer.Goal))
            Player.GoalHandler.OnCollision();
    }
}