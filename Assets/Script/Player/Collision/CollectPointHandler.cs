using UnityEngine;

public class CollectPointHandler : AbstractPlayerHandler
{
    void Update()
    {
        if (Player.References.PointsLabel)
            Player.References.PointsLabel.text = Player.Points.ToString();
    }

    public void RemoveAndCount(GameObject go)
    {
        Player.Points++;
        Destroy(go);
    }
}