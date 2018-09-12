using UnityEngine;

public class LevelController : MonoBehaviour
{
    [Header("Level properties...")] //
    public int AvailableMoves;
    public int RequiredPoints;

    [Header("References...")] //
    public Transform StartPosition;
    public GameObject Container;

    private Player _player;

    void Start()
    {
        _player = FindObjectOfType<Player>();

        // Hide all containers at start
        Container.SetActive(false);
    }

    public void StartLevel()
    {
        Container.SetActive(true);
        Container.transform.ApplyRecursivelyOnAllChildren(transform1 =>
        {
            Debug.Log(transform1.gameObject.name);
            transform1.gameObject.SetActive(true);
        });
        _player.MovesLeft = AvailableMoves;
        _player.Points = 0;
        _player.MovementHandler.MoveImmediately(StartPosition.position);
    }

    public void EndLevel()
    {
        Container.SetActive(false);
    }
}