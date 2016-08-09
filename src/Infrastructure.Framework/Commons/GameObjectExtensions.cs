using System;
using UnityEngine;

public static class GameObjectExtensions
{
    public static bool HasComponent(this GameObject go, Type type)
    {
        return go.GetComponentInChildren(type) != null;
    }
}