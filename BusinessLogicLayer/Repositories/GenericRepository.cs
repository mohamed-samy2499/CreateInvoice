﻿using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Contexts;
using Microsoft.EntityFrameworkCore;


namespace BusinessLogicLayer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext context;

        public GenericRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<int> Add(T item)
        {
            await context.Set<T>().AddAsync(item);
            return await context.SaveChangesAsync();
        }

        public async Task<int> Delete(T item)
        {
            context.Set<T>().Remove(item);
            return await context.SaveChangesAsync();
        }
        public async Task<T> GetById(int? id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        => await context.Set<T>().ToListAsync();


        public async Task<int> Update(T item)
        {
            context.Set<T>().Update(item);
            return await context.SaveChangesAsync();
        }
        public async Task<bool> EnsureEntityExists(int id)
        {
            var result = await context.Set<T>().FindAsync(id);
            if (result == null)
            {
                return false;
            }
            return true;
        }
    }
}
