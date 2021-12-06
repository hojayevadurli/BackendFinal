﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Data
{
    public class DataRepository : IDataRepository
    {

        private readonly ApplicationDbContext context;

        //Task<IEnumerable<Post>> IRepository.PostList => throw new NotImplementedException();


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

            context.Channels.Include `;
            await context.SaveChangesAsync();

            Channel = await dbContext.Channels
.Include(p => p.TopicsList)
.FirstOrDefaultAsync(m => m.Slug.ToLower() == slug.ToLower());
        }

        public async Task<IEnumerable<Topics>> GetTopicListAsync(string slug)
        {
            return await context.Topics.Where(m => m.Channel.ChannelId == channelId).ToListAsync();

        }

        public async Task AddTopicAsync(int channelID,Topics topic)
        {

            Channel channel = context.Channels.FirstOrDefault(c => c.ChannelId == channelID);
            topic.Channel = channel;

            context.Topics.Add(topic);
            channel.TopicList.Add(topic);
            context.Update(channel);
            await context.SaveChangesAsync();
        }

        //public async Task AddPostAsync(Post post)
        //{
        //    post.PostedOn = DateTime.Now;
        //    context.Posts.Add(post);
        //    await context.SaveChangesAsync();
        //}

        //public async Task EditPostAsync(Post post)
        //{
        //    post.EditedOn = DateTime.Now;
        //    context.Update(post);
        //    await context.SaveChangesAsync();
        //    //await Task.Run(() => context.Posts. = post);
        //}

        //public async Task<Post> GetPostAsync(int postID)
        //{
        //    //var postlist = await EntityFrameworkQueryableExtensions.ToListAsync(context.Posts);
        //    //return postlist.Find(x=>x.ID.Equals(postID));
        //    return await context.Posts.Include(r => r.Comments)
        //        .Include(p => p.PostCategories)
        //        .ThenInclude(pc => pc.Category)
        //        .FirstOrDefaultAsync(r => r.ID == postID);
        //}

        //public async Task<Category> GetCategoryAsync(int categoryID)
        //{
        //    return await Task.Run(() => context.Categories.Include(c => c.PostCategories).ThenInclude(pc => pc.Post).First(r => r.CategoryId == categoryID));
        //}

        //public async Task AddCommentAsync(Comment comment)
        //{
        //    context.Comments.Add(comment);
        //    await context.SaveChangesAsync();
        //}

        //public async Task DeleteCommentAsync(Comment comment)
        //{
        //    comment.Deleted = true;
        //    comment.DeletedOn = DateTime.Now;
        //    context.Update(comment);
        //    await context.SaveChangesAsync();
        //}

        //public async Task AddCategoryAsync(Category category, int postId)
        //{

        //    var tempCat = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(context.Categories, c => c.CategoryName == category.CategoryName);


        //    if (tempCat == null)
        //    {
        //        tempCat = new Category { CategoryName = category.CategoryName };
        //        context.Categories.Add(tempCat);
        //        await context.SaveChangesAsync();
        //    }

        //    var newPostCategory = new PostCategory()
        //    {
        //        CategoryId = tempCat.CategoryId,
        //        PostId = postId
        //    };

        //    context.PostCategories.Add(newPostCategory);
        //    await context.SaveChangesAsync();

    }


    //public async Task<IEnumerable<Category>> GetCategoriesAsync()
    //    {
    //        return await context.Categories.Include(c => c.PostCategories).ThenInclude(r => r.Post).ThenInclude(r => r.Comments).ToListAsync();
    //    }

    //    public Task<Category> GetPostCategoriesAsync(int postID)
    //    {
    //        throw new NotImplementedException();
    //    }


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



