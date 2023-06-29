using DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {


        public MyContext databaseContext { get; }

        public IArtistRepository ArtistRepository { get; }

        public UnitOfWork(
           MyContext databaseContext,
           IArtistRepository ArtistRepository
           )
        {
            this.databaseContext = databaseContext;
            this.ArtistRepository = ArtistRepository;

        }
        public async Task SaveChangesAsync()
        {
            await databaseContext.SaveChangesAsync();
        }
    }
}
