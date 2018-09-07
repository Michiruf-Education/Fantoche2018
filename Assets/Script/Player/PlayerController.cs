using UnityEngine;

public class PlayerController : AbstractPlayerHandler
{
    void Update()
    {
        DetectInputs();
        UpdateMovesLeftLabel();
    }

    private void DetectInputs()
    {
        var buttonDown = Input.GetMouseButtonDown(0);
        var buttonUp = Input.GetMouseButtonUp(0);

        // Cancel early
        if (!buttonDown && !buttonUp)
            return;

        var worldPosition = Player.MovementPositionCalculator.CalculateWorldPosition(Input.mousePosition);
        var movementPosition = Player.MovementPositionCalculator.CalculateMovementPosition(worldPosition);
        if (!movementPosition.Valid)
            return;

        if (buttonUp)
        {
            Player.DrawPathHandler.AddPoint(movementPosition.Position);
            Player.MovementHandler.Move(movementPosition.Position);
            Player.MovesLeft--;
        }
        else
            // buttonDown is always true in else case
            Player.MovementIndicatorHandler.DrawIndicator(movementPosition.Position);
    }

    private void UpdateMovesLeftLabel()
    {
        if (Player.References.MovesLeftLabel)
            Player.References.MovesLeftLabel.text = Player.MovesLeft.ToString();
    }
}