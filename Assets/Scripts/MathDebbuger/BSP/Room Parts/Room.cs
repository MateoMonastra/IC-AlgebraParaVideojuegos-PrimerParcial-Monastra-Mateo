using System;
using System.Collections.Generic;
using MathDebbuger.BSP.Room_Parts;
using Unity.VisualScripting;
using UnityEngine;

namespace MathDebbuger
{
    public class Room : MonoBehaviour
    {
        [Header("Walls: ")] 
        [SerializeField] public List<Wall> roomWalls;

        [Header("Adjacent: ")]
        [SerializeField] public List<Room> adjacent;

        [Header("Doors: ")]
        [SerializeField] public List<Door> doors;

        public bool IsPointInside(Vec3 position)
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