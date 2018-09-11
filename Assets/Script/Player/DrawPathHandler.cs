using System;
using System.Collections.Generic;
using UnityEngine;

// NOTE Not yet needed
[Obsolete]
public class DrawPathHandler : AbstractPlayerHandler
{
    private readonly List<Vector2> _points = new List<Vector2>();

    void Update()
    {
        DrawPoints();
    }

    private void DrawPoints()
    {
        for (var i = 0; i < _points.Count - 1; i++)
        {
            Debug.DrawLine(_points[i], _points[i + 1], Color.grey);
        }
    }

    public void AddPoint(Vector2 position)
    {
        _points.Add(position);
    }
}