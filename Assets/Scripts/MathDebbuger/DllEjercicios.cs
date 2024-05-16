using System.Collections.Generic;
using MathDebbuger.Debuggers;
using UnityEngine;
using UnityEngine.Serialization;

namespace MathDebbuger
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

    public class DllEjercicios : MonoBehaviour
    {
        [SerializeField] private Ejercicios exercise;

        [SerializeField] private Vector3 A;
        [SerializeField] private Vector3 B;
        private Vector3 R;

        private Vec3 a;
        private Vec3 b;
        private Vec3 r;

        public void Init()
        {
            a = new Vec3(A);
            b = new Vec3(B);
            r = new Vec3(R);
        }

        private void Update()
        {
            Init();

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

        public Vec3 Ejercicio1()
        {
            return Vec3.Zero;
        }

        public Vec3 Ejercicio2()
        {
            return Vec3.Zero;
        }

        public Vec3 Ejercicio3()
        {
            return Vec3.Zero;
        }

        public Vec3 Ejercicio4()
        {
            return Vec3.Zero;
        }

        public Vec3 Ejercicio5()
        {
            return Vec3.Zero;
        }

        public Vec3 Ejercicio6()
        {
            return Vec3.Zero;
        }

        public Vec3 Ejercicio7()
        {
            return Vec3.Zero;
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