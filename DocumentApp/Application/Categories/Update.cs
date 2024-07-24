using MediatR;
using Domain;
using Persistence;

namespace Application.Categories
{
    public class Update
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
                var category = await _context.Categories.FindAsync(request.Category.Id);
                category.Title = request.Category.Title ?? category.Title;
                category.Description = request.Category.Description ?? category.Description;
                category.IsDeleted = request.Category.IsDeleted;
                await _context.SaveChangesAsync();
            }
        }
    }
}