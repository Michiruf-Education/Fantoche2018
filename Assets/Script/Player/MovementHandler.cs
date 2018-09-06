using UnityEngine;

public class MovementHandler : AbstractPlayerHandler
{
    private Vector3 _targetPosition;

    void Start()
    {
        _targetPosition = Player.transform.position;
    }

    void Update()
    {
        Player.transform.position = Vector3.Lerp(
            Player.transform.position,
            _targetPosition,
            Player.Speed * Time.deltaTime);
    }

    public bool Move(Vector3 position)
    {
        // Calculate obstacle detection
        Debug.Log("Move :: Trying to move to " + position);
        // NOTE Might use Collider.Raycast
        var direction = position - Player.transform.position;
        var result = Physics2D.BoxCast(
            Player.transform.position,
            Player.Collider.size,
            0f, // Player.eulerAngles.z,
            direction,
            direction.magnitude,
            LayerMask.GetMask(Layer.Obstacle));
        Debug.Log("Move :: Hit collider " + (result.collider ? result.collider.gameObject.name : "none"));

        // Get the correct target position
        Vector3 newTargetPosition;
        if (result.collider == null)
        {
            newTargetPosition = position;
        }
        else
        {
            Debug.DrawLine(
                new Vector3(result.centroid.x, result.centroid.y, -1f),
                new Vector3(result.centroid.x, result.centroid.y, 1f),
                Color.red, 3f);
            var space = -Player.SpaceToObstacles * direction.normalized;
            newTargetPosition = new Vector3(result.centroid.x, result.centroid.y) + space;
        }
        Debug.DrawLine(
            newTargetPosition + Vector3.forward,
            newTargetPosition + Vector3.back,
            Color.green, 3f);
        Debug.Log("Move :: Next position shall be " + _targetPosition);

        // Prove for the minimum move distance
        if ((Player.transform.position - newTargetPosition).magnitude < Player.MinDistance)
        {
            Debug.Log("Move :: Next position not away far enough");
            return false;
        }

        // Apply the movement
        Debug.Log("Move :: Moving to next position");
        _targetPosition = newTargetPosition;
        return true;
    }
}