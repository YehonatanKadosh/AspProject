using AspProject_DataBase.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AspProject_Services.Timers
{
    public class TimerForUpdatingDB
    {
        Timer timer;
        DBContext Context;
        public TimerForUpdatingDB(DBContext context) // optional
        {
            Context = context;
            init();
        }
        private void init() => timer = new Timer(async (stam) => { await tick(); }, null, 10000 ,30000 );
        
        private async Task tick()
        {
            
            await Task.Run(async () =>
            {
                await Context.Products.Include(p => p.Buyer).Include(p => p.Seler).ForEachAsync((p) =>
                {
                    if (DateTime.Now >= p.LastModified + TimeSpan.FromMinutes(1) && p.State == AspProject_Entities.Enums.ProductState.InCart)
                    {
                        p.State = AspProject_Entities.Enums.ProductState.UnSold;
                        p.Buyer = p.Seler;
                    }
                });
            });
        }
    }
}
