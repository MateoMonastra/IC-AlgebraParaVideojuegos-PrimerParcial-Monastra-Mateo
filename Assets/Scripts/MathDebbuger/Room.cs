using System.Collections.Generic;
using UnityEngine;

namespace MathDebbuger
{
    public class Room : MonoBehaviour
    {
        [Header("Walls: ")]
        [SerializeField] public List<Wall> roomWalls;
    
        [Header("Adjacents: ")]
        [SerializeField] public List<Room> adjacents;
    
        [Header("Doors: ")]
        [SerializeField] public List<Door> doors;
        // Start is called before the first frame update
        void Start()
        {
            foreach (var Wall in roomWalls)
            {
            
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
