using System.Collections.Generic;
using UnityEngine;

public class DrawPathHandler : MonoBehaviour
{

	private List<Vector3> _points = new List<Vector3>();
	
	void Update ()
	{
		DrawPoints();
	}

	private void DrawPoints()
	{
		for (var i = 0; i < _points.Count - 1; i++)
		{
			Debug.DrawLine(_points[i], _points[i+1], Color.grey);
		}
	}

	public void AddPoint(Vector3 position)
	{
		_points.Add(position);
	}
}
