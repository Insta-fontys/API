using API.Services.Interfaces;
using DataAccesLibrary.DataAccess;
using DataAccesLibrary.Dto;
using DataAccesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class TokenDatabaseService: ITokenDatabase
    {
        private readonly DatabaseContext _database;
        private readonly UserService userService;

        public TokenDatabaseService(DatabaseContext database, UserService userService)
        {
            _database = database;
            this.userService = userService;
        }

        public async Task<bool> BuyTokens(BuyTokensModel buyTokensModel)
        {
            Fan fan = await userService.GetFanById(buyTokensModel.FanId);
            fan.Tokens += buyTokensModel.Amount;

            try
            {
                _database.Fans.Update(fan);
                await _database.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DonateTokens(DonateTokensModel donateTokensModel)
        {
            Fan fan = await userService.GetFanById(donateTokensModel.FanId);
            if (!HasEnoughTokens(fan, donateTokensModel.Amount))
                return false;

            Creator creator = await userService.GetCreatorById(donateTokensModel.CreatorId);

            fan.Tokens -= donateTokensModel.Amount;
            creator.Tokens += donateTokensModel.Amount;

            try
            {
                    _database.Fans.Update(fan);
                _database.Creators.Update(creator);

                await _database.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool HasEnoughTokens(Fan fan, int tokensAmount)
        {
            return fan.Tokens >= tokensAmount;
        }
    }
}
