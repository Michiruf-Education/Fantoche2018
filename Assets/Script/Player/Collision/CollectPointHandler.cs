using UnityEngine;

public class CollectPointHandler : AbstractPlayerHandler
{
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (!_audioSource)
        {
            var parent = transform.parent ? transform.parent.gameObject.name : "<none>";
            Debug.LogWarning(
                "CollectPointHandler :: Collectable with name \"" + gameObject.name + "\" and parent \"" + parent +
                "\" has no AudioSource");
        }
    }

    void Update()
    {
        if (Player.References.PointsLabel)
            Player.References.PointsLabel.text = Player.Points.ToString();
    }

    public void OnCollision(GameObject go, bool enter)
    {
        if (enter && _audioSource && Player.CollectObjectSound)
            _audioSource.PlayOneShot(Player.CollectObjectSound);
        Player.Points++;
        go.SetActive(false);
    }
}