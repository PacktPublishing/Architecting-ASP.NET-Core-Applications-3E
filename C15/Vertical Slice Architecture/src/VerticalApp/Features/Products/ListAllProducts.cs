using AutoMapper;
using MediatR;
using VerticalApp.Data;
using VerticalApp.Models;

namespace VerticalApp.Features.Products;

public class ListAllProducts
{
    public class Command : IRequest<IEnumerable<Result>>
    {
    }

    public record class Result(int Id, string Name, int QuantityInStock);

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, Result>();
        }
    }

    public class Handler : IRequestHandler<Command, IEnumerable<Result>>
    {
        private readonly ProductContext _db;
        private readonly IMapper _mapper;

        public Handler(ProductContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<IEnumerable<Result>> Handle(Command request, CancellationToken cancellationToken)
        {
            var results = _mapper.ProjectTo<Result>(_db.Products);
            return Task.FromResult(results.AsEnumerable());
        }
    }
}
