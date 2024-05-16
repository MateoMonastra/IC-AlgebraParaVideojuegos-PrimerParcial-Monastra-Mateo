using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

namespace MathDebbuger
{
    public class FrustumController : MonoBehaviour
    {
        [SerializeField] int screenWidth;
        [SerializeField] int screenHeight;

        [SerializeField] float fieldOfViewAngle;
        [SerializeField] float nearClippingPlane;
        [SerializeField] float renderingDistance;
        public float verticalfieldOfViewAngle;

        [FormerlySerializedAs("linesAmount")] [Header("LINES: ")] [SerializeField]
        private int linesAmountH;

        [SerializeField] private int linesAmountV;
        [SerializeField] private int amountPoints;
        [SerializeField] private float pointSpacing;
        public List<List<Line>> Lines = new List<List<Line>>();

        float aspectRatio;
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
            SetLines();
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
            nearLowerLeftVertex = nearLimit - fixedNearCenterX - fixedNearCenterY;
            nearLowerRightVertex = nearLimit + fixedNearCenterX - fixedNearCenterY;

            farUpperLeftVertex = farLimit - fixedFarCenterX + fixedFarCenterY;
            farUpperRightVertex = farLimit + fixedFarCenterX + fixedFarCenterY;
            farLowerLeftVertex = farLimit - fixedFarCenterX - fixedFarCenterY;
            farLowerRightVertex = farLimit + fixedFarCenterX - fixedFarCenterY;

            SetLines();
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

                foreach (var lines in Lines)
                {
                    foreach (var line in lines)
                    {
                        line.Draw();
                    }
                }
            }
        }

        private void DrawFrustumLines()
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

        private void SetVertices()
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

        private void UpdateVertices()
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

        private Vector3 GetFaceNormal(int index)
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
            // var nearLeftPoint = Vec3.Lerp(nearUpperLeftVertex, nearLowerLeftVertex, 0.5f);
            // var farLeftPoint = Vec3.Lerp(farUpperLeftVertex, farLowerLeftVertex, 0.5f);
            //
            // var nearRightPoint = Vec3.Lerp(nearUpperRightVertex, nearUpperRightVertex, 0.5f);
            // var farRightPoint = Vec3.Lerp(farUpperRightVertex, farLowerRightVertex, 0.5f);

            List<List<Line>> ListAux = new List<List<Line>>();
            List<Line> listAux = new List<Line>();

            for (int i = 1; i < linesAmountV + 1; i++)
            {
                Vec3 nearLeftPoint;
                Vec3 farLeftPoint;
                Vec3 nearRightPoint;
                Vec3 farRightPoint;

                if (linesAmountV == 1)
                {
                    nearLeftPoint = Vec3.Lerp(nearUpperLeftVertex, nearLowerLeftVertex,
                        0.5f / linesAmountV * (float)i);
                    farLeftPoint = Vec3.Lerp(farUpperLeftVertex, farLowerLeftVertex,
                        0.5f / linesAmountV * (float)i);

                    nearRightPoint = Vec3.Lerp(nearUpperRightVertex, nearLowerRightVertex,
                        0.5f / linesAmountV * (float)i);
                    farRightPoint = Vec3.Lerp(farUpperRightVertex, farLowerRightVertex,
                        0.5f / linesAmountV * (float)i);
                }
                else
                {
                    nearLeftPoint = Vec3.Lerp(nearUpperLeftVertex, nearLowerLeftVertex,
                        1.0f / linesAmountV * (float)i);
                    farLeftPoint = Vec3.Lerp(farUpperLeftVertex, farLowerLeftVertex,
                        1.0f / linesAmountV * (float)i);

                    nearRightPoint = Vec3.Lerp(nearUpperRightVertex, nearLowerRightVertex,
                        1.0f / linesAmountV * (float)i);
                    farRightPoint = Vec3.Lerp(farUpperRightVertex, farLowerRightVertex,
                        1.0f / linesAmountV * (float)i);
                }

                for (var j = 0; j < linesAmountH; j++)
                {
                    float t;
                    if (linesAmountH == 1)
                    {
                        t = (0.5f / linesAmountH) * ((float)j + 1.0f);
                    }
                    else
                    {
                        t = (1.0f / linesAmountH) * ((float)j + 1.0f);
                    }

                    var newNear = (Vec3.Lerp(nearLeftPoint, nearRightPoint, t));
                    var newFar = (Vec3.Lerp(farLeftPoint, farRightPoint, t));

                    var newLine = new Line(newNear, newFar, amountPoints, pointSpacing);
                    listAux.Add(newLine);
                }

                ListAux.Add(listAux);
            }

            Lines = ListAux;
        }
    }
}