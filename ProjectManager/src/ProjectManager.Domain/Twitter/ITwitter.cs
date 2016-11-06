using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManager.Model.Twitter;

namespace ProjectManager.Domain.Twitter
{
    public interface ITwitter : IDisposable
    {
        void SendTweet(Tweet tweet);
    }
}
