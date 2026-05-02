using TodoAPI.Domain.Entities;

namespace TodoAPI.Application.Algorithms
{
    public class LexoRankMergeSort : ISortingStrategy<TodoTask>
    {
        public List<TodoTask> Sort(List<TodoTask> input)
        {
            // Base Case: A list of zero or one elements is already sorted.
            if (input.Count <= 1)
                return input;

            //Division
            int mid = input.Count / 2;
            List<TodoTask> left = new List<TodoTask>();
            List<TodoTask> right = new List<TodoTask>();

            for (int i = 0; i < mid; i++)
                left.Add(input[i]);

            for (int i = mid; i < input.Count; i++)
                right.Add(input[i]);

            // Recursively sort both halves and merge them
            left = Sort(left);
            right = Sort(right);
            return Merge(left, right);
        }

        private List<TodoTask> Merge(List<TodoTask> left, List<TodoTask> right)
        {
            List<TodoTask> result = new List<TodoTask>();
            int i = 0, j = 0;

            //Compare the rank strings and merge in alphabetical order
            while (i < left.Count && j < right.Count)
            {
                if (string.CompareOrdinal(left[i].Rank, right[j].Rank) <= 0)
                {
                    result.Add(left[i]);
                    i++;
                }
                else
                {
                    result.Add(right[j]);
                    j++;
                }
            }

            //Clean up remaining elements in the left list
            while (i < left.Count)
            {
                result.Add(left[i]);
                i++;
            }

            //Clean up remaining elements in the right list 
            while (j < right.Count)
            {
                result.Add(right[j]);
                j++;
            }

            return result;
        }
    }
}