using System.Collections.Generic;
using MathDebbuger.Debuggers;
using UnityEngine;
using UnityEngine.Serialization;

namespace MathDebbuger
{
    public class DllEjercicios : MonoBehaviour
    {
        [SerializeField]
        enum Ejercicios
        {
            UNO = 1,
            DOS,
            TRES,
            CUATRO,
            CINCO,
            SEIS,
            SIETE,
            OCHO,
            NUEVE,
            DIEZ
        }

        [SerializeField] private Ejercicios exercise;

        [SerializeField] private Vector3 A;
        [SerializeField] private Vector3 B;

        private Vec3 a;
        private Vec3 b;
        private Vec3 r;

        private void Update()
        {
            a = new Vec3(A);
            b = new Vec3(B);
            
            switch (exercise)
            {
                case Ejercicios.UNO:
                {
                    r = Ejercicio1();
                    break;
                }
                case Ejercicios.DOS:
                {
                    r = Ejercicio2();
                    break;
                }
                case Ejercicios.TRES:
                {
                    r = Ejercicio3();
                    break;
                }
                case Ejercicios.CUATRO:
                {
                    r = Ejercicio4();
                    break;
                }
                case Ejercicios.CINCO:
                {
                    r = Ejercicio5();
                    break;
                }
                case Ejercicios.SEIS:
                {
                    r = Ejercicio6();
                    break;
                }
                case Ejercicios.SIETE:
                {
                    r = Ejercicio7();
                    break;
                }
                case Ejercicios.OCHO:
                {
                    r = Ejercicio8();
                    break;
                }
                case Ejercicios.NUEVE:
                {
                    r = Ejercicio9();
                    break;
                }
                case Ejercicios.DIEZ:
                {
                    r = Ejercicio10();
                    break;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 1, 1, 1f);
            Gizmos.DrawLine(transform.position, transform.position + a);

            Gizmos.color = new Color(0, 0, 0, 1f);
            Gizmos.DrawLine(transform.position, transform.position + b);

            Gizmos.color = new Color(1, 0, 0, 1f);
            Gizmos.DrawLine(transform.position, transform.position + r);
        }

        public Vec3 Ejercicio1()
        {
            return a + b;
        }

        public Vec3 Ejercicio2()
        {
            return b - a;
        }

        public Vec3 Ejercicio3()
        {
            a.Scale(b);

            return a;
        }

        public Vec3 Ejercicio4()
        {
            return Vec3.Cross(b, a);
        }

        public Vec3 Ejercicio5()
        {
            var elapsedTime = Time.deltaTime;

            float totalDuration = 1.0f;
            
            float t = Mathf.Clamp01(elapsedTime / totalDuration);

            Vec3 result = Vec3.Lerp(a, b, t);

            if (t >= 1.0f)
                elapsedTime = 0.0f;
            
            return result;
        }

        public Vec3 Ejercicio6()
        {
            return Vec3.Max(a, b);
        }

        public Vec3 Ejercicio7()
        {
            return Vec3.Project(a, b);
        }

        public Vec3 Ejercicio8()
        {
            return Vec3.Zero;
        }

        public Vec3 Ejercicio9()
        {
            return Vec3.Zero;
        }

        public Vec3 Ejercicio10()
        {
            return Vec3.Zero;
        }
    }
}