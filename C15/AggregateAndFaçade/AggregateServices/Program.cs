using AggregateServices;
using Microsoft.Extensions.DependencyInjection;
using Shared.Mappers;
using Shared.Contracts;
using Shared.Models;

var services = new ServiceCollection();
services
    // Register the mapper bindings
    .AddSingleton<IMapper<InsertProduct, Product>, InsertProductToProductMapper>()
    .AddSingleton<IMapper<Product, ProductSummary>, ProductToProductSummaryMapper>()
    .AddSingleton<IMapper<UpdateProduct, Product>, UpdateProductToProductMapper>()

    // Register aggregate services binding
    .AddSingleton<IProductMappers, ProductMappers>()
;

var serviceProvider = services.BuildServiceProvider();
var mapper = serviceProvider.GetRequiredService<IProductMappers>();

// EntityToDto
var smartphone = new Product("Smartphone", 10, id: 1);
var smartphoneSummary = mapper.EntityToDto.Map(smartphone);
Write("EntityToDto", smartphone, smartphoneSummary);

// InsertDtoToEntity
var insertProductDto = new InsertProduct("Laptop");
var laptop = mapper.InsertDtoToEntity.Map(insertProductDto);
Write("InsertDtoToEntity", insertProductDto, laptop);

// UpdateDtoToEntity
var updateProductDto = new UpdateProduct(Id: 3, Name: "Smartwatch", quantityInStock: 5);
var smartwatch = mapper.UpdateDtoToEntity.Map(updateProductDto);
Write("UpdateDtoToEntity", updateProductDto, smartwatch);

/// <summary>
/// Utility method that formats the console output.
/// </summary>
static void Write(string title, object input, object output)
{
    Console.WriteLine("===[{0}]===", title);
    Console.WriteLine("Input: {0}", input);
    Console.WriteLine("Output: {0}", output);
    Console.WriteLine();
}