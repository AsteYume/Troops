using System.Collections.Generic;

public struct MoveValue
{
    public int X;
    public int Z;
    public int MoveCost;
    public int Movement;
    public Queue<int[,]> MoveQueue;
    public HashSet<int[,]> MoveLog;

    public MoveValue(int x, int z, int movement, int moveCost, Queue<int[,]> moveQueue, HashSet<int[,]> moveLog)
    {
        X = x;
        Z = z;
        Movement = movement;
        MoveCost = moveCost;
        MoveQueue = moveQueue;
        MoveLog = moveLog;
    }
}