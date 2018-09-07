using UnityEngine;

public class MovementPositionCalculator : AbstractPlayerHandler
{
    public Vector2 CalculateWorldPosition(Vector3 mousePosition)
    {
        var ray = Camera.main.ScreenPointToRay(mousePosition);
        var result = Physics2D.GetRayIntersection(ray, Constants.RaycastDistance, LayerMask.GetMask(Layer.GameField));
        if (result.collider != null)
        {
            Debug.Log("DetectMove :: Hit -> " + result.collider.gameObject.name);
            return result.point;
        }

        // TODO Remove second (old 3D variant)
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Constants.RaycastDistance, LayerMask.GetMask(Layer.GameField)))
        {
            Debug.Log("DetectMove :: Hit -> " + hit.collider.gameObject.name);
            return hit.point;
        }

        return Vector2.zero;
    }

    public Result CalculateMovementPosition(Vector3 position)
    {
        var direction = position - Player.transform.position;

        // Calculate obstacle detection
        Debug.Log("Move :: Trying to move to " + position);
        // NOTE Might use Collider.Raycast
        var result = Physics2D.BoxCast(
            Player.transform.position,
            Player.Collider.size,
            0f, // Player.eulerAngles.z,
            direction,
            direction.magnitude,
            LayerMask.GetMask(Layer.Obstacle));
        Debug.Log("CalculatePosition :: Hit collider " + (result.collider ? result.collider.gameObject.name : "none"));

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
        Debug.Log("CalculatePosition :: Next position shall be " + newTargetPosition);

        // Prove for the minimum move distance
        if ((Player.transform.position - newTargetPosition).magnitude < Player.MinDistance)
        {
            Debug.Log("CalculatePosition :: Next position not away far enough");
            return new Result(Vector3.zero, false);
        }

        if (Player.GridEnabled)
        {
            newTargetPosition.x = Mathf.Round(newTargetPosition.x / Player.GridSize) * Player.GridSize;
            newTargetPosition.y = Mathf.Round(newTargetPosition.y / Player.GridSize) * Player.GridSize;
        }

        return new Result(newTargetPosition, true);
    }

    public class Result
    {
        public readonly Vector3 Position;
        public readonly bool Valid;

        public Result(Vector3 position, bool valid)
        {
            Position = position;
            Valid = valid;
        }
    }
}