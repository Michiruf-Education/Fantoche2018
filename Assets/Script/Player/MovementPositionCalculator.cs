using System;
using UnityEngine;

public class MovementPositionCalculator : AbstractPlayerHandler
{
    public NullableStruct<Vector2> CalculateWorldPosition(Vector3 mousePosition)
    {
        if (Camera.main == null)
            throw new Exception("Main camera not set!");

        var ray = Camera.main.ScreenPointToRay(mousePosition);
        var result = Physics2D.GetRayIntersection(ray, Constants.RaycastDistance, LayerMask.GetMask(Layer.GameField));
        if (result.collider != null)
        {
            Debug.Log("CalculateWorldPosition :: Hit -> " + result.collider.gameObject.name);
            return new NullableStruct<Vector2>(result.point, true);
        }

        // NOTE Remove second (old 3D variant)
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Constants.RaycastDistance, LayerMask.GetMask(Layer.GameField)))
        {
            Debug.Log("CalculateWorldPosition :: Hit -> " + hit.collider.gameObject.name);
            return new NullableStruct<Vector2>(hit.point, true);
        }

        return new NullableStruct<Vector2>(Vector2.zero, false);
    }

    public NullableStruct<Vector3> CalculateMovementPosition(Vector3 position)
    {
        var direction = position - Player.transform.position;

        // Calculate obstacle detection
        Debug.Log("CalculateMovementPosition :: Trying to move to " + position);
        // NOTE Might use Collider.Raycast
        var result = Physics2D.BoxCast(
            Player.transform.position,
            Player.Collider.size,
            0f, // Player.eulerAngles.z,
            direction,
            direction.magnitude + Player.SpaceToObstacles,
            LayerMask.GetMask(Layer.Obstacle));
        Debug.Log("CalculateMovementPosition :: Hit collider " +
                  (result.collider ? result.collider.gameObject.name : "none"));

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
        Debug.Log("CalculateMovementPosition :: Next position shall be " + newTargetPosition);

        // Prove for the minimum move distance
        if ((Player.transform.position - newTargetPosition).magnitude < Player.MinDistance)
        {
            Debug.Log("CalculateMovementPosition :: Next position not away far enough");
            return new NullableStruct<Vector3>(Vector3.zero, false);
        }

        if (Player.GridEnabled)
        {
            newTargetPosition.x = Mathf.Round(newTargetPosition.x / Player.GridSize) * Player.GridSize;
            newTargetPosition.y = Mathf.Round(newTargetPosition.y / Player.GridSize) * Player.GridSize;
        }

        return new NullableStruct<Vector3>(newTargetPosition, true);
    }
}