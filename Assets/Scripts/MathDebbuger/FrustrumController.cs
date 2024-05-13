using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace MathDebbuger
{
    public class FrustumController : MonoBehaviour
    {
        [SerializeField] int screenWidth;
        [SerializeField] int screenHeight;

        [SerializeField] float fieldOfViewAngle;
        public float verticalfieldOfViewAngle;
        [SerializeField] float nearClippingPlane;
        [SerializeField] float renderingDistance;

        [Header("LINES: ")] 
        
        private List<Lines> lines;
        [SerializeField] private int linesAmount;
        [SerializeField] private int circleAmount;
        [SerializeField] private int circleDistance;
        [SerializeField] private float circleRadius;

        [SerializeField] float aspectRatio;
        // Start is called before the first frame update

        Vec3 farLimit;
        Vec3 nearLimit;

        // Frustum
        // DL "DownLeft" DR "DownRight"
        // UL "UpperLeft" UR "UpperRight"

        Vec3 nearUpperLeftVertex;
        Vec3 nearUpperRightVertex;
        Vec3 nearLowerLeftVertex;
        Vec3 nearLowerRightVertex;

        Vec3 farUpperLeftVertex;
        Vec3 farUpperRightVertex;
        Vec3 farLowerLeftVertex;
        Vec3 farLowerRightVertex;

        public List<Vec3> vertices = new List<Vec3>();

        void Start()
        {
            SetVertices();
        }

        void Update()
        {
            UpdateVertices();

            aspectRatio = (float)screenWidth / (float)screenHeight;

            verticalfieldOfViewAngle = fieldOfViewAngle / aspectRatio;

            farLimit = new Vec3(transform.position + transform.forward * renderingDistance);
            nearLimit = new Vec3(transform.position + transform.forward * nearClippingPlane);

            float nearPlaneHalfWidth = Mathf.Tan((fieldOfViewAngle / 2) * Mathf.Deg2Rad) * nearClippingPlane;
            float nearPlaneHalfHeight = Mathf.Tan((verticalfieldOfViewAngle / 2) * Mathf.Deg2Rad) * nearClippingPlane;

            float farPlaneHalfWidth = Mathf.Tan((fieldOfViewAngle / 2) * Mathf.Deg2Rad) * renderingDistance;
            float farPlaneHalfHeight = Mathf.Tan((verticalfieldOfViewAngle / 2) * Mathf.Deg2Rad) * renderingDistance;

            // up and Right are the current direction of the local axes of the transform, this means, that it takes in account the current rotation of the object.
            Vec3 up = new Vec3(transform.up);
            Vec3 right = new Vec3(transform.right);

            // all four fixed Centers reference the local position (towards the object transform, and more specifically it's current rotation) that represent the center of the planes.
            Vec3 fixedNearCenterX = right * nearPlaneHalfWidth;
            Vec3 fixedNearCenterY = up * nearPlaneHalfHeight;

            Vec3 fixedFarCenterX = right * farPlaneHalfWidth;
            Vec3 fixedFarCenterY = up * farPlaneHalfHeight;

            // Calculations needed to obtain each vertex of the planes
            nearUpperLeftVertex = nearLimit - fixedNearCenterX + fixedNearCenterY;
            nearUpperRightVertex = nearLimit + fixedNearCenterX + fixedNearCenterY;
            nearLowerLeftVertex =nearLimit - fixedNearCenterX - fixedNearCenterY;
            nearLowerRightVertex =nearLimit + fixedNearCenterX - fixedNearCenterY;

            farUpperLeftVertex = farLimit - fixedFarCenterX + fixedFarCenterY;
            farUpperRightVertex =farLimit + fixedFarCenterX + fixedFarCenterY;
            farLowerLeftVertex = farLimit - fixedFarCenterX - fixedFarCenterY;
            farLowerRightVertex =farLimit + fixedFarCenterX - fixedFarCenterY;
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                Gizmos.DrawCube(farLimit, new Vector3(0.3f, 0.3f, 0.3f));
                Gizmos.DrawCube(nearLimit, new Vector3(0.3f, 0.3f, 0.3f));

                DrawFrustumLines();

                for (int faceIndex = 0; faceIndex <= vertices.Count - 3; faceIndex += 3)
                {
                    Vector3 point;
                    point.x = (vertices[faceIndex].x + vertices[faceIndex + 1].x + vertices[faceIndex + 2].x) / 3;
                    point.y = (vertices[faceIndex].y + vertices[faceIndex + 1].y + vertices[faceIndex + 2].y) / 3;
                    point.z = (vertices[faceIndex].z + vertices[faceIndex + 1].z + vertices[faceIndex + 2].z) / 3;

                    Gizmos.DrawCube(point, new Vector3(0.1f, 0.1f, 0.1f));
                    Gizmos.color = new Color(1, 0, 0, 1f);
                    Gizmos.DrawLine(point, point + GetFaceNormal(faceIndex));
                }

                for (int i = 0; i < linesAmount; i++)
                {
                    Gizmos.DrawLine(lines[i].startPos * i, lines[i].finalPos * i);
                    
                    for (int j = 1; i <= circleAmount; j++)
                    {
                        Gizmos.color = new Color(0, 0, 1, 1f);
                        Gizmos.DrawSphere(lines[i].startPos * j, circleRadius);
                    }
                }
            }
        }

        void DrawFrustumLines()
        {
            Gizmos.DrawLine(nearUpperLeftVertex, farUpperLeftVertex);
            Gizmos.DrawLine(nearUpperRightVertex, farUpperRightVertex);
            Gizmos.DrawLine(nearLowerLeftVertex, farLowerLeftVertex);
            Gizmos.DrawLine(nearLowerRightVertex, farLowerRightVertex);

            Gizmos.DrawLine(nearUpperLeftVertex, nearUpperRightVertex);
            Gizmos.DrawLine(nearUpperRightVertex, nearLowerRightVertex);
            Gizmos.DrawLine(nearLowerRightVertex, nearLowerLeftVertex);
            Gizmos.DrawLine(nearLowerLeftVertex, nearUpperLeftVertex);

            Gizmos.DrawLine(farUpperLeftVertex, farUpperRightVertex);
            Gizmos.DrawLine(farUpperRightVertex, farLowerRightVertex);
            Gizmos.DrawLine(farLowerRightVertex, farLowerLeftVertex);
            Gizmos.DrawLine(farLowerLeftVertex, farUpperLeftVertex);
        }

        void SetVertices()
        {
            vertices.Add(new Vec3(transform.position));
            vertices.Add(farLowerRightVertex);
            vertices.Add(farLowerLeftVertex);

            vertices.Add(new Vec3(transform.position));
            vertices.Add(farUpperLeftVertex);
            vertices.Add(farUpperRightVertex);

            vertices.Add(new Vec3(transform.position));
            vertices.Add(farLowerLeftVertex);
            vertices.Add(farUpperLeftVertex);

            vertices.Add(new Vec3(transform.position));
            vertices.Add(farLowerRightVertex);
            vertices.Add(farUpperRightVertex);

            vertices.Add(nearUpperLeftVertex);
            vertices.Add(nearUpperRightVertex);
            vertices.Add(nearLowerRightVertex);

            vertices.Add(farUpperLeftVertex);
            vertices.Add(farLowerRightVertex);
            vertices.Add(farUpperRightVertex);
        }

        void UpdateVertices()
        {
            vertices[0] = new Vec3(transform.position);
            vertices[1] = farLowerRightVertex;
            vertices[2] = farLowerLeftVertex;

            vertices[3] = new Vec3(transform.position);
            vertices[4] = farUpperLeftVertex;
            vertices[5] = farUpperRightVertex;

            vertices[6] = new Vec3(transform.position);
            vertices[7] = farLowerLeftVertex;
            vertices[8] = farUpperLeftVertex;

            vertices[9] = new Vec3(transform.position);
            vertices[10] = farUpperRightVertex;
            vertices[11] = farLowerRightVertex;

            vertices[12] = nearUpperLeftVertex;
            vertices[13] = nearUpperRightVertex;
            vertices[14] = nearLowerRightVertex;

            vertices[15] = farUpperLeftVertex;
            vertices[16] = farLowerRightVertex;
            vertices[17] = farUpperRightVertex;
        }

        public Vector3 GetFaceNormal(int index)
        {
            // https://www.khronos.org/opengl/wiki/Calculating_a_Surface_Normal#:~:text=A%20surface%20normal%20for%20a,of%20the%20face%20w.r.t.%20winding).
            Vector3 firstVertex = vertices[index];
            Vector3 secondVertex = vertices[index + 1];
            Vector3 thirdVertex = vertices[index + 2];

            Vector3 normal;
            Vector3 firstSecond = secondVertex - firstVertex;
            Vector3 firstThird = thirdVertex - firstVertex;

            normal.x = (firstThird.y * firstSecond.z) - (firstThird.z * firstSecond.y);
            normal.y = (firstThird.z * firstSecond.x) - (firstThird.x * firstSecond.z);
            normal.z = (firstThird.x * firstSecond.y) - (firstThird.y * firstSecond.x);

            float magnitude = Mathf.Sqrt(Mathf.Pow(normal.x, 2) + Mathf.Pow(normal.y, 2) + Mathf.Pow(normal.z, 2));
            Vector3 normalizedNormal = normal / magnitude;

            return normalizedNormal;
        }

        private void SetLines()
        {
            for (int i = 0; i <= linesAmount; i++)
            {
                lines[i].startPos = nearUpperLeftVertex / 2;
                lines[i].finalPos = farUpperLeftVertex / 2;
            
            }
        }
    }
}