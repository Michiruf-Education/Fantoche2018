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

        var distance = (_targetPosition - _lastPosition).magnitude;
        var duration = distance / Player.Speed;
        // Check avoids NaN-vectors, because division by zero
        // ... and checks if we are still moving
        if (Mathf.Abs(duration) > 0.1f)
        {
            var timePassed = Time.realtimeSinceStartup - _startTime;
            var distancePercentage = Player.MovementCurve.Evaluate(timePassed / duration);
            nextPosition = Vector3.Lerp(
                _lastPosition,
                _targetPosition,
                distancePercentage);

            // Play sound
            Debug.Log("ApplyPosition :: Position not reached");
            if (!Player.AudioSource.isPlaying && Player.MoveSound)
            {
                Debug.Log("ApplyPosition :: Starting sound");
                Player.AudioSource.clip = Player.MoveSound;
                Player.AudioSource.Play();
            }
        }
        else
        {
            nextPosition = Player.transform.position;

            // Stop sound
            Debug.Log("ApplyPosition :: Position reached");
            if (Player.AudioSource.isPlaying)
            {
                // TODO This is not working yet
                Debug.Log("ApplyPosition :: Stopping sound");
                Player.AudioSource.Stop();
            }
        }

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