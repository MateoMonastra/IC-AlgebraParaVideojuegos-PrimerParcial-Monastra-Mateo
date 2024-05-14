using System;
using System.Collections.Generic;
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

        [Header("Floor: ")] [SerializeField] public List<Door> floor;

        private Transform player;
        // Start is called before the first frame update

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

        void Update()
        {
            if (!CheckPlayerIsInside(player))
            {
                foreach (MeshRenderer renderer in transform.GetComponentsInChildren<MeshRenderer>())
                {
                    renderer.enabled = false;
                }
            }
            else
            {
                foreach (MeshRenderer renderer in transform.GetComponentsInChildren<MeshRenderer>())
                {
                    renderer.enabled = true;
                }
            }
        }

        private bool CheckPlayerIsInside(Transform player)
        {
            int count = 0;
            
            foreach (Wall wall in roomWalls)
            {
                if (wall.IsOnSide(player))
                {
                    count++;
                }
            }
            
            return count == roomWalls.Count;
        }
    }
}