using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollisionHandler : AbstractPlayerHandler
{
    void OnCollisionEnter2D(Collision2D other)
    {
        OnCollision(other.collider, true);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        OnCollision(other.collider, false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        OnCollision(other, true);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        OnCollision(other, false);
    }

    private void OnCollision(Collider2D other, bool enter)
    {
        var parent = other.transform.parent ? other.transform.parent.gameObject.name : "<none>";
        Debug.Log("OnCollision :: Hit other object \"" + other.gameObject.name + "\" with parent \"" + parent + "\"");
        var layer = other.gameObject.layer;
        if (layer == LayerMask.NameToLayer(Layer.Collectible))
            Player.CollectPointHandler.OnCollision(other.transform.parent.gameObject, enter);
        else if (layer == LayerMask.NameToLayer(Layer.Enemy))
            Player.EnemyHandler.OnCollision(enter);
        else if (layer == LayerMask.NameToLayer(Layer.Goal))
            Player.GoalHandler.OnCollision(enter);
    }
}