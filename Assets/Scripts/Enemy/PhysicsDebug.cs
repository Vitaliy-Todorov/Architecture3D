using UnityEngine;

namespace Scrips.Enemy
{
    public static class PhysicsDebug
    {
        public static void DrawDebug(Vector3 worldpos, float radius, float second)
        {
            Debug.DrawRay(worldpos, radius * Vector3.up, Color.red, second);
            Debug.DrawRay(worldpos, radius * Vector3.down, Color.red, second);
            Debug.DrawRay(worldpos, radius * Vector3.left, Color.red, second);
            Debug.DrawRay(worldpos, radius * Vector3.right, Color.red, second);
            Debug.DrawRay(worldpos, radius * Vector3.forward, Color.red, second);
            Debug.DrawRay(worldpos, radius * Vector3.back, Color.red, second);
        }
    }
}