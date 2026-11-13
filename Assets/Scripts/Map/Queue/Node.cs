using System;

public class Node : IComparable<Node>
{
    public int[,] Board; // 이 노드의 좌표
    public int Cost; // 누적 비용
    public Node Parent; // 부모 노드
    
    /// <summary>
    /// 좌표의 정보와 비용을 확인
    /// </summary>
    /// <param name="board">좌표</param>
    /// <param name="cost">누적 비용</param>
    /// <param name="parent">부모 노드</param>
    public Node (int[,] board, int cost,  Node parent)
    {
        Board = board;
        Cost = cost;
        Parent = parent;
    }
    
    /// <summary>
    /// 현재노드와 인수 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public int CompareTo(Node other)
    {
        return Cost.CompareTo(other.Cost);
    }
}