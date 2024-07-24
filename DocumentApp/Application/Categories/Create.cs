using MediatR;
using Domain;
using Persistence;

namespace Application.Categories
{
    public class Create
    {
        public class Command : IRequest
        {
            public Category Category { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                _context.Categories.Add(request.Category);
                await _context.SaveChangesAsync();
            }
        }
    }
}