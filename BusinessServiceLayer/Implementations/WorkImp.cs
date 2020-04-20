using BusinessServiceLayer.AbstractUserInfo;
using DataLayer;
using DataLayer.Entityes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServiceLayer.Implementations
{
    public class WorkImp : UserInfo<UserWorkInfo>
    {
        private DBContext context;
        public WorkImp(DBContext context)
        {
            this.context = context;
        }
        public override async Task Add(UserWorkInfo entity)
        {
            context.UserWorkInformations.Add(entity);
            await context.SaveChangesAsync();
        }

        public override async Task<IEnumerable<UserWorkInfo>> GetAllInfoByDate(DateTime createDate)
        {
            return await context.UserWorkInformations.Where(w => w.CreateDate.Date == createDate.Date).ToListAsync();
        }

        public override async Task Remove(int id)
        {
            var workInfoToRemove = await context.UserWorkInformations.FindAsync(id);
            if (workInfoToRemove != null)
            {
                context.UserWorkInformations.Remove(workInfoToRemove);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public override async Task Update(UserWorkInfo workInfo)
        {
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var workInfoToUpdate = await context.UserWorkInformations.FindAsync(workInfo.Id);
            if (workInfoToUpdate != null)
            {
                context.UserWorkInformations.Update(workInfo);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
