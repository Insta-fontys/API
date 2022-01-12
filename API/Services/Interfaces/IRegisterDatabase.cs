using DataAccesLibrary.Dto;
using DataAccesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IRegisterDatabase
    {
        public Task<bool> PostFanAccount(Fan fan);
        public Task<bool> PostCreatorAccount(Creator creator);
        public Task<bool> PostAdminAccount(RegisterModel registerModel);
    }
}
