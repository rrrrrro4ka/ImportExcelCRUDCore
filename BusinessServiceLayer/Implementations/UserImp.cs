using BusinessServiceLayer.AbstractUserInfo;
using DataLayer;
using DataLayer.Entityes;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessServiceLayer.Implementations
{
    public sealed class UserImp : UserInfo<User>
    {
        private DBContext context;
        public UserImp(DBContext context)
        {
            this.context = context;
        }
        public override async Task Add(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

        public override async Task Remove(int id)
        {
            var userToDelete = await context.Users.FindAsync(id);
            if (userToDelete != null)
            {
                context.Users.Remove(userToDelete);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
        public override async Task<IEnumerable<User>> GetAllInfoByDate(DateTime createDate)
        {
            return await context.Users.Where(u => u.CreateDate.Date == createDate.Date).ToListAsync();
        }

        public override async Task Update(User user)
        {
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var userToUpdate = await context.Users.FindAsync(user.Id);
            if (userToUpdate != null)
            {
                context.Users.Update(user);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
