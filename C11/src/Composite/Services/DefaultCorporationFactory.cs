#define ADD_BOOKS
#define ADD_SETS
#undef ONLY_FICTION
using Composite.Models;

namespace Composite.Services;

public class DefaultCorporationFactory : ICorporationFactory
{
    public Corporation Create()
    {
        var corporation = new Corporation(
            "Boundless Shelves Corporation",
            "Bosmang Kapawu"
        );
#if !ONLY_FICTION
        corporation.Add(CreateTaleTowersStore());
#endif
        corporation.Add(CreateEpicNexusStore());
        return corporation;
    }

    private IComponent CreateTaleTowersStore()
    {
        var store = new Store(
            "Tale Towers",
            "125 Enchantment Street, Storyville, SV 72845",
            "Malcolm Reynolds"
        );
        store.Add(CreateFantasySection());
        store.Add(CreateAdventureSection());
        store.Add(CreateDramaSection());
        return store;
    }

    private IComponent CreateEpicNexusStore()
    {
        var store = new Store(
            "Epic Nexus",
            "369 Parchment Plaza, Novelty, NV 68123",
            "Ellen Ripley"
        );
        store.Add(CreateFictionSection());
#if !ONLY_FICTION
        store.Add(CreateFantasySection());
        store.Add(CreateAdventureSection());
#endif
        return store;
    }

    private IComponent CreateFictionSection()
    {
        var section = new Section("Fiction");
#if ADD_BOOKS
        section.Add(new Book("Some alien cowboy"));
#endif
        section.Add(CreateScienceFictionSection());
        return section;
    }

    private IComponent CreateScienceFictionSection()
    {
        var section = new Section("Science Fiction");
#if ADD_BOOKS
        section.Add(new Book("Adventures in space"));
#endif
#if ADD_SETS
        section.Add(new Set(
            "Star Wars",
            new Set(
                "Prequel trilogy",
#if ADD_BOOKS
                new Book("Episode I: The Phantom Menace"),
                new Book("Episode II: Attack of the Clones"),
                new Book("Episode III: Revenge of the Sith")
#else
                Array.Empty<Book>()
#endif
            ),
            new Set(
                "Original trilogy",
#if ADD_BOOKS
                new Book("Episode IV: A New Hope"),
                new Book("Episode V: The Empire Strikes Back"),
                new Book("Episode VI: Return of the Jedi")
#else
                Array.Empty<Book>()
#endif
            ),
            new Set(
                "Sequel trilogy",
#if ADD_BOOKS
                new Book("Episode VII: The Force Awakens"),
                new Book("Episode VIII: The Last Jedi"),
                new Book("Episode IX: The Rise of Skywalker")
#else
                Array.Empty<Book>()
#endif
            )
        ));
#endif
        return section;
    }

    private IComponent CreateFantasySection()
    {
        var section = new Section("Fantasy");
#if ADD_SETS
        section.Add(new Set(
            "A Song of Ice and Fire",
#if ADD_BOOKS
            new Book("A Game of Thrones"),
            new Book("A Clash of Kings"),
            new Book("A Storm of Swords"),
            new Book("A Feast for Crows"),
            new Book("A Dance with Dragons"),
            new Book("The Winds of Winter"),
            new Book("A Dream of Spring")
#else
            Array.Empty<Book>()
#endif
        ));
#endif
#if ADD_BOOKS
        section.Add(new Book("The legend of the dwarven dragon"));
        section.Add(new Book("The epic journey of nobody"));
#endif
#if ADD_SETS
        section.Add(new Set(
            "The Lord of the Ping",
#if ADD_BOOKS
            new Book("Hello World"),
            new Book("How to intercept HTTP communication 101.2")
#else
            Array.Empty<Book>()
#endif
        ));
#endif
        return section;
    }

    private IComponent CreateAdventureSection()
    {
        var section = new Section("Adventure");
#if ADD_BOOKS
        section.Add(new Book("Gulliver's Travels"));
        section.Add(new Book("Moby-Dick"));
        section.Add(new Book("The Adventures of Huckleberry Finn"));
#endif
        return section;
    }

    private IComponent CreateDramaSection()
    {
        var section = new Section("Drama");
#if ADD_BOOKS
        section.Add(new Book("Romeo and Juliet"));
        section.Add(new Book("Hamlet"));
        section.Add(new Book("Macbeth"));
        section.Add(new Book("Othello"));
#endif
        return section;
    }
}
