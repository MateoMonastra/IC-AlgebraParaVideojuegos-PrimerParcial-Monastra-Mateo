using System;
using System.Collections.Generic;
using MathDebbuger.BSP.Room_Parts;
using Unity.VisualScripting;
using UnityEngine;

namespace MathDebbuger
{
    public class Room : MonoBehaviour
    {
        [Header("Walls: ")] [SerializeField] public List<Wall> roomWalls;

        [Header("Adjacents: ")] [SerializeField]
        public List<Room> adjacents;

        [Header("Doors: ")] [SerializeField] public List<Door> doors;

        public bool CheckIsPointInside(Transform position)
        {
            int count = 0;
            
            foreach (Wall wall in roomWalls)
            {
                if (wall.IsOnSide(position))
                {
                    count++;
                }
            }
            
            return count == roomWalls.Count;
        }
    }
}