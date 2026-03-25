using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Utilis;
using Vault_Scavanger.Classes.Utilis.Rooms;
using Vault_Scavanger.Interfaces.CoreInterfaces;

namespace Vault_Scavanger.Classes.Core.VaultBuilder.Building_Procedures;

public class AddCorridors : IBuildProcedure
{
    int Count;
    public AddCorridors(int count)
    {
        Count = count;
    }
    public void Execute(VaultBuilder builder)
    {

        List<Room> rooms = builder.rooms;
        if (rooms.Count == 0)
        {
            for (int i = 0; i < Count; i++)
            {
                int firstEntranceY = builder.rand.Next(1, builder.Y - 1);
                int firstEntranceX = builder.rand.Next(1, builder.X - 1);

                int secondEntranceY = builder.rand.Next(1, builder.Y - 1);
                int secondEntranceX = builder.rand.Next(1, builder.X - 1);

                Corridor corridor = new Corridor((firstEntranceY, firstEntranceX), (secondEntranceY, secondEntranceX));
                builder.corridors.Add(corridor);
            }
        }
        else
        {
            BuildSpanningTree(builder, Count);
            if (Count > builder.rooms.Count - 1) 
            {
                int additionalCorridors = Count - (builder.rooms.Count - 1);
                BuildAdditionalCorridors(builder, additionalCorridors);
                
            }
        }
    }

    (Room, int) FindNearest(Room thisRoom, List<Room> rooms)
    {
        if (rooms.Count == 0)
            return (thisRoom, -1);

        Room nearest = rooms[0];
        int index = 0;
        int minDist = thisRoom.DistanceTo(nearest);

        for (int i = 1; i < rooms.Count; i++)
        {
            Room other = rooms[i];
            int dist = thisRoom.DistanceTo(other);
           
            if (dist < minDist)
            {
                if(other == thisRoom)
                    continue;
                nearest = other;
                index = i;
                minDist = dist;
            }
        }

        return (nearest, index);
    }
    
    void BuildSpanningTree(VaultBuilder builder, int limit = -1)
    {
        List<Room> rooms = builder.rooms;
        if(limit < 0) limit = rooms.Count;
        if(rooms.Count < 2) return;

        List<Room> connected = new List<Room>();
        List<Room> unconnected = new List<Room>(rooms);
        connected.Add(unconnected.GetAndRemove(0));

        while (unconnected.Count > 0 && limit > connected.Count - 1) 
        {
            Room bestFrom = null;
            Room bestTo = null;
            Room connectedTo = null;    
            int bestDist = int.MaxValue;
            int dist = 0;
            int i = 0;
            int desiredIndex = 0;
            foreach (Room UnconnectedRoom in unconnected) 
            {
                (connectedTo, _) = FindNearest(UnconnectedRoom, connected);
                dist = UnconnectedRoom.DistanceTo(connectedTo);
                if (dist < bestDist) 
                {
                    bestFrom = UnconnectedRoom;
                    bestDist = dist;
                    bestTo = connectedTo;
                    desiredIndex = i;
                }
                i++;
            }
            Corridor corridor = new Corridor(bestFrom.GetCorridorEntrance(bestTo, builder.rand), bestTo.GetCorridorEntrance(bestFrom, builder.rand));
            builder.corridors.Add(corridor);
            connected.Add(unconnected.GetAndRemove(desiredIndex));
        }
    }


    void BuildAdditionalCorridors(VaultBuilder builder, int count)
    {
        List<Room> rooms = builder.rooms;

        List<List<Room>> possibleConnections = new List<List<Room>>();
        for (int i = 0; i < builder.rooms.Count; i++)
        {
            possibleConnections.Add(new List<Room>(builder.rooms));
            possibleConnections[i].RemoveAt(i);
        }
        for (int i = 0; i < count; i++)
        {
            bool lookForCorridors = false;
            for (int j = 0; j < possibleConnections.Count; j++)
            {
                if (possibleConnections[j].Count > 0)
                    lookForCorridors = true;
                else
                {
                    possibleConnections.RemoveAt(i);
                    j--;
                }
            }


            if (!lookForCorridors)
            {
                break;
            }

            int randIndex = builder.rand.Next(possibleConnections.Count);
            Room roomFrom = builder.rooms[randIndex];

            Room roomTo = null;
            int index = 0;
            (roomTo, index) = FindNearest(roomFrom, possibleConnections[randIndex]);
            if (!HaveCorridorsBetween(roomFrom, roomTo, builder.corridors))
            {
                Corridor corridor = new Corridor(roomFrom.GetCorridorEntrance(roomTo, builder.rand), roomTo.GetCorridorEntrance(roomFrom, builder.rand));
                builder.corridors.Add(corridor);
            }
            possibleConnections[randIndex].RemoveAt(index);

        }
    }

    bool HaveCorridorsBetween(Room FirstRoom, Room SecondRoom, List<Corridor> corridors) 
    {
        foreach (Corridor corridor in corridors) 
        {
            if(FirstRoom.PointIsInside(corridor.From))
            {
                if (SecondRoom.PointIsInside(corridor.To))
                {
                    return true;
                }
            }

            if (FirstRoom.PointIsInside(corridor.To))
            {
                if (SecondRoom.PointIsInside(corridor.From))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
