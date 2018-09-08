using UnityEngine;

public class GoalHandler : AbstractPlayerHandler
{
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (!_audioSource)
        {
            var parent = transform.parent ? transform.parent.gameObject.name : "<none>";
            Debug.LogWarning(
                "GoalHandler :: Goal with name \"" + gameObject.name + "\" and parent \"" + parent +
                "\" has no AudioSource");
        }
    }

    public void OnCollision(bool enter)
    {
        if (enter && _audioSource && Player.CollectObjectSound)
            _audioSource.PlayOneShot(Player.CollectObjectSound);
        Player.References.GameController.FinishGame(Player);
    }
}