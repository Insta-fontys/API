﻿using DataAccesLibrary.DataAccess;
using DataAccesLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class UserService
    {
        private readonly DatabaseContext _context;

        public UserService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Creator> GetCreatorByUserName(string username)
        {
            return await _context.Creators.Where(i => i.Username == username).FirstOrDefaultAsync();
        }
    }
}