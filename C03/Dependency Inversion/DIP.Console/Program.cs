using DIP.Core;
using DIP.Data.InMemory;

namespace DIP.App;

public class Program
{
    private static BookPresenter presenter = new();

    public static async Task Main(string[] args)
    {
        var isPublic = args?.Length == 0 || args?[0] != "admin";
        Console.WriteLine($"isPublic: {isPublic}");
        if (isPublic)
        {
            await PublicAppAsync();
        }
        else
        {
            await AdminAppAsync();
        }
        Console.ReadLine();
    }

    private static async Task PublicAppAsync()
    {
        var publicService = Composer.CreatePublicService();
        var books = await publicService.FindAllAsync();
        foreach (var book in books)
        {
            presenter.Display(book);
        }
    }

    private static async Task AdminAppAsync()
    {
        var adminService = Composer.CreateAdminService();
        var books = await adminService.FindAllAsync();
        foreach (var book in books)
        {
            presenter.Display(book);
        }
    }

    private static class Composer
    {
        private readonly static BookStore BookStore = new();

        public static AdminService CreateAdminService()
        {
            return new AdminService(
                bookReader: BookStore,
                bookWriter: BookStore
            );
        }

        public static PublicService CreatePublicService()
        {
            return new PublicService(
                bookReader: BookStore
            );
        }
    }
}
