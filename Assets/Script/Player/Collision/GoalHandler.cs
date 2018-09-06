public class GoalHandler : AbstractPlayerHandler
{
    public void OnCollision()
    {
        Player.References.GameController.FinishGame(Player);
    }
}