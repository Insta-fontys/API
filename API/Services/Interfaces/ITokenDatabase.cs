using DataAccesLibrary.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface ITokenDatabase
    {
        public Task<bool> BuyTokens(BuyTokensModel buyTokensModel);
        public Task<bool> DonateTokens(DonateTokensModel donateTokensModel);
    }
}
