using API.Services;
using API.TestServices;
using DataAccesLibrary.DataAccess;
using DataAccesLibrary.Dto;
using DataAccesLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace Application.tests
{
    public class ApplicationTests
    {

        [Fact, Trait("Category", "A")]
        public async Task Create_Fan()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "MyGram").Options;

            using var context = new DatabaseContext(options);
            var _database = new RegisterTestService(context);

            Fan fan = CreateFan();

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

        [Fact, Trait("Category", "A")]
        public async Task Get_Creator_By_Username()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "MyGram").Options;

            using var context = new DatabaseContext(options);
            var _database = new RegisterTestService(context);

            Creator creator = new Creator()
            {
                Username = "TestCreator",
                Email = "TestCreator@gmail.com"
            };

            var result = await _database.PostCreatorAccount(creator);

            UserService userService = new UserService(context);
            var response = await userService.GetCreatorByUserName("TestCreator");

            Assert.NotNull(response);
        }

        [Fact, Trait("Category", "A")]
        public async Task Get_Fan_By_Username()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "MyGram").Options;

            using var context = new DatabaseContext(options);
            var _database = new RegisterTestService(context);
            UserService userService = new UserService(context);

            Fan fan = CreateFan();

            var result = await _database.PostFanAccount(fan);

            var response = await userService.GetFanByUsername("TestFan");

            Assert.NotNull(response);
        }

        [Fact, Trait("Category", "A")]
        public async Task Buy_Tokens()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "MyGram").Options;

            using var context = new DatabaseContext(options);
            var _database = new RegisterTestService(context);
            UserService userService = new UserService(context);
            var _tokenDatabase = new TokenDatabaseService(context, userService);

            await _database.PostFanAccount(CreateFan());
            Fan response = await userService.GetFanById(1);
            Assert.Equal(0, response.Tokens);

            BuyTokensModel buyTokensModel = new BuyTokensModel()
            {
                Amount = 100,
                FanId = 1
            };

            await _tokenDatabase.BuyTokens(buyTokensModel);

            Fan result = await userService.GetFanById(1);
            Assert.Equal(100, result.Tokens);
        }

        [Fact, Trait("Category", "A")]
        public async Task Donate_Tokens()
        {

            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "MyGram").Options;

            using var context = new DatabaseContext(options);
            var _database = new RegisterTestService(context);
            UserService userService = new UserService(context);
            var _tokenDatabase = new TokenDatabaseService(context, userService);

            await _database.PostFanAccount(CreateFan());
            await _database.PostCreatorAccount(CreateCreator());
            Fan response = await userService.GetFanById(1);
            
            BuyTokensModel buyTokensModel = new BuyTokensModel()
            {
                Amount = 100,
                FanId = 1
            };

            await _tokenDatabase.BuyTokens(buyTokensModel);

            DonateTokensModel donateTokensModel = new DonateTokensModel()
            {
                FanId = 1,
                CreatorId = 1,
                Amount = 100
            };
            var result = await _tokenDatabase.DonateTokens(donateTokensModel);
            Assert.True(result);
        }

        private Fan CreateFan()
        {
            Fan fan = new Fan()
            {
                Username = "TestFan",
                Email = "TestFan@gmail.com"
            };
            return fan;
        }

        private Creator CreateCreator()
        {
            Creator creator = new Creator()
            {
                Username = "TestCreator",
                Email = "TestCreator@gmail.com"
            };
            return creator;
        }
    }
}
