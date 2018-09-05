using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Properties...")] //
    public bool DevEndlessMoves;
    public int MovesLeft;
    public Text MovesLeftDisplay;

    [Header("References")] //
    public MovementHandler MovementHandler;
    public DrawPathHandler DrawPathHandler;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            if (MovesLeft > 0 || DevEndlessMoves)
                DetectMove(Input.mousePosition);
            else
                Debug.Log("No moves left");

        if (MovesLeftDisplay)
            MovesLeftDisplay.text = MovesLeft.ToString();
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
        MovesLeft--;
        position.z = -0.01f;
        DrawPathHandler.AddPoint(position);
        MovementHandler.Move(position);
    }
}