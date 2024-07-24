using AutoMapper;
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
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var category = await _context.Categories.FindAsync(request.Category.Id);
                _mapper.Map(request.Category, category);
                await _context.SaveChangesAsync();
            }
        }
    }
}