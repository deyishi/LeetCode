﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode
{
    public class TwoPointersSet
    {

        [Test]
        public void Test()
        {
            var c = new []
            {
                'h', 'e', 'l', 'l', 'o'
            };
            ReverseString(c);
        }

        /// <summary>
        /// 0.Loop through s.length.
        /// 1.Find a qualified result starting i and j (remember j), j stops at j+1 have duplicate.
        /// 2.Record result (j-i) using Math.Max(result, j-i) for each i.
        /// 3.Increment i and remove s[i] in the map, the result won't count due to Math.Max() during the time while j+1 is still the duplicate.
        /// 4.I is incremented to a point where j+1 is no longer duplicate, then increment j to reach another result. Check result.length at this i.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int LengthOfLongestSubstring(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }

            var result = int.MinValue;
            var map = new int[256];
            var j = 0;
            for (var i = 0; i < s.Length; i++)
            {
                while (j < s.Length && map[s[j]] < 1)
                {
                    map[s[j]]++;
                    j++;
                }

                result = Math.Max(result, j - i);
                map[s[i]]--;
            }

            return result;
        }


        [Test]
        public void MinWindow()
        {
            var s = "ADOBECODEBANC";
            var t = "ABC";

            var r = MinWindow(s, t);
        }


        /// <summary>
        /// Two char[] array, one for current string char (start empty), one for target string char (scan target string).
        /// Two counters C and K, one for unique char from target string found in current substring (when current string char map[char] == targetmap[char], increase count), one for unique char found in target string (ex. Target ABBC, unique 3).
        /// Loop through string and increment or decrement two counter and left and right point for result substring index.
        /// At each i, if  two counters are equal, there is a window found for this i and record that to result.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public string MinWindow(string s, string t)
        {
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }

            int[] tMap = new int[256];
            int l = 0;
            int r = 0;
            int len = s.Length;
            int tCharCount = 0;
            int wCharCount = 0;
            foreach (char c in t)
            {
                tMap[c]++;
                if (tMap[c] == 1)
                {
                    tCharCount++;
                }
            }

            int[] wMap = new int[256];

            int resultStart = -1;
            int resultEnd = -1;
            while (l < len)
            {
                // Create current window
                while (r < len && wCharCount < tCharCount)
                {
                    wMap[s[r]]++;
                    if (wMap[s[r]] == tMap[s[r]])
                    {
                        wCharCount++;
                    }
                    r++;
                }

                // Check if current window meet condition and update result
                if (wCharCount == tCharCount)
                {
                    if (resultEnd == -1 || r - l < resultEnd - resultStart)
                    {
                        resultStart = l;
                        resultEnd = r;
                    }
                }

                // Move to next window
                wMap[s[l]]--;
                if (wMap[s[l]] == tMap[s[l]] - 1)
                {
                    wCharCount--;
                }

                l++;
            }

            if (resultEnd == -1)
            {
                return "";
            }

            return s.Substring(resultStart, resultEnd - resultStart);
        }

        //259. 3Sum Smaller
        //Sort array.
        //Loop through array, for each number user two pointers to find a pair that adds to current number is less than target.
        //Anything from the pair end to the pair start is also less than target.
        public int ThreeSumSmaller(int[] nums, int target)
        {
            var result = 0;
            if (nums == null || nums.Length < 3)
            {
                return result;
            }

            Array.Sort(nums);
            var n = nums.Length;
            for (var i = 0; i < n - 2; i++)
            {

                var l = i + 1;
                var r = n - 1;
                while (l < r)
                {
                    if (nums[l] + nums[r] + nums[i] < target)
                    {
                        result += r - l;
                        l++;
                    }
                    else
                    {
                        r--;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Two pointers need to track sum and difference.
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public int ThreeSumClosest(int[] nums, int target)
        {
            if (nums == null || nums.Length < 3)
            {
                return 0;
            }

            var diff = int.MaxValue;
            var result = 0;
            Array.Sort(nums);
            var n = nums.Length;
            for (var i = 0; i < n - 2; i++)
            {

                var l = i + 1;
                var r = n - 1;
                while (l < r)
                {
                    var currSum = nums[l] + nums[r] + nums[i];

                    var currDiff = Math.Abs(currSum - target);
                    if (diff > currDiff)
                    {
                        diff = currDiff;
                        result = currSum;
                    }
                    if (currSum < target)
                    {
                        l++;
                    }
                    else if (currSum > target)
                    {
                        r--;
                    }
                    else
                    {
                        return result;
                    }
                }
            }

            return result;
        }

        //31. Partition Array
        //Two pointers:
        //i find the index where n[i] is large or equal k
        //j find the index where n[j] is less than k
        //swap
        //After the loop everything less than k will be at left of i.everything greater than k will be at right of i.
        public int PartitionArray(int[] nums, int k)
        {
            if (nums == null || nums.Length == 0)
            {
                return 0;
            }

            int l = 0;
            int r = nums.Length - 1;
            while (l <= r)
            {
                //Find one doesn't belong to left.
                while (l <= r && nums[l] < k)
                {
                    l++;
                }

                //Find the one doesn't belong to right among the ones doesn't belong to left..
                while (l <= r && nums[r] >= k)
                {
                    r--;
                }

                if (l <= r)
                {
                    int temp = nums[l];
                    nums[l] = nums[r];
                    nums[r] = temp;
                    l++;
                    r--;
                }
            }

            return l;
        }

        //42. Trapping Rain Water
        //    Two pointer to track left and right position.
        //    Two variable to track current position's max left and max right height.
        //    Update max left height and max right height when pointer moves, if left is shorter than the right, we know the water level for the left pointer.Otherwise, we know the water level for the right pointer.To calculate water level, use max left or right minutes current position height. After calculation, increment pointer.
        public int Trap(int[] height)
        {
            if (height == null || height.Length < 3)
            {
                return 0;
            }

            int left = 0;
            int right = height.Length - 1;
            int leftMax = 0;
            int rightMax = 0;
            int result = 0;
            while (left < right)
            {
                leftMax = Math.Max(leftMax, height[left]);
                rightMax = Math.Max(rightMax, height[right]);
                if (left < rightMax)
                {
                    result += leftMax - height[left];
                    left++;
                }
                else
                {
                    result += rightMax - height[right];
                    right--;
                }
            }

            return result;

        }

        public void SortColors(int[] nums)
        {
            // Two pointers l, r to track where to add 0 and 2. l = 0, r = nums.Length -1
            // While loop go through numbers using i.
            // At n[i]:
            // 0 swap n[i] with n[l] and i++ l++.
            // 1 i++ no action, all 1s will end up in the mid between 1 and 2.
            // 2 swap n[i] with n[r] and r++, no i++ since we want to check the number swapped to i again incase it is 0.
            // Loop ends when i <= r, since everything after r should be sorted.

            int i = 0;
            int left = 0;
            int right = nums.Length - 1;

            while (i <= right)
            {

                if (nums[i] == 0)
                {
                    Swap(nums, i, left);
                    left++;
                    i++;
                }
                else if (nums[i] == 1)
                {
                    i++;
                }
                else
                {
                    Swap(nums, i, right);
                    right--;
                }
            }
        }

        public void SortKColors(int[] nums, int k)
        {
            if (nums == null || nums.Length == 0 || k == 1)
            {
                return;
            }

            SortKColorsHelper(nums, 0, nums.Length - 1, 1, k);
        }

        public void SortKColorsHelper(int[] nums, int start, int end, int colorFrom, int colorTo)
        {
            if (colorFrom == colorTo)
            {
                return;
            }

            if (start >= end)
            {
                return;
            }

            int l = start;
            int r = end;
            int midColor = (colorFrom + colorTo) / 2;
            int i = l;
            while (i <= r)
            {
                if (nums[i] < midColor)
                {
                    Swap(nums, l++, i++);
                }
                else if (nums[i] > midColor)
                {
                    Swap(nums, i, r--);
                }
                else
                {
                    i++;
                }
            }

            SortKColorsHelper(nums, start, l - 1, colorFrom, midColor - 1);
            SortKColorsHelper(nums, r, end, midColor + 1, colorTo);
        }


        public void Swap(int[] nums, int i, int j)
        {
            var temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }

        public IList<int> FindSubstring(string s, string[] words)
        {
            var result = new List<int>();
            if (string.IsNullOrEmpty(s) || words == null || words.Length == 0)
            {
                return result;
            }
            // Dict<string,int> to count word occurrence in target.Target word count. Window word count.
            int tWordsCount = words.Length;
            int tWordLength = words[0].Length;
            int windowLength = tWordLength * tWordsCount;
            if (s.Length < windowLength)
            {
                return result;
            }

            Dictionary<string, int> found = new Dictionary<string, int>();
            Dictionary<string, int> target = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!target.ContainsKey(word))
                {
                    target.Add(word, 1);
                }
                else
                {
                    target[word]++;
                }
            }

            int len = s.Length;
            for (int i = 0; i <= len - windowLength; i++)
            {
                int currentWindowWordCount;
                found.Clear();
                for (currentWindowWordCount = 0; currentWindowWordCount < tWordsCount; currentWindowWordCount++)
                {
                    int start = i + currentWindowWordCount * tWordLength;
                    string word = s.Substring(start, tWordLength);

                    // Current window doesn't have targeted words.
                    if (!target.ContainsKey(word))
                    {
                        break;
                    }

                    // Update current window word dictionary.
                    if (!found.ContainsKey(word))
                    {
                        found.Add(word, 1);
                    }
                    else
                    {
                        found[word]++;
                    }

                    // Compare current window word dictionary with target word dictionary.
                    if (found[word] > target[word])
                    {
                        break;
                    }
                }

                if (currentWindowWordCount == tWordsCount)
                {
                    result.Add(i);
                }
            }

            return result;
        }
        public int[] MaxSlidingWindow(int[] nums, int k)
        {
            if (nums == null || nums.Length == 0)
            {
                return new int[0];
            }

            List<int> list = new List<int>();
            int l = 0;
            int r = 0;
            int len = nums.Length;
            int[] result = new int[len - k + 1];
            int index = 0;
            while (l < len)
            {
                while (list.Count > 0 && list[0] < l - k + 1)
                {
                    list.RemoveAt(0);
                }

                while (list.Count > 0 && nums[list.Last()] < nums[l])
                {
                    list.RemoveAt(list.Count - 1);
                }

                list.Add(l);

                if (l >= k - 1)
                {
                    result[index++] = nums[list[0]];
                }

                l++;
            }

            return result;
        }

        public int LengthOfLongestSubstringTwoDistinct(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }

            int distinct = 0;
            int l = 0;
            int r = 0;
            int len = s.Length;
            int[] map = new int[256];
            int result = 0;
            while (r < len)
            {
                if (map[s[r]] == 0)
                {
                    distinct++;
                }

                map[s[r]]++;

                while (distinct > 2)
                {
                    map[s[l]]--;
                    if (map[s[l]] == 0)
                    {
                        distinct--;
                    }
                    l++;
                }
                r++;
                result = Math.Max(result, r - l);
            }

            return result;
        }
        /// <summary>
        /// Char counter
        /// Loop through s and set right index and update counter, at every right index check counter if this (l to r) went over, if so reduce l and update counter.
        /// Record result.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public int LengthOfLongestSubstringKDistinct(string s, int k)
        {
            if (string.IsNullOrEmpty(s) || k == 0)
            {
                return 0;
            }

            int[] map = new int[256];
            int r = 0;
            int l = 0;
            int len = s.Length;
            int distinct = 0;
            int result = 0;
            while (r < len)
            {
                if (map[s[r]] == 0)
                {
                    distinct++;
                }

                map[s[r]]++;

                while (distinct > k)
                {

                    map[s[l]]--;
                    if (map[s[l]] == 0)
                    {
                        distinct--;
                    }
                    l++;
                }
                r++;
                result = Math.Max(result, r - l);
            }
            return result;
        }

        public IList<int> FindAnagrams(string s, string p)
        {
            List<int> result = new List<int>();
            if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(p))
            {
                return result;
            }

            int l = 0;
            int r = 0;
            int len = s.Length;

            int[] map = new int[256];
            int[] window = new int[256];
            int anagramLength = p.Length;
            foreach (var c in p)
            {
                map[c]++;
            }
            while (l < len)
            {
                while (r < len && r < l + anagramLength)
                {
                    if (window[s[r]] > map[s[r]] - 1)
                    {
                        break;
                    }

                    window[s[r]]++;
                    r++;
                }

                if (r - l == anagramLength)
                {
                    result.Add(l);
                }

                window[s[l]]--;
                l++;
            }

            return result;
        }

        public bool CheckInclusion(string s1, string s2)
        {
            // String permutation is a anagram. Need to check all the windows in s2 contains the chars in s1 regardless of order.
            if (string.IsNullOrEmpty(s1))
            {
                return false;
            }

            if (string.IsNullOrEmpty(s2))
            {
                return true;
            }

            // s2 only contain a-z 26 letters.
            int[] map = new int[26];
            int[] window = new int[26];
            foreach (var c in s1)
            {
                map[c - 'a']++;
            }

            int l = 0; // window left index
            int r = 0; // window right index
            int len = s2.Length;
            int windowLen = s1.Length;
            while (l < len)
            {
                while (r < len && r < l + windowLen)
                {
                    // Current window has extra char.
                    if (window[s2[r] - 'a'] > map[s2[r] - 'a'] - 1)
                    {
                        break;
                    }
                    window[s2[r] - 'a']++;
                    r++;
                }

                if (r - l == windowLen)
                {
                    return true;
                }

                window[s2[l] - 'a']--;
                l++;
            }

            return false;
        }

        public bool BackspaceCompare(string s, string t)
        {
            int i = s.Length - 1;
            int j = t.Length - 1;
            int skipS = 0;
            int skipT = 0;

            while (i >= 0 || j >= 0)
            {
                // Find s after remove last back space and see a char.
                while (i >= 0)
                {
                    if (s[i] == '#')
                    {
                        skipS++;
                        i--;
                    }
                    else if (skipS > 0)
                    {
                        skipS--;
                        i--;
                    }
                    else
                    {
                        break;
                    }
                }

                while (j >= 0)
                {
                    if (t[j] == '#')
                    {
                        skipT++;
                        j--;
                    }
                    else if (skipT > 0)
                    {
                        skipT--;
                        j--;
                    }
                    else
                    {
                        break;
                    }
                }


                // -1 here means both are empty.
                if (i >= 0 && j >= 0 && s[i] != t[j])
                {
                    return false;
                }
                // They both run out.
                if (i == -1 ^ j == -1)
                {
                    return false;
                }
                i--;
                j--;
            }

            return true;
        }

        public void ReverseString(char[] s)
        {
            int l = 0;
            int r = s.Length - 1;
            while (l < r)
            {
                char temp = s[l];
                s[l] = s[r];
                s[r] = temp;
                l++;
                r--;
            }
        }

        public string ReverseVowels(string s)
        {
            int l = 0;
            int r = s.Length - 1;
            char[] vowels = "aeiouAEIOU".ToCharArray();
            char[] a = s.ToCharArray();
            while (l < r)
            {
                while (!vowels.Contains(a[l]) && l < r)
                {
                    l++;
                }

                while (!vowels.Contains(a[r]) && l < r)
                {
                    r--;
                }

                if (l < r)
                {
                    char temp = a[l];
                    a[l] = a[r];
                    a[r] = temp;
                }

                l++;
                r--;
            }

            return new string(a);
        }

        public string ReverseStr(string s, int k)
        {
            char[] a = s.ToCharArray();
            for (int i = 0; i < s.Length; i += 2 *k)
            {
                int start = i;
                int end = Math.Min(start + k - 1, s.Length - 1); 
                while (start < end)
                {
                    char tmp = a[start];
                    a[start++] = a[end];
                    a[end--] = tmp;
                }
            }

            return new string(a);
        }

        public string ReverseWords(string s)
        {
            var words = s.Split(' ');
            var sb = new StringBuilder();
            foreach (var word in words)
            {
               var reverse = word.ToCharArray();
               Array.Reverse(reverse);
               sb.Append(new string(reverse));
                sb.Append(' ');
            }

            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}
