namespace MathDebbuger.BSP
{
    public class Point
    {
        public Vec3 position;
        public float radius = 0.2f;

        public Point(Vec3 newPosition)
        {
            position = newPosition;
        }

        public bool IsColliding()
        {
            
            return false;
        }
    }
}
