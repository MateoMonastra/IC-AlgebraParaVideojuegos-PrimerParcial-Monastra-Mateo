using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MathDebbuger
{
    public class Line
    { 
        private List<Vec3> points = new List<Vec3>();
        public Vec3 startPos;
        public Vec3 finalPos;
        
        public Line(Vec3 startPos, Vec3 finalPos)
        {
            this.startPos = startPos;
            this.finalPos = finalPos;
        }
        
        public void UpdateLine(Vec3 start, Vec3 final)
        {
            this.startPos = start;
            this.finalPos = final;
        }

        public void UpdateBalls()
        {
            
        }
        
        public void Draw()
        {
            Gizmos.color = new Color(0, 1, 0, 1f);
            
            Gizmos.DrawLine(startPos, finalPos);

            // foreach (var point in points)
            // {
            //     Gizmos.color = new Color(0, 0, 1, 1f);
            //     Gizmos.DrawSphere(point, 0.2f);
            // }
        }

    }
}