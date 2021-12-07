using System.Collections.Generic;
using System.Threading.Tasks;

namespace Final.Data
{
    public interface IDataRepository
    {
        Task AddChannelAsync(Channel channel);
        Task<IEnumerable<Channel>> GetChannelListAsync();
       
        Task AddTopicAsync(int channelId, Topics topics);
        Task<Channel> GetChannelBySlugAsync(string channelSlug);
    }
}