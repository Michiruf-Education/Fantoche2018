using UnityEngine;

public class PlayerController : AbstractPlayerHandler
{
    void Update()
    {
        if (Player.MovesLeft > 0 || Player.References.GameController.DevEndlessMoves)
            DetectInputs();
        UpdateMovesLeftLabel();
    }

    private void DetectInputs()
    {
        var buttonDown = Input.GetMouseButton(0);
        var buttonUp = Input.GetMouseButtonUp(0);

        // Disable the indicator before the movement detection
        // (to just enable it in the case of mouse down)
        Player.MovementIndicatorHandler.IndicatorEnabled = false;

        // Cancel early
        if (!buttonDown && !buttonUp)
            return;

        var worldPosition = Player.MovementPositionCalculator.CalculateWorldPosition(Input.mousePosition);
        if (!worldPosition.IsSet)
            return;
        var movementPosition = Player.MovementPositionCalculator.CalculateMovementPosition(worldPosition.Value);
        if (!movementPosition.IsSet)
            return;

        if (buttonUp)
        {
            Player.DrawPathHandler.AddPoint(movementPosition.Value);
            Player.MovementHandler.Move(movementPosition.Value);
            Player.MovesLeft--;
        }
        else
        {
            // buttonDown is always true in else case
            Player.MovementIndicatorHandler.IndicatorEnabled = true;
            Player.MovementIndicatorHandler.DrawIndicator(movementPosition.Value);
        }
    }

    private void UpdateMovesLeftLabel()
    {
        if (Player.References.MovesLeftLabel)
            Player.References.MovesLeftLabel.text = Player.MovesLeft.ToString();
    }
}