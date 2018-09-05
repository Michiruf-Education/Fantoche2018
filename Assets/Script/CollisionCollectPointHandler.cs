using UnityEngine;
using UnityEngine.UI;

public class CollisionCollectPointHandler : MonoBehaviour
{
    public int Points;
    public Text PointsDisplay;

    public void RemoveAndCount(GameObject go)
    {
        Points++;
        if (PointsDisplay)
            PointsDisplay.text = Points.ToString();
        Destroy(go);
    }
}