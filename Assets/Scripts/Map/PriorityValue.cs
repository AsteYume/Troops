using System.Collections.Generic;

public struct PriorityValue
{
    public int X;
    public int Z;
    public int MoveCost;
    public int Priority;
    public Queue<int[,]> MoveQueue;

    public PriorityValue(int x, int z, int moveCost, int priority, Queue<int[,]> moveQueue)
    {
        X = x;
        Z = z;
        MoveCost = moveCost;
        Priority = priority;
        MoveQueue = moveQueue;
    }
}