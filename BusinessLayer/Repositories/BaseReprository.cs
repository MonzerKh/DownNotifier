using AccessLayer.Data;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repositories
{
    public class BaseReprository
    {
        protected private readonly ApplicationDbContext _context;
        protected private readonly IMapper _mapper;

        public BaseReprository(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
    }
}
