using System;

public class Node : IComparable<Node>
{
    public int x; // x좌표
    public int z; // z좌표
    public int Cost; // 누적 비용
    public Node Parent; // 부모 노드
    
    /// <summary>
    /// 좌표의 정보와 비용을 확인
    /// </summary>
    /// <param name="board">좌표</param>
    /// <param name="cost">누적 비용</param>
    /// <param name="parent">부모 노드</param>
    public Node (int X, int Z, int cost,  Node parent)
    {
        x = X;
        z = Z;
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