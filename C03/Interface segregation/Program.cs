using InterfaceSegregation.After;

var publicProductReader = new PublicProductReader();
var privateProductRepository = new PrivateProductRepository();

ReadProducts(publicProductReader);
ReadProducts(privateProductRepository);

// Error: Cannot convert from PublicProductReader to IProductWriter
// WriteProducts(publicProductReader); // Invalid
WriteProducts(privateProductRepository);

ReadAndWriteProducts(privateProductRepository, privateProductRepository);
ReadAndWriteProducts(publicProductReader, privateProductRepository);

void ReadProducts(IProductReader productReader)
{
    Console.WriteLine(
        "Reading from {0}.",
        productReader.GetType().Name
    );
}
void WriteProducts(IProductWriter productWriter)
{
    Console.WriteLine(
        "Writing to {0}.",
        productWriter.GetType().Name
    );
}
void ReadAndWriteProducts(IProductReader productReader, IProductWriter productWriter)
{
    Console.WriteLine(
        "Reading from {0} and writing to {1}.",
        productReader.GetType().Name,
        productWriter.GetType().Name
    );
}
