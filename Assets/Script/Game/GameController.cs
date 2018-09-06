using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool DevEndlessMoves;
    
    private PlayerController _playerController;
    
    void Start()
    {
        DontDestroyOnLoad(this);
        
        _playerController = FindObjectOfType<PlayerController>();
    }

    public void StartGame()
    {
        
    }

    public void Notify()
    {
        // TODO Maybe use this?
    }

    public void FinishGame(Player player)
    {
        Debug.LogWarning("Finished game");
    }
}