using API.Services;
using API.TestServices;
using DataAccesLibrary.DataAccess;
using DataAccesLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Application.tests
{
    public class UnitTest1
    {

        [Fact, Trait("Category", "A")]
        public async Task Create_Fan()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "MyGram").Options;

            using var context = new DatabaseContext(options);
            var _database = new RegisterTestService(context);

            Fan fan = new Fan()
            { 
                Username = "TestFan",
                Email = "TestFan@gmail.com"
            };

            var result = await _database.PostFanAccount(fan);
            Assert.True(result);
        }


        [Fact, Trait("Category", "A")]
        public async Task Create_Creator()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "MyGram").Options;

            using var context = new DatabaseContext(options);
            var _database = new RegisterTestService(context);

            Creator creator = new Creator()
            {
                Username = "TestFan",
                Email = "TestFan@gmail.com"
            };

            var result = await _database.PostCreatorAccount(creator);
            Assert.True(result);
        }

        [Fact, Trait("Category", "A")]
        public async Task Create_FollowModel()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "MyGram").Options;
            using var context = new DatabaseContext(options);

            var _database = new FollowDatabaseService(context);

            CreatorFans creatorFans = new CreatorFans()
            {
                FanId = 1,
                CreatorId = 1,
            };

            var result = await _database.PostFollower(creatorFans);

            Assert.True(result);
        }

        [Fact, Trait("Category", "A")]
        public async Task Like_Post()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "MyGram").Options;
            using var context = new DatabaseContext(options);

            var _database = new LikePostDatabaseService(context);

            Post post = new Post()
            {
                Image = "",
                Id = 0,
                Description = "Test",
                Likes = 0
            };

            var result = await _database.PostILiked(post, 1);

            Assert.True(result);
        }

        [Fact, Trait("Category", "A")]
        public async Task CreatePost()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "MyGram").Options;
            using var context = new DatabaseContext(options);

            var _databsae = new PostDatabaseService(context);

            Post post = new Post()
            {
                Image = "",
                Id = 0,
                Description = "Test",
                Likes = 0
            };

            Creator creator = new Creator()
            {
                Username = "Creator",
                Email = "Creator@gmail.com"
            };

            var result = await _databsae.PostPost(post, creator);
            Assert.True(result);
        }

        [Fact, Trait("Category", "A")]
        public async Task React_To_Post()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                            .UseInMemoryDatabase(databaseName: "MyGram").Options;
            using var context = new DatabaseContext(options);

            var _database = new ReactionDatabaseService(context);

            Reaction reaction = new Reaction()
            {
                Message = "nice"
            };

            var result = await _database.PostReaction(reaction, 1, 1);
            Assert.True(result);
        }
    }
}
