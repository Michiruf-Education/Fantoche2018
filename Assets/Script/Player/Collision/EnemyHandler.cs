using UnityEngine;

public class EnemyHandler : AbstractPlayerHandler
{
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (!_audioSource)
        {
            var parent = transform.parent ? transform.parent.gameObject.name : "<none>";
            Debug.LogWarning(
                "EnemyHandler :: Enemy with name \"" + gameObject.name + "\" and parent \"" + parent +
                "\" has no AudioSource");
        }
    }

    public void OnCollision(bool enter)
    {
        if (enter && _audioSource && Player.HitEnemySound)
            _audioSource.PlayOneShot(Player.HitEnemySound);
        // TODO ... Then wait a few seconds

        // Restart
        Player.References.GameController.RestartLevel();
    }
}