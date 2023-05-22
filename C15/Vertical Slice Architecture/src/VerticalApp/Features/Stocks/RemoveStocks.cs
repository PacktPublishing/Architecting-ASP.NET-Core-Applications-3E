using AutoMapper;
using FluentValidation;
using MediatR;
using VerticalApp.Data;
using VerticalApp.Models;

namespace VerticalApp.Features.Stocks;

public class RemoveStocks
{
    public class Command : IRequest<Result>
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }

    public class Result
    {
        public int QuantityInStock { get; set; }
    }

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, Result>();
        }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Amount).GreaterThan(0);
        }
    }

    public class Handler : IRequestHandler<Command, Result>
    {
        private readonly ProductContext _db;
        private readonly IMapper _mapper;

        public Handler(ProductContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var product = await _db.Products.FindAsync(new object[] { request.ProductId }, cancellationToken);
            if (product == null)
            {
                throw new ProductNotFoundException(request.ProductId);
            }
            if (request.Amount > product.QuantityInStock)
            {
                throw new NotEnoughStockException(product.QuantityInStock, request.Amount);
            }

            product.QuantityInStock -= request.Amount;
            await _db.SaveChangesAsync(cancellationToken);

            return _mapper.Map<Result>(product);
        }
    }
}
