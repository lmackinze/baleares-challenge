using AutoMapper;

namespace BalearesChallengeApp.Data.Services
{
    public class ServiceBase
    {
        protected readonly BalearesDbContext _context;
        protected readonly IMapper _mapper;
        //protected readonly ILogger _logger;

        public ServiceBase(BalearesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}