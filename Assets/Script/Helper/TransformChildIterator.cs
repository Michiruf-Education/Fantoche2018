using System;
using UnityEngine;

public static class TransformChildIterator
{
    public static void ApplyRecursivelyOnAllChildren(this Transform transform, Action<Transform> action)
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            action.Invoke(child);
            ApplyRecursivelyOnAllChildren(child, action);
        }
    }
}