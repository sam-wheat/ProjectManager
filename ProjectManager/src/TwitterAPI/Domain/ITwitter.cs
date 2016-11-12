using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterAPI.Model;

namespace TwitterAPI.Domain
{
    public interface ITwitter : IDisposable
    {
        void SendTweet(Tweet tweet);
    }
}
