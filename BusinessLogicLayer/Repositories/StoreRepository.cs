﻿using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Repositories
{
    public class StoreRepository : GenericRepository<Store>, IStoreRepository
    {
        private readonly AppDbContext context;
        public StoreRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}