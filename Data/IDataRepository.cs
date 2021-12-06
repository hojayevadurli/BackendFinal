﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Final.Data
{
    public interface IDataRepository
    {
        Task AddChannelAsync(Channel channel);
        Task<IEnumerable<Channel>> GetChannelListAsync();
        Task<IEnumerable<Topics>> GetTopicListAsync(string slug);
        Task AddTopicAsync(int channelID, Topics topic);

    }
}