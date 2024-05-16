using UnityEngine;

namespace MathDebbuger.BSP
{
    public class Point
    {
        public Vec3 position;
        public float radius = 0.13f;

        public Point(Vec3 newPosition)
        {
            position = newPosition;
        }
    }
}