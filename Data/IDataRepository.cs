using System.Collections.Generic;
using System.Threading.Tasks;

namespace Final.Data
{
    public interface IDataRepository
    {
        Task AddChannelAsync(Channel channel);
        Task<IEnumerable<Channel>> GetChannelListAsync();
       
        Task AddTopicAsync(int channelId, Topic topics);
        Task<Channel> GetChannelBySlugAsync(string channelSlug);
        Task<IEnumerable<Topic>> GetTopicByChannelSlugAsync(string channelSlug);

        Task<Topic> GetTopicBySlugAsync(string channelSlug);
        Task AddPostAsync(int topicId, Post post);

        Task RemoveChannelAsync(string topicSlug, Channel channel);

        Task EditChannelAsync(string slug);

        Task<Post> GetPostAsync(string slug);
    }
}