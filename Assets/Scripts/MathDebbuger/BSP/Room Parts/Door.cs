using System;
using System.Collections.Generic;
using UnityEngine;

namespace MathDebbuger.BSP.Room_Parts
{
    public class Door : MonoBehaviour
    { 
        private enum CalculationType
        {
            XY,
            ZY
        }

        [SerializeField] private CalculationType type;
        [SerializeField] public List<Room> roomsConected;
        [SerializeField] private MeshCollider mesh;
        public bool IsColliding(Vec3 point)
        {
            if (type == CalculationType.XY)
            {
                return CheckXY(point, mesh.bounds);
            }
            else
            {
                return CheckZY(point, mesh.bounds);
            }

        }
        
        private bool CheckXY(Vec3 point, Bounds bounds)
        {
            return point.x >= bounds.center.x - bounds.extents.x &&
                   point.x <= bounds.center.x + bounds.extents.x &&
                   point.y >= bounds.center.y - bounds.extents.y &&
                   point.y <= bounds.center.y + bounds.extents.y;
        }
    
        private bool CheckZY(Vec3 point, Bounds bounds)
        {
            return point.z >= bounds.center.z - bounds.extents.z &&
                   point.z <= bounds.center.z + bounds.extents.z &&
                   point.y >= bounds.center.y - bounds.extents.y &&
                   point.y <= bounds.center.y + bounds.extents.y;
        }
    }
}