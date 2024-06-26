using UnityEngine;

namespace MathDebbuger.BSP.Room_Parts
{
    public class Wall : MonoBehaviour
    {
        public MyPlane plane;

        void Start()
        {
            plane = new MyPlane(new Vec3(transform.forward), new Vec3(transform.position));
        }
        
        public void Draw()
        {
                Gizmos.color = new Color(1, 0, 1, 1f);
                Gizmos.DrawLine(transform.position, transform.position + plane.Normal );
            
        }

        public bool IsOnSide(Vec3 player)
        {
            return plane.GetSide(player);
        }
    }
}
