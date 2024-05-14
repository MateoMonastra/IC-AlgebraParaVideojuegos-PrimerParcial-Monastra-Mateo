using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MathDebbuger
{
    public class Line : MonoBehaviour
    { 
        private List<Vec3> points;
        public Vec3 startPos;
        public Vec3 finalPos;
        
        public Line(Vec3 startPos, Vec3 finalPos)
        {
            startPos = startPos;
            finalPos = finalPos;
        }
        
        public void Update(Vec3 start, Vec3 final)
        {
            startPos = start;
            finalPos = final;
        }
        
        public void Draw()
        {
            Gizmos.color = new Color(0, 1, 0, 1f);
            
            Gizmos.DrawLine(startPos, finalPos);

            foreach (var point in points)
            {
                Gizmos.color = new Color(0, 0, 1, 1f);
                Gizmos.DrawSphere(point, 0.2f);
            }
        }

    }
}