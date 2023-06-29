using BLL.Services.Contracts;
using DAL.Models;
using DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ArtistManager : IArtistManager
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public ArtistManager(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

    }
}
