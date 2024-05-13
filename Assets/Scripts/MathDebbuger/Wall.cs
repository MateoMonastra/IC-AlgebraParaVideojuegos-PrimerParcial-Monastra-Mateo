using UnityEditor;
using UnityEngine;

namespace MathDebbuger
{
    public class Wall : MonoBehaviour
    {
        private MyPlane plane;
        // Start is called before the first frame update
        void Start()
        {
            plane = new MyPlane(new Vec3(transform.forward), new Vec3(transform.position));
        }
        
        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                Gizmos.color = new Color(1, 0, 1, 1f);
                Gizmos.DrawLine(transform.position, transform.position + plane.Normal );
            }
        }
    }
}
