using UnityEngine;
using UnityEngine.UI;

public class PlayerController : AbstractPlayerHandler
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            if (Player.MovesLeft > 0 || Player.References.GameController.DevEndlessMoves)
                DetectMove(Input.mousePosition);
            else
                Debug.Log("No moves left");

        if (Player.References.MovesLeftLabel)
            Player.References.MovesLeftLabel.text = Player.MovesLeft.ToString();
    }

    private void DetectMove(Vector3 mousePosition)
    {
        var ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Constants.RaycastDistance, LayerMask.GetMask(Layer.GameField)))
        {
            Debug.Log("DetectMove :: Hit -> " + hit.collider.gameObject.name);
            StartMove(hit.point);
        }
    }

    private void StartMove(Vector3 position)
    {
        position.z = -0.01f;

        if (Player.MovementHandler.Move(position))
        {
            Player.MovesLeft--;
            Player.DrawPathHandler.AddPoint(position);
        }
    }
}