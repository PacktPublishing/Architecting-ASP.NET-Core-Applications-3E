using TransformTemplateView.Models;

namespace TransformTemplateView.Services;

public class DefaultCorporationFactory : ICorporationFactory
{
public Corporation Create()
{
    var corporation = new Corporation("My Huge Book Store Company!");
    corporation.Add(CreateEastStore());
    corporation.Add(CreateWestStore());
    return corporation;
}

private IComponent CreateEastStore()
{
    var store = new Store("East Store");
    store.Add(CreateFantasySection());
    store.Add(CreateAdventureSection());
    store.Add(CreateDramaSection());
    return store;
}

private IComponent CreateWestStore()
{
    var store = new Store("West Store");
    store.Add(CreateFictionSection());
    store.Add(CreateFantasySection());
    store.Add(CreateAdventureSection());
    return store;
}

private IComponent CreateFictionSection()
{
    var section = new Section("Fiction");
    section.Add(new Book("Some alien cowboy"));
    section.Add(CreateScienceFictionSection());
    return section;
}

private IComponent CreateScienceFictionSection()
{
    var section = new Section("Science Fiction");
    section.Add(new Book("Some weird adventure in space"));
    section.Add(new Set(
        "Star Wars",
        new Set(
            "Prequel trilogy",
            new Book("Episode I: The Phantom Menace"),
            new Book("Episode II: Attack of the Clones"),
            new Book("Episode III: Revenge of the Sith")
        ),
        new Set(
            "Original trilogy",
            new Book("Episode IV: A New Hope"),
            new Book("Episode V: The Empire Strikes Back"),
            new Book("Episode VI: Return of the Jedi")
        ),
        new Set(
            "Sequel trilogy",
            new Book("Episode VII: The Force Awakens"),
            new Book("Episode VIII: The Last Jedi"),
            new Book("Episode IX: The Rise of Skywalker")
        )
    ));
    return section;
}

private IComponent CreateFantasySection()
{
    var section = new Section("Fantasy");
    section.Add(new Set(
        "A Song of Ice and Fire",
        new Book("A Game of Thrones"),
        new Book("A Clash of Kings"),
        new Book("A Storm of Swords"),
        new Book("A Feast for Crows"),
        new Book("A Dance with Dragons"),
        new Book("The Winds of Winter"),
        new Book("A Dream of Spring")
    ));
    section.Add(new Book("The legend of the dwarven dragon"));
    section.Add(new Book("The epic journey of nobody"));
    section.Add(new Set(
        "The Lord of the Ping",
        new Book("Hello World"),
        new Book("How to intercept HTTP communication 101.2")
    ));
    return section;
}

private IComponent CreateAdventureSection()
{
    var section = new Section("Adventure");
    section.Add(new Book("Gulliver's Travels"));
    section.Add(new Book("Moby-Dick"));
    section.Add(new Book("The Adventures of Huckleberry Finn"));
    return section;
}

private IComponent CreateDramaSection()
{
    var section = new Section("Drama");
    section.Add(new Book("Romeo and Juliet"));
    section.Add(new Book("Hamlet"));
    section.Add(new Book("Macbeth"));
    section.Add(new Book("Othello"));
    return section;
}
}
