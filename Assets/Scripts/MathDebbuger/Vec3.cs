﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CustomMath
{
    public struct Vec3 : IEquatable<Vec3>
    {
        #region Variables

        public float x;
        public float y;
        public float z;

        public float sqrMagnitude
        {
            get { return x * x + y * y + z * z; }
        }

        public Vector3 normalized
        {
            get { return new Vec3(x / this.magnitude, y / this.magnitude, z / this.magnitude); }
        }

        public float magnitude
        {
            get { return Mathf.Sqrt(this.sqrMagnitude); }
        }

        #endregion

        #region constants

        public const float epsilon = 1e-05f;

        #endregion

        #region Default Values

        public static Vec3 Zero
        {
            get { return new Vec3(0.0f, 0.0f, 0.0f); }
        }

        public static Vec3 One
        {
            get { return new Vec3(1.0f, 1.0f, 1.0f); }
        }

        public static Vec3 Forward
        {
            get { return new Vec3(0.0f, 0.0f, 1.0f); }
        }

        public static Vec3 Back
        {
            get { return new Vec3(0.0f, 0.0f, -1.0f); }
        }

        public static Vec3 Right
        {
            get { return new Vec3(1.0f, 0.0f, 0.0f); }
        }

        public static Vec3 Left
        {
            get { return new Vec3(-1.0f, 0.0f, 0.0f); }
        }

        public static Vec3 Up
        {
            get { return new Vec3(0.0f, 1.0f, 0.0f); }
        }

        public static Vec3 Down
        {
            get { return new Vec3(0.0f, -1.0f, 0.0f); }
        }

        public static Vec3 PositiveInfinity
        {
            get { return new Vec3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity); }
        }

        public static Vec3 NegativeInfinity
        {
            get { return new Vec3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity); }
        }

        #endregion

        #region Constructors

        public Vec3(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.z = 0.0f;
        }

        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vec3(Vec3 v3)
        {
            this.x = v3.x;
            this.y = v3.y;
            this.z = v3.z;
        }

        public Vec3(Vector3 v3)
        {
            this.x = v3.x;
            this.y = v3.y;
            this.z = v3.z;
        }

        public Vec3(Vector2 v2)
        {
            this.x = v2.x;
            this.y = v2.y;
            this.z = 0.0f;
        }

        #endregion

        #region Operators

        public static bool operator ==(Vec3 left, Vec3 right)
        {
            float diff_x = left.x - right.x;
            float diff_y = left.y - right.y;
            float diff_z = left.z - right.z;
            float sqrmag = diff_x * diff_x + diff_y * diff_y + diff_z * diff_z;
            return sqrmag < epsilon * epsilon;
        }

        public static bool operator !=(Vec3 left, Vec3 right)
        {
            return !(left == right);
        }

        public static Vec3 operator +(Vec3 leftV3, Vec3 rightV3)
        {
            return new Vec3(leftV3.x + rightV3.x, leftV3.y + rightV3.y, leftV3.z + rightV3.z);
        }

        public static Vec3 operator -(Vec3 leftV3, Vec3 rightV3)
        {
            return new Vec3(leftV3.x - rightV3.x, leftV3.y - rightV3.y, leftV3.z - rightV3.z);
        }

        public static Vec3 operator -(Vec3 v3)
        {
            return new Vec3(-v3.x, -v3.y, -v3.z);
        }

        public static Vec3 operator *(Vec3 v3, float scalar)
        {
            return new Vec3(v3.x * scalar, v3.y * scalar, v3.z * scalar);
        }

        public static Vec3 operator *(float scalar, Vec3 v3)
        {
            return new Vec3(v3.x * scalar, v3.y * scalar, v3.z * scalar);
        }

        public static Vec3 operator /(Vec3 v3, float scalar)
        {
            return new Vec3(v3.x / scalar, v3.y / scalar, v3.z / scalar);
        }

        public static implicit operator Vector3(Vec3 v3)
        {
            return new Vector3(v3.x, v3.y, v3.z);
        }

        public static implicit operator Vector2(Vec3 v2)
        {
            return new Vector3(v2.x, v2.y);
        }

        #endregion

        #region Functions

        public override string ToString()
        {
            return "X = " + x.ToString() + "   Y = " + y.ToString() + "   Z = " + z.ToString();
        }

        public static float Angle(Vec3 from, Vec3 to)
        {
            float dotProduct = Vec3.Dot(from, to);
            float magnitudeA = from.magnitude;
            float magnitudeB = to.magnitude;

            float cosAngle = dotProduct / (magnitudeA * magnitudeB);
            float angle = Mathf.Acos(cosAngle);


            float angleDegrees = angle * Mathf.Rad2Deg;

            return angleDegrees;
        }

        public static Vec3 ClampMagnitude(Vec3 vector, float maxLength)
        {
            float multiplier = vector.magnitude;
            if (maxLength > vector.magnitude)
            {
                multiplier = maxLength;
            }

            return new Vec3(vector.normalized.x, vector.normalized.y, vector.normalized.z) * multiplier;
        }

        public static float Magnitude(Vec3 vector)
        {
            return vector.magnitude;
        }

        public static Vec3 Cross(Vec3 a, Vec3 b)
        {
            return new Vec3(
                a.y * b.z - a.z * b.y,
                a.z * b.x - a.x * b.z,
                a.x * b.y - a.y * b.x);
        }

        public static float Distance(Vec3 a, Vec3 b)
        {
            return a.magnitude - b.magnitude;
        }

        public static float Dot(Vec3 a, Vec3 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public static Vec3 Lerp(Vec3 a, Vec3 b, float t)
        {
            t = Mathf.Clamp01(t);
            Vec3 interpolatePosition = a + (b - a) * t;

            return interpolatePosition;
        }

        public static Vec3 LerpUnclamped(Vec3 a, Vec3 b, float t)
        {
            Vec3 interpolatePosition = a + (b - a) * t;

            return interpolatePosition;
        }

        public static Vec3 Max(Vec3 a, Vec3 b)
        {
            return new Vec3(
                a.x > b.x ? a.x : b.x,
                a.y > b.y ? a.y : b.y,
                a.z > b.z ? a.z : b.z
            );
        }

        public static Vec3 Min(Vec3 a, Vec3 b)
        {
            return new Vec3(
                a.x < b.x ? a.x : b.x,
                a.y < b.y ? a.y : b.y,
                a.z < b.z ? a.z : b.z
            );
        }

        public static float SqrMagnitude(Vec3 vector)
        {
            return vector.sqrMagnitude;
        }

        public static Vec3 Project(Vec3 vector, Vec3 onNormal)
        {
            if (onNormal.magnitude < 0.0001f)
                return Vec3.Zero;
            
            Vec3 projection = Vec3.Dot(vector, onNormal) / onNormal.magnitude * onNormal;

            return projection;
        }

        public static Vec3 Reflect(Vec3 inDirection, Vec3 inNormal)
        {
            float dotProduct = Vec3.Dot(inDirection, inNormal);
            
            Vec3 reflection = inDirection - 2f * dotProduct * inNormal;
            
            return reflection;
        }

        public void Set(float newX, float newY, float newZ)
        {
            this.x = newX;
            this.y = newY;
            this.z = newZ;
        }

        public void Scale(Vec3 scale)
        {
            this.x *= scale.x;
            this.y *= scale.y;
            this.z *= scale.z;
        }

        #endregion

        #region Internals

        public override bool Equals(object other)
        {
            if (!(other is Vec3)) return false;
            return Equals((Vec3)other);
        }

        public bool Equals(Vec3 other)
        {
            return x == other.x && y == other.y && z == other.z;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2);
        }

        #endregion

        public static Vec3 Normalize(Vec3 vector)
        {
            return vector / vector.magnitude;
        }
    }
}