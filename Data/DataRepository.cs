using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Data
{
    public class DataRepository : IDataRepository
    {

        private readonly ApplicationDbContext context;

       public DataRepository(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Channel>> GetChannelListAsync()
        {
            return await EntityFrameworkQueryableExtensions.ToListAsync(context.Channels);

        }

        public async Task AddChannelAsync(Channel channel)
        {

            context.Channels.Add(channel);
            await context.SaveChangesAsync();


        }

        
        public async Task AddTopicAsync(int channelId, Topic topic)
        {
            
            try
            {
                 context.Topics.Add(topic);
                await context.SaveChangesAsync();
            }
            catch(Exception _ex)
            {
                throw;
            }


        }
        public async Task<IEnumerable<Topic>>GetTopicByChannelSlugAsync(string channelSlug)
        {
            return await context.Topics.Where(t => t.Channel.Slug == channelSlug).ToListAsync();
        }

        public async Task<Channel> GetChannelBySlugAsync(string channelSlug)
        {
            return await context.Channels.Include(c=>c.TopicList).FirstOrDefaultAsync(c => c.Slug == channelSlug);
        }


        public  async Task RemoveChannelAsync(string slug, Channel channel)
        {

           Channel ch= await context.Channels.FirstOrDefaultAsync(t=>t.Slug == slug);
            context.Channels.Remove(ch);
           // await context.Update(ch);
            // await context.Channels.Remove(channel);
            await context.SaveChangesAsync();           
            
        }

        public async Task EditChannelAsync(string slug)
        {

            Channel ch = await context.Channels.FirstOrDefaultAsync(t => t.Slug == slug);
            context.Channels.Attach(ch).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }

        }

        private bool ChannelExists(string slug)
        {
            return context.Channels.Any(e => e.Slug == slug);
        }
        public async Task<Topic> GetTopicBySlugAsync(string topicSlug)
        {
            return await context.Topics.Include(c => c.Posts).FirstOrDefaultAsync(c => c.Slug == topicSlug);
        }

        public async Task AddPostAsync(int topicId, Post post)
        {
           // post.Published = DateTime.Now;

            try
            {
                context.Posts.Add(post);
                await context.SaveChangesAsync();
            }
            catch (Exception _ex)
            {
                throw;
            }
          
        }

        public async Task EditPostAsync(string slug, Post post)
        {
            Post p= await context.Posts.FirstOrDefaultAsync(t => t.Slug == slug);

            p.Title = post.Title;
            p.Body = post.Body;
            p.Description = post.Description;
            
            context.Posts.Attach(p).State = EntityState.Modified;
            context.Posts.Update(p);

            try
            {
                await context.SaveChangesAsync();
            p.LastEditedOn = DateTime.Now;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
               
            }
           
        }

        public async Task<Post> GetPostAsync(string slug)
        {
            
            return await context.Posts.Include(r => r.Comments)
                .Include(p=>p.Topic)
                .FirstOrDefaultAsync(r => r.Slug == slug);
        }

        public async Task RemovePostAsync(string slug)
        {

            Post p = await context.Posts.FirstOrDefaultAsync(t => t.Slug == slug);
            context.Posts.Remove(p);
            // await context.Update(ch);
            // await context.Channels.Remove(channel);
            await context.SaveChangesAsync();
            

        }


        public async Task AddCommentAsync(Comment comment)
        {
            
            context.Comments.Add(comment);
            await context.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(Comment comment)
        {
            comment.Deleted = true;
            //comment.DeletedOn = DateTime.Now;
            context.Update(comment);
            await context.SaveChangesAsync();
        }

    }    


    //    public async Task<Comment> GetCommentAsync(int commentID)
    //    {
    //        return await context.Comments.Include(r => r.ParentPost).FirstOrDefaultAsync(r => r.ID == commentID);
    //    }

        //public async Task<IEnumerable<Comment>> GetCommentAsync(int PostID)
        //{
        //    var commentlist = await EntityFrameworkQueryableExtensions.ToListAsync(context.Comments);
        //    return (IEnumerable<Comment>)commentlist.Find(x => x.PostID.Equals(PostID));
        //}
    }



