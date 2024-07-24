using Microsoft.EntityFrameworkCore;
using MediatR;
using Domain;
using Persistence;

namespace Application.Categories
{
    public class List
    {
        public class Query : IRequest<List<Category>> { }

        public class Handler : IRequestHandler<Query, List<Category>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Category>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Categories.ToListAsync();
            }
        }
    }
}