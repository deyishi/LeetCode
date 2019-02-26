using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeetCode.DataModel;
using NUnit.Framework;

namespace LeetCode.Design
{
    public class TwitterTest
    {
        [Test]
        public void Test()
        {
            var twitter = new Twitter();
            twitter.PostTweet(1,1);
            var feeds = twitter.GetNewsFeed(1);
            twitter.Follow(2, 1);

        }
    }

    public class Twitter
    {
        // Fast user search.
        private Dictionary<int, User> userMap;

        private static int _timeStamp;

        private int Limit = 10;
        /** Initialize your data structure here. */
        public Twitter()
        {
            userMap = new Dictionary<int, User>();
            _timeStamp = 0;
        }

        /** Compose a new tweet. */
        public void PostTweet(int userId, int tweetId)
        {
            if (!userMap.ContainsKey(userId))
            {
                userMap.Add(userId, new User(userId));
            }

            userMap[userId].Post(tweetId, _timeStamp);
            _timeStamp++;
        }

        /** Retrieve the 10 most recent tweet ids in the user's news feed. Each item in the news feed must be posted by userMap who the user followed or by the user herself. Tweets must be ordered from most recent to least recent. */
        public IList<int> GetNewsFeed(int userId)
        {
          List<int> res = new List<int>();
          if (!userMap.ContainsKey(userId))
          {
              return res;
          }

          var user = userMap[userId];
          HashSet<int> followed = user.Followed;
          MaxHeap<Tweet> tweets = new MaxHeap<Tweet>();
          foreach (var followedUserId in followed)
          {
              var tweetHead = userMap[followedUserId].TweetHead;
              while (tweetHead != null)
              {
                  tweets.Push(tweetHead);
                  tweetHead = tweetHead.Next;
              }
          }

          while (tweets.Any() && res.Count < Limit)
          {
              var tweet = tweets.Pop();
              res.Add(tweet.Id);
          }
          return res;
        }

        /** Follower follows a followee. If the operation is invalid, it should be a no-op. */
        public void Follow(int followerId, int followeeId)
        {
            if (!userMap.ContainsKey(followerId))
            {
                userMap.Add(followerId, new User(followerId));
            }
            if (!userMap.ContainsKey(followeeId))
            {
                userMap.Add(followeeId, new User(followeeId));
            }
            userMap[followerId].Follow(followeeId);
        }

        /** Follower unfollows a followee. If the operation is invalid, it should be a no-op. */
        public void Unfollow(int followerId, int followeeId)
        {
            if (!userMap.ContainsKey(followerId) || followerId == followeeId)
            {
                return;
            }

            userMap[followerId].Unfollow(followeeId);
        }

    }

    public class Tweet : IComparable<Tweet>
    {
        public Tweet(int id, int time)
        {
            Id = id;
            Time = time;
            Next = null;
        }

        public int Id { get; set; }
        public int Time { get; set; }

        // Use linked list for fast lookup.
        public Tweet Next { get; set; }

        public int CompareTo(Tweet other)
        {
            if (Time == other.Time)
            {
                return 0;
            }

            return Time > other.Time ? 1 : -1;
        }
    }

    public class User
    {
        public User(int id)
        {
            UserId = id;
            Followed = new HashSet<int>();
            Follow(id);
            TweetHead = null;
        }

        public void Follow(int userId)
        {
            Followed.Add(userId);
        }

        public void Unfollow(int userId)
        {
            Followed.Remove(userId);
        }

        public void Post(int tweetId, int time)
        {
            Tweet t = new Tweet(tweetId, time)
            {
                Next = TweetHead
            };
            TweetHead = t;
        }

        public int UserId { get; set; }
        // Use set for fast lookup and remove duplicate.
        public HashSet<int> Followed { get; set; }
        public Tweet TweetHead { get; set; }
    }
}
