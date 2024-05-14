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

        public Line(Vec3 startPos, Vec3 finalPos, int amountPoints, float spacing)
        {
            this.startPos = startPos;
            this.finalPos = finalPos;

            SetPoints(amountPoints,spacing);
        }

        public void UpdateLine(Vec3 start, Vec3 final, int amountPoints,float spacing )
        {
            this.startPos = start;
            this.finalPos = final;
            
            SetPoints(amountPoints,spacing);
        }

        public void SetPoints(int amountPoints, float spacing)
        {

            if (points.Count != amountPoints)
            {
                List<Vec3> listAux = new List<Vec3>();
                for (int i = 0; i < amountPoints; i++)
                {
                    var place = Vec3.Lerp(startPos, finalPos, spacing * (i + 1));

                    var newPoint = new Vec3(place);
                    listAux.Add(newPoint);
                }

                points = listAux;
            }
            else
            {
                for (int i = 0; i < amountPoints; i++)
                {
                    points[i] = Vec3.Lerp(startPos, finalPos, spacing * (i + 1));
                }
            }
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