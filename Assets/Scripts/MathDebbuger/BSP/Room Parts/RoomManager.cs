using System.Collections.Generic;
using UnityEngine;

namespace MathDebbuger.BSP.Room_Parts
{
    public class RoomManager : MonoBehaviour
    {
        [SerializeField] private List<Room> rooms;
        
        [SerializeField] private Transform player;

        void Update()
        {
            foreach (var room in rooms)
            {
                if (!room.CheckIsPointInside(player))
                {
                    foreach (MeshRenderer renderer in room.GetComponentsInChildren<MeshRenderer>())
                    {
                        renderer.enabled = false;
                    }
                }
                else
                {
                    foreach (MeshRenderer renderer in room.GetComponentsInChildren<MeshRenderer>())
                    {
                        renderer.enabled = true;
                    }
                }
            }
        }
    }
}
