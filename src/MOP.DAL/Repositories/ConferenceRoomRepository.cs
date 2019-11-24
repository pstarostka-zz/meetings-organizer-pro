using Microsoft.EntityFrameworkCore;
using MOP.DAL.Model;
using MOP.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;
using Dapper;

namespace MOP.DAL.Repositories
{
    public class ConferenceRoomRepository : BaseRepository<ConferenceRoom>, IConferenceRoomRepository
    {

        public ConferenceRoomRepository(EfCoreContext context) : base(context)
        { }

    }
}
