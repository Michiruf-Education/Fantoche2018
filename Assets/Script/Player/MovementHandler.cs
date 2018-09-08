using UnityEngine;

public class MovementHandler : AbstractPlayerHandler
{
    private Vector3 _lastPosition;
    private Vector3 _targetPosition;
    private float _startTime;

    void Start()
    {
        StopMove();
    }

    void Update()
    {
        ApplyPosition();
    }

    private void ApplyPosition()
    {
        Vector3 nextPosition;

        if (Player.UseVariant2)
        {
            var distance = (_targetPosition - _lastPosition).magnitude;
            var duration = distance / Player.Speed;
            // Check avoids NaN-vectors, because division by zero
            // ... and checks if we are still moving
            if (Mathf.Abs(duration) > 0.001f)
            {
                var timePassed = Time.realtimeSinceStartup - _startTime;
                var distancePercentage = Player.MovementCurve.Evaluate(timePassed / duration);
                nextPosition = Vector3.Lerp(
                    _lastPosition,
                    _targetPosition,
                    distancePercentage);
                // TODO Play sound
            }
            else
            {
                nextPosition = _lastPosition;
            }
        }
        else
        {
            // Variant 1 (works, but bad to configure)
            var distanceToGo = (_targetPosition - _lastPosition).magnitude;
            var distanceRemaining = (_targetPosition - Player.transform.position).magnitude;
            var distanceRemainingFactor = 1f - distanceRemaining / Player.DecelerationDistance;
            var distanceAlreadyGone = distanceToGo - distanceRemaining;
            var distanceAlreadyGoneFactor = distanceAlreadyGone / Player.AccelerationDistance;
            float currentSpeed;
            if (distanceAlreadyGoneFactor < 1)
            {
                currentSpeed = Player.AccelerationCurve.Evaluate(distanceAlreadyGoneFactor) * Player.Speed;
            }
            else if (distanceRemainingFactor < 1)
            {
                currentSpeed = Player.DecelerationCurve.Evaluate(distanceRemainingFactor) * Player.Speed;
            }
            else
            {
                currentSpeed = Player.Speed;
            }
            nextPosition = Vector3.MoveTowards(
                Player.transform.position,
                _targetPosition,
                currentSpeed * Time.deltaTime);
            Debug.Log(
                "distanceToGo " + distanceToGo + "\n" +
                "distanceAlreadyGone " + distanceAlreadyGone + "\n" +
                "distanceRemaining " + distanceRemaining + "\n" +
                "currentSpeed " + currentSpeed + "\n" +
                "nextPosition " + nextPosition
            );
        }

        // Calculate next position (Old lerp variant 1)
        //var nextPosition = Vector3.Lerp(
        //    Player.transform.position,
        //    _targetPosition,
        //    Player.Speed * Time.deltaTime);

        // Switch orientation
        var xDifference = nextPosition.x - Player.transform.position.x;
        if (Mathf.Abs(xDifference) > 0.01f)
        {
            var visual = Player.transform.Find("Visual");
            visual.transform.localScale = new Vector3(
                xDifference > 0 ? -0.8f : 0.8f,
                visual.transform.localScale.y,
                visual.transform.localScale.z);
        }
        
        // Apply position
        Player.transform.position = nextPosition;
    }

    public void Move(Vector3 position)
    {
        _startTime = Time.realtimeSinceStartup;
        _lastPosition = Player.transform.position;
        _targetPosition = position;
    }

    public void MoveImmediately(Vector3 position)
    {
        Player.transform.position = position;
        StopMove();
    }

    public void StopMove()
    {
        _lastPosition = Player.transform.position;
        _targetPosition = Player.transform.position;
    }
}