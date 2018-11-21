namespace LeetCode.DataModel
{
    public static class ArrayExtension
    {
        public static ListNode CreateLinkedListFromArray(this int[] a)
        {
            if (a == null || a.Length < 1)
            {
                return null;
            }

            var head = new ListNode(a[0]);
            var curr = head;

            for (var i = 1; i < a.Length; i++)
            {
                curr.next = new ListNode(a[i]);
                curr = curr.next;
            }

            return head;
        }

        public static TreeNode CreateTreeFromArray(this int[] a)
        {
            if (a == null || a.Length < 1)
            {
                return null;
            }

            return InsertLevelOrder(a, new TreeNode(0), 0);
        }

        // Function to insert nodes in level order 
        public static TreeNode InsertLevelOrder(int[] arr, TreeNode root, int i)
        {
            // Base case for recursion 
            if (i < arr.Length)
            {
                var temp = new TreeNode(arr[i]);
                root = temp;

                // insert left child 
                root.left = InsertLevelOrder(arr, root.left, 2 * i + 1);

                // insert right child 
                root.right = InsertLevelOrder(arr, root.right, 2 * i + 2);
            }

            return root;
        }
    }
}
