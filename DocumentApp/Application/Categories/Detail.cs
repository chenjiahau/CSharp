using MediatR;
using Domain;
using Persistence;

namespace Application.Categories
{
    public class Detail
    {
        public class Query : IRequest<Category> 
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Category>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Category> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Categories.FindAsync(request.Id);
            }
        }
    }
}