using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MathDebbuger.BSP.Room_Parts
{
    public class RoomManager : MonoBehaviour
    {
        [SerializeField] private List<Room> rooms;

        [SerializeField] private GameObject player;

        private List<List<Line>> linesToCheck;

        private Room actualRoom;

        private List<Door> doorsChecked = new List<Door>();

        private void Update()
        {
            linesToCheck = player.GetComponent<FrustumController>().Lines;

            foreach (var room in rooms)
            {
                if (room.IsPointInside(new Vec3(player.transform.position)))
                {
                    actualRoom = room;
                    foreach (MeshRenderer renderer in room.GetComponentsInChildren<MeshRenderer>())
                    {
                        renderer.enabled = true;
                    }
                }
                else
                {
                    foreach (MeshRenderer renderer in room.GetComponentsInChildren<MeshRenderer>())
                    {
                        renderer.enabled = false;
                    }
                }
            }

            CheckPlayerLines();
        }

        private void CheckPlayerLines()
        {
            foreach (var line in linesToCheck)
            {
                foreach (var lines in line)
                {
                    ColitionCheck(actualRoom, lines);
                    doorsChecked.Clear();
                }
            }
        }

        private void ColitionCheck(Room room, Line lines)
        {
            foreach (var point in lines.points)
            {
                foreach (var door in room.doors)
                {
                    if ((door.IsColliding(point.position)
                         && !doorsChecked.Contains(door)))
                    {
                        doorsChecked.Add(door);
                        foreach (var roomB in door.roomsConected)
                        {
                            if (room != roomB)
                            {
                                if (roomB.IsPointInside(point.position))
                                {
                                    foreach (MeshRenderer renderer in roomB.GetComponentsInChildren<MeshRenderer>())
                                    {
                                        renderer.enabled = true;
                                    }
                                }

                                ColitionCheck(roomB, lines);
                            }
                        }
                    }
                }
            }
        }
    }
}