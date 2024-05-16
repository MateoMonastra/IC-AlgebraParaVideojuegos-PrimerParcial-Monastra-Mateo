using UnityEngine;

namespace MathDebbuger
{
    public class MyPlane
    {
        private Vec3 normal;
        private float distance;

        public Vec3 Normal
        {
            get => this.normal;
            set => this.normal = value;
        }

        public float Distance
        {
            get => this.distance;
            set => this.distance = value;
        }

        public MyPlane(Vec3 inNormal, Vec3 inPoint)
        {
            this.normal = Vec3.Normalize(inNormal);
            this.distance = Vec3.Dot(this.normal, inPoint) * -1f;
        }

        public MyPlane(Vec3 inNormal, float d)
        {
            this.normal = Vec3.Normalize(inNormal);
            this.distance = d;
        }

        public MyPlane(Vec3 a, Vec3 b, Vec3 c)
        {
            this.normal = Vec3.Normalize(Vec3.Cross(b - a, c - a));
            this.distance = Vec3.Dot(this.normal, a) * -1;
        }

        public void SetNormalAndPosition(Vec3 inNormal, Vec3 inPoint)
        {
            this.normal = Vec3.Normalize(inNormal);
            this.distance = Vec3.Dot(this.normal, inPoint) * -1;
        }

        public void Set3Points(Vec3 a, Vec3 b, Vec3 c)
        {
            this.normal = Vec3.Normalize(Vec3.Cross(b - a, c - a));
            this.distance = Vec3.Dot(this.normal, a) * -1;
        }

        public void Flip()
        {
            this.normal = normal * -1;
            this.distance = distance * -1;
        }

        public void Translate(Vec3 translation)
        {
            this.distance += Vec3.Dot(this.normal, translation);
        }

        public static MyPlane Translate(MyPlane plane, Vec3 translation)
        {
            return new MyPlane(plane.normal, plane.distance += Vec3.Dot(plane.normal, translation));
        }

        public Vec3 ClosestPointOnPlane(Vec3 point)
        {
            float num = Vec3.Dot(this.normal, point) + this.distance;
            return point - this.normal * num;
        }

        public float GetDistanceToPoint(Vec3 point)
        {
            return Vec3.Dot(this.normal, point) + this.distance;
        }

        public bool GetSide(Vec3 point)
        {
            return (double)Vec3.Dot(this.normal, point) + (double)this.distance > 0.0;
        }

        public bool SameSide(Vec3 inPt0, Vec3 inPt1)
        {
            float distanceToPoint1 = this.GetDistanceToPoint(inPt0);
            float distanceToPoint2 = this.GetDistanceToPoint(inPt1);
            return (double)distanceToPoint1 > 0.0 && (double)distanceToPoint2 > 0.0 
                   || (double)distanceToPoint1 <= 0.0 && (double)distanceToPoint2 <= 0.0;
        }
    }
}