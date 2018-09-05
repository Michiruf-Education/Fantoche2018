using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    [Header("Properties...")] //
    public float Speed;

    [Header("References...")] //
    public Transform Player;

    private Vector3 _targetPosition;

    void Start()
    {
        Stop();
    }

    void Update()
    {
        Player.transform.position = Vector3.MoveTowards(
            Player.transform.position,
            _targetPosition,
            Speed);
    }

    public void Move(Vector3 position)
    {
        Debug.Log("Move :: Moving to " + position);
        _targetPosition = position;
    }

    public void Stop()
    {
        _targetPosition = Player.transform.position;
    }
}