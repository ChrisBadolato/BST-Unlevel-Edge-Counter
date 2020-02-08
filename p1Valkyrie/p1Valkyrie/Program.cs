using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p1Valkyrie
{
    class Program
    {
        public static class Q3
        {
                //create node objects and vairables used throughout the distance calculations
            public static Node root;
            public static int distanceA = -1;
            public static int distanceB = -1;
            public static int totalDistance = 0;
            public class Node
            {
                public int data;
                public Node left, right;
                public Node(int d)
                {
                    this.data = d;
                    left = right = null;
                }
            }

            public static int BSTdistance(IEnumerable<int> values, int nodeAdata, int nodeBdata)
            {
                int[] array = values.ToArray();
                Node startingRoot = new Node(array[0]);
                    //for each value in the array, send the starting node(8), and the current value.
                for (int i = 0; i <= array.Length - 1; i++)
                {
                    root = BinaryTree.arrayToBST(startingRoot, array[i]);
                }                    
                int distance = BinaryTree.findDistance(array, startingRoot, nodeAdata, nodeBdata);

                return distance;
            }
                //creates binary tree
            public static class BinaryTree
            {
                public static Node arrayToBST(Node root, int data)
                {
                        //if root in null, this is our first node.
                    if (root == null)
                    {
                        root = new Node(data);
                    }
                        //if current data is less than the root.
                    else if (data < root.data)
                    {
                            //if the child to the left of root is null, we can create a new node with the data
                        if (root.left == null)
                        {
                            root.left = new Node(data);
                        }
                            //otherwise we need to traverse down the BST again.
                        else
                        {
                            arrayToBST(root.left, data);
                        }
                    }
                        //otherwise our value is greater than the current root.
                    else if(data > root.data)
                    {
                            //if the right child is null, we can store our value.
                        if (root.right == null)
                        {
                            root.right = new Node(data);
                        }
                            //otherwise traverse further down the BST
                        else
                        {
                            arrayToBST(root.right, data);
                        }
                    }
                    return root;
                }

                    //finds the level of the data value we are searching for
                public static int findLevel(Node root, int valueToFind, int level)
                {
                    // Base Case 
                    if (root == null)
                    {
                        return -1;
                    }
                        // If the value is present at root, or in either subtree return true                    
                    if (root.data == valueToFind)
                    {
                        return level;
                    }
                        //if we didn't find our value, we need to continue to traverse the BST
                    int currentLevel = findLevel(root.left, valueToFind, level + 1);
                    
                    if (currentLevel != -1)
                    {
                        return currentLevel;
                    }
                    else
                    {
                        return findLevel(root.right, valueToFind, level + 1);
                    }
                    
                }
                    //find distnace between nodes
                public static Node findNodesDistance(Node root, int valueA, int valueB, int lvl)
                {
                        //empty tree
                    if (root == null)
                    {
                        return null;
                    }
                        //found our first value, return the node and set the distance level.
                    if (root.data == valueA)
                    {
                        distanceA = lvl;
                        return root;
                    }
                        //found our second value return the node and set the distance level
                    if (root.data == valueB)
                    {
                        distanceB = lvl;
                        return root;
                    }
                        //find the next children nodes in each direction
                    Node leftLowestCommonAncestor = findNodesDistance(root.left, valueA, valueB, lvl + 1);
                    Node rightLowestCommonAncestor = findNodesDistance(root.right, valueA, valueB, lvl + 1);
                        //if neither are null, we can calculate the distance
                    if (leftLowestCommonAncestor != null && rightLowestCommonAncestor != null)
                    {                       
                        totalDistance = (distanceA + distanceB) - 2 * lvl;
                        return root;
                    }
                        //otherwise return empty nodes.                   
                    if(leftLowestCommonAncestor != null)
                    {
                        return leftLowestCommonAncestor;
                    }
                    else
                    {
                        return rightLowestCommonAncestor;
                    }                
                }

                public static int findDistance(int[] array, Node root, int valueA, int valueB)
                {
                    distanceA = -1;
                    distanceB = -1;
                    totalDistance = 0;
                    Node lowestCommonAncestor = findNodesDistance(root, valueA, valueB, 1);
                        // If both are present in the binary tree, return the distance                 
                    if (distanceA != -1 && distanceB != -1)
                    {
                        return totalDistance;
                    }

                        // If valueA is ancestor of valueB, use ValueA as root and find level of valueB in subtree with ValueA as root.                  
                    if (distanceA != -1)
                    {
                        totalDistance = findLevel(lowestCommonAncestor, valueB, 0);
                        return totalDistance;
                    }
                        // If valueB is ancestor of valueA, use ValueB as root and find level of valueA in subtree with ValueB as root. 
                    if (distanceB != -1)
                    {
                        totalDistance = findLevel(lowestCommonAncestor, valueA, 0);
                        return totalDistance;
                    }

                    return -1;
                }
            }

            static void Main(string[] args)
            {
                var values = new List<int>() { 8, 7, 13, 6, 2, 5, 1, 9, 11, 3, 4, 10 };              
                int outputInt;
                outputInt = Q3.BSTdistance(values, 10, 2);              
                Console.WriteLine(outputInt);
                Console.ReadKey();
            }
        }
    }
}
