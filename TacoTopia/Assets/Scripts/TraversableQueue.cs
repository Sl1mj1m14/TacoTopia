using UnityEngine;

public class TraversableQueue<T>
{

private class Node 
{

    public T element;
    public Node next;

    public Node (T value, Node node) {

        element = value;
        next = node;
    }

}

    private Node front;
    private Node rear;

    public TraversableQueue() {

        front = null;
        rear = null;
    }

    //Checks whether the queue is empty
    public bool IsEmpty() {
         return front == null;
    }

    //Checks the size of the queue
    public int Size() {
        
        int length = 0;
        Node iterator = front;
        
        while (iterator != null)
        {
            
            length++;
            iterator = iterator.next;
            
        }
        
        return length;
        
    }

    //Adds an element to the queue
    public void Add (T element) {

        if (IsEmpty()) {

            front = new Node(element,null);
            rear = front;
        } else {
            rear.next = new Node(element,null);
            rear = rear.next;
        }
    }

    //Adds an element to the queue at the specified index
    public void Add (int index, T element) {

        if (index < 0 || index > Size()) return;

        if (index == 0)
        {
            
            front = new Node(element, front);
            
            if (rear == null)
            {   
                rear = front;  
            }
            
            return;
        }

        Node pre = front;
        
        for (int i = 1; i <= index - 1; i++)
        {
            pre = pre.next;
        }
        
        pre.next = new Node(element, pre.next);
        
        if (pre.next.next == null)
        {
            rear = pre.next;
        } 
    }

    //Removes the first element from the queue
    public T Remove () {

        if (IsEmpty()) return default(T);

        T element = front.element;
        front = front.next;

        if (front == null) rear = null;

        return element;

    }

    //Removes an element from the queue at the specified index
    public T Remove (int index) {

        if (index < 0 || index > Size()) return default(T);

        if (index == 0) return Remove();

        T element;

        Node pre = front;
        
            for (int i = 1; i <= index - 1; i++)
            {
                pre = pre.next;
            }
            
            element = pre.next.element;
            pre.next = pre.next.next;
            
            if (pre.next == null)
            {
                rear = pre;
            }

        return element;
    }

    //Returns the first element
    public T Peek () {
        return front.element;
    }

    //Returns the element at the specified index
    public T Peek (int index) {

        if (index < 0 || index > Size()) return default(T);

        if (index == 0) return Peek();

        Node pre = front;

        for (int i = 1; i <= index; i++)
            {
                pre = pre.next;
            }
            
        return pre.element;

    }
}
