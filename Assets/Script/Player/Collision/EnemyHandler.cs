public class EnemyHandler : AbstractPlayerHandler
{
    public void OnCollision()
    {
        // TODO Play sound
        // TODO ... Then wait a few seconds

        // Restart
        Player.References.GameController.RestartLevel();
    }
}