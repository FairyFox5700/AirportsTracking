using System;

namespace Structures
{
    public class AVLNode<T>
    {
        public AVLNode<T> parent;
        public AVLNode<T> left;
        public AVLNode<T> right;
        public T data;
        public int height = 1;
    }

    public class AVLTree<T> where T : IComparable<T>
    {
        private AVLNode<T> node = new AVLNode<T>();
        private int count = 0;

        private bool isLeftChild(AVLNode<T> elem)
        {
            if (Object.ReferenceEquals(elem, elem.parent.right))
                return false;
            return true;
        }

        public void Add(T item)
        {
            if (count == 0)
            {
                count++;
                node.data = item;
                node.parent = node;
                return;
            }
            AVLNode<T> curr_node = this.node;
            while (true)
            {
                if (item.CompareTo(curr_node.data) < 0)
                {
                    if (curr_node.left == null)
                    {
                        curr_node.left = new AVLNode<T>();
                        curr_node.left.data = item;
                        curr_node.left.parent = curr_node;
                        count++;
                        fullBalance(curr_node);
                        break;
                    }
                    else
                        curr_node = curr_node.left;
                }
                else if (item.CompareTo(curr_node.data) > 0)
                {
                    if (curr_node.right == null)
                    {
                        curr_node.right = new AVLNode<T>();
                        curr_node.right.data = item;
                        curr_node.right.parent = curr_node;
                        count++;
                        fullBalance(curr_node);
                        break;
                    }
                    else throw new ArgumentException("Index exists");
                        curr_node = curr_node.right;
                }
            }
        }
        public bool search(T item)
        {
            AVLNode<T> curr_node = node;
            while (true)
            {
                if (curr_node == null)
                {
                    return false;
                }
                else if (item.CompareTo(curr_node.data) == 0)
                {
                    return true;
                }
                else if (item.CompareTo(curr_node.data) < 0)
                {
                    curr_node = curr_node.left;
                }
                else if (item.CompareTo(curr_node.data) > 0)
                {
                    curr_node = curr_node.right;
                }
            }
        }
        public void remove(T item)
        {
            AVLNode<T> curr_node = node;
            while (true)
            {
                if (item.CompareTo(curr_node.data) == 0)
                {
                    bool isLeftChild = true;
                    if (Object.ReferenceEquals(curr_node, curr_node.parent.right))
                        isLeftChild = false;

                    if (curr_node.left == null && curr_node.right == null)
                    {
                        if (isLeftChild)
                            curr_node.parent.left = null;
                        else
                            curr_node.parent.right = null;
                        fullBalance(curr_node);
                        count--;
                        return;
                    }
                    else if (curr_node.left == null && curr_node.right != null)
                    {
                        if (isLeftChild)
                            curr_node.parent.left = curr_node.right;
                        else
                            curr_node.parent.right = curr_node.right;
                        fullBalance(curr_node);
                        count--;
                        return;
                    }
                    else if (curr_node.left != null && curr_node.right == null)
                    {
                        if (isLeftChild)
                            curr_node.parent.left = curr_node.left;
                        else
                            curr_node.parent.right = curr_node.left;
                        fullBalance(curr_node);
                        count--;
                        return;
                    }
                    else if (curr_node.left != null && curr_node.right != null)
                    {
                        AVLNode<T> temp = curr_node.right;
                        while (temp.left != null)
                        {
                            temp = temp.left;
                        }
                        curr_node.data = temp.data;
                        if (temp.right != null)
                            if (Object.ReferenceEquals(temp, temp.parent.left))
                                temp.parent.left = temp.right;
                            else
                                temp.parent.right = temp.right;
                        else
                            if (Object.ReferenceEquals(temp, temp.parent.left))
                            temp.parent.left = null;
                        else
                            temp.parent.right = null;
                        fullBalance(curr_node);
                        count--;
                        return;
                    }
                }
                else if (curr_node == null)
                {
                    return;
                }
                else if (item.CompareTo(curr_node.data) < 0)
                {
                    curr_node = curr_node.left;
                }
                else if (item.CompareTo(curr_node.data) > 0)
                    curr_node = curr_node.right;
            }
        }

        private int height(AVLNode<T> elem)
        {
            if (elem != null)
                return elem.height;
            else
                return 0;
        }

        private int bfactor(AVLNode<T> elem)
        {
            return height(elem.right) - height(elem.left);
        }

        private void fixheight(AVLNode<T> elem)
        {
            int hl = height(elem.left);
            int hr = height(elem.right);
            elem.height = (hl > hr ? hl : hr) + 1;
        }

        private AVLNode<T> balance(AVLNode<T> elem)
        {
            fixheight(elem);
            if (bfactor(elem) == 2)
            {
                if (bfactor(elem.right) < 0)
                    elem.right = rotateRight(elem.right);
                return rotateLeft(elem);
            }
            if (bfactor(elem) == -2)
            {
                if (bfactor(elem.left) > 0)
                    elem.left = rotateLeft(elem.left);
                return rotateRight(elem);
            }
            return elem;
        }

        private void fullBalance(AVLNode<T> elem)
        {
            AVLNode<T> curr_elem = elem;
            while (true)
            {
                if (curr_elem == curr_elem.parent)
                {
                    node = balance(curr_elem);
                    break;
                }
                if (isLeftChild(curr_elem))
                    curr_elem.parent.left = balance(curr_elem);
                else
                    curr_elem.parent.right = balance(curr_elem);
                curr_elem = curr_elem.parent;
            }
        }

        public AVLNode<T> rotateRight(AVLNode<T> elem)
        {
            AVLNode<T> second = elem.left;
            elem.left = second.right;
            if (second.right != null)
                second.right.parent = elem;
            second.right = elem;
            if (elem.parent != elem)
                second.parent = elem.parent;
            else
                second.parent = second;
            elem.parent = second;
            fixheight(elem);
            fixheight(second);
            return second;
        }
        public AVLNode<T> rotateLeft(AVLNode<T> elem)
        {
            AVLNode<T> second = elem.right;
            elem.right = second.left;
            if (second.left != null)
                second.left.parent = elem;
            second.left = elem;
            if (elem.parent != elem)
                second.parent = elem.parent;
            else
                second.parent = second;
            elem.parent = second;
            fixheight(elem);
            fixheight(second);
            return second;
        }
    }
}
