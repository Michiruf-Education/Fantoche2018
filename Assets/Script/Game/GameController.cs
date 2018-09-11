using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool DevEndlessMoves;
    public bool DevInitLevel;
    public int DevInitLevelNumber;

    private int _currentLevel;
    private LevelController _currentLevelController;

    void Start()
    {
        if (DevInitLevel)
            StartCoroutine(StartDev());
    }

    private IEnumerator StartDev()
    {
        yield return new WaitForSeconds(0.1f);
        StartLevel(DevInitLevelNumber);
    }

    public void StartGame()
    {
        _currentLevel = 0;
        StartNextLevel();
    }

    public void RestartLevel()
    {
        StartLevel(_currentLevel);
    }

    public void StartNextLevel()
    {
        StartLevel(_currentLevel + 1);
    }

    public void StartLevel(int level)
    {
        var controllerName = "Level" + level;
        var levelController = FindObjectsOfType<LevelController>()
            .FirstOrDefault(controller => controller.transform.parent.gameObject.name == controllerName);

        if (levelController != null)
        {
            // Tear down the previous level
            if (_currentLevelController)
                _currentLevelController.EndLevel();

            _currentLevel = level;
            _currentLevelController = levelController;
            _currentLevelController.StartLevel();
        }
        else
        {
            Debug.LogError(
                "Level with LevelController and parent game object with name \"" + controllerName + "\" not found!");
        }
    }

    public void FinishGame(Player player)
    {
        Debug.Log("Trying to finish the game (or level)");
        // NOTE Level Controller stuff should not be here
        
        if (!_currentLevelController)
            throw new ArgumentException("FinishGame :: Level controller not set");

        Debug.Log("FinishGame :: points: " + player.Points + " required: " + _currentLevelController.RequiredPoints);
        if (player.Points < _currentLevelController.RequiredPoints)
        {
            Debug.LogWarning("Player tried to finish the game with " + player.Points + " of " +
                             _currentLevelController.RequiredPoints + " required");
            return;
        }

        if (player.AudioSource && player.FinishLevelSound)
            player.AudioSource.PlayOneShot(player.FinishLevelSound);

        StartNextLevel();
    }
}