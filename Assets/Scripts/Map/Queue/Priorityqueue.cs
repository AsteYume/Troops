using System;
using System.Collections.Generic;

public class PriorityQueue<T>
{
    private List<T> data; // 리스트
    private Func<T, T, int> comparer; // 비교기

    public int Count => data.Count;

    public bool IsEmpty() => data.Count == 0;

    public PriorityQueue() // 생성자
    {
        data = new List<T>();
        comparer = Comparer<T>.Default.Compare;
    }

    public void Enqueue(T item)
    {
        data.Add(item); // 데이터 추가
        int currentIndex = data.Count - 1; // 맨 뒤에서부터 시작

        while (currentIndex > 0) // 맨 앞에 도착하거나 중단될때까지 반복
        {
            int parentIndex = (currentIndex - 1) / 2; // 이진트리 
            if (comparer(data[currentIndex], data[parentIndex]) >= 0)
                break;

            SwapElements(currentIndex, parentIndex);
            currentIndex = parentIndex;
        }
    }

    public T Dequeue()
    {
        if (data.Count == 0)
            throw new InvalidOperationException("Queue is empty");

        int lastIndex = data.Count - 1; // 맨뒤
        T frontItem = data[0]; // 맨앞 원소를 저장해둠
        data[0] = data[lastIndex]; // 맨앞에 맨뒤원소를 저장해둠
        data.RemoveAt(lastIndex); // 맨 뒤를 지움
        
        lastIndex--; // 인덱스 포인터 하나 앞으로 땡김
        int parentIndex = 0; // 맨앞을 부모 인덱스로 지정 

        while (true)
        {
            int leftChildIndex = parentIndex * 2 + 1; 

            if (leftChildIndex > lastIndex)
                break;

            int rightChildIndex = leftChildIndex + 1;
            if (rightChildIndex <= lastIndex && comparer(data[rightChildIndex], data[leftChildIndex]) < 0)
                leftChildIndex = rightChildIndex;

            if (comparer(data[parentIndex], data[leftChildIndex]) <= 0)
                break;

            SwapElements(parentIndex, leftChildIndex);
            parentIndex = leftChildIndex;
        }

        return frontItem;
    }

    public T Peek()
    {
        if (data.Count == 0)
            throw new InvalidOperationException("Queue is empty");
        return data[0];
    }

    private void SwapElements(int index1, int index2)
    {
        (data[index1], data[index2]) = (data[index2], data[index1]);
    }
}