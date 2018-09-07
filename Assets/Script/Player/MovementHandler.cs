using UnityEngine;

public class MovementHandler : AbstractPlayerHandler
{
    private bool _hasTarget;
    private Vector3 _targetPosition;

    void Start()
    {
        ResetTargetPosition();
    }

    void Update()
    {
        Player.transform.position = Vector3.Lerp(
            Player.transform.position,
            _targetPosition,
            Player.Speed * Time.deltaTime);
    }

    public void Move(Vector3 position)
    {
        _targetPosition = position;
    }

    public void MoveImmediately(Vector3 position)
    {
        Player.transform.position = position;
        ResetTargetPosition();
    }

    public void ResetTargetPosition()
    {
        _targetPosition = Player.transform.position;
    }
}