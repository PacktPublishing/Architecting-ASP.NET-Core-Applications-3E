# Architecting ASP.NET Core Applications, Third Edition

<a href="https://www.packtpub.com/product/architecting-aspnet-core-applications-third-edition/9781805123385"><img src="cover.png?raw=true" alt="An Atypical ASP.NET Core 5 Design Patterns Guide" height="256px" align="right"></a>

This is the code repository for [Architecting ASP.NET Core Applications](https://www.packtpub.com/product/architecting-aspnet-core-applications-third-edition/9781805123385), published by Packt. You can also purchase this book on [Amazon](https://adpg.link/buy8).

**An atypical design patterns guide for .NET 8, C# 12, and beyond**

## What is this book about?

Backend design like you’ve never seen it before – a guide to building SOLID ASP.NET Core web apps that stand the test of time. Featuring more Minimal APIs, more testing, more building blocks, and the modular monolith!

This book covers the following exciting features:

-   Apply the SOLID principles for building flexible and maintainable software
-   Test your apps effectively with automated tests, including black-box testing
-   Enter the path of ASP.NET Core dependency injection mastery
-   Work with GoF design patterns such as strategy, decorator, facade, and composite
-   Design REST APIs using Minimal APIs and MVC
-   Discover layering techniques and the tenets of clean architecture
-   Use feature-oriented techniques as an alternative to layering
-   Explore microservices, CQRS, REPL, vertical slice architecture, and many more patterns

If you feel this book is for you, get your [copy](https://adpg.link/buy8) today!

## Instructions and Navigations

The code is organized into folders. For example, C02 contains the code of Chapter 2.

The code will look like the following:

```csharp
public class InlineDataTest
{
    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(5, 5)]
    public void Should_be_equal(int value1, int value2)
    {
        Assert.Equal(value1, value2);
    }
}
```

**Following is what you need for this book:**

This book is for intermediate-level ASP.NET Core developers who want to improve their C# app code structure. ASP.NET developers who want to modernize their knowledge and enhance their technical architecture skills will also like this book. It’s also a good refresher for those in software design roles with more experience looking to update their knowledge.

A good knowledge of C# programming and a basic understanding of web concepts is necessary to get the most out of this book, though some refreshers are included along the way.

### Software and Hardware List

With the following software and hardware list you can run all code files present in the book (Chapter 1-20).

| Chapter | Software required                         | OS required                        |
| ------- | ----------------------------------------- | ---------------------------------- |
| 1-20    | .NET 8                                    | Windows, Mac OS X, and Linux (Any) |
| 1-20    | ASP.NET Core 8                            | Windows, Mac OS X, and Linux (Any) |
| 1-20    | C# 12                                     | Windows, Mac OS X, and Linux (Any) |
| 1-20    | xUnit                                     | Windows, Mac OS X, and Linux (Any) |
| 1-20    | Multiple other .NET open source libraries | Windows, Mac OS X, and Linux (Any) |

### Introduction

This repository contains the code of _**Architecting ASP.NET Core Applications**: An atypical design patterns guide for .NET 8, C# 12, and beyond_.
It also contains pointers to help you find your way around the repository.
This repo is also there to rectify any mistake that could have been made in the book and missed during reviews.

Please open an issue if you find something missing from the instructions below or the book's instructions.

### UML Diagrams

In the book, we have UML Class diagrams, UML Sequence Diagrams, and some non-UML diagrams.
We assumed that most-developers would know about UML, so we decided not to add pages about it.

The author used [diagrams.net (draw.io)](https://draw.io) to draw the diagrams (which is free and open-source). You can use the [Draw.io extension](https://marketplace.visualstudio.com/items?itemName=hediet.vscode-drawio) for VS Code to open the source diagrams, available in this repository.

The following two articles should help you get started if you don't know UML:

-   [UML: What is Class Diagram?](https://adpg.link/UML1)
-   [UML: What is Sequence Diagram?](https://adpg.link/UML2)

### Getting Started

1. You need a copy of `Architecting ASP.NET Core Applications` to make sense of the code projects as many projects start with bad code and get refactored into better ones.
    - You can find [Architecting ASP.NET Core Applications](https://adpg.link/buy8) on Amazon.
1. You need an IDE/Text Editor like Visual Studio or Visual Studio Code, but you could do without (not recommended).
    - [Download Visual Studio](https://adpg.link/VS)
    - [Download Visual Studio Code](https://adpg.link/VSCode)
1. You need .NET 8 SDK. If you installed Visual Studio, you should be fine. Otherwise, here is the link:
    - [Download .NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

With that in place, you should be good to go!

### Build and Test

All projects and solutions can be built using the .NET CLI or Visual Studio.
You can find the most useful commands in the introduction chapter, under _**Running and building your program**_ or online.

The commands you will need the most are:

-   `dotnet new {INSERT_TEMPLATE_HERE}` to create a new project or solution. For example:
    -   `dotnet new web`
    -   `dotnet new sln`
    -   `dotnet new xunit`
-   `dotnet build` to build a project or solution
-   `dotnet run` to run an application
-   `dotnet test` to run one or more test project.

# Content/Code

Throughout the book, there are many projects, and many are copies with a little update done to them, so they may look a lot alike.
If you find something missing, erroneous, or hard to find, please open an issue and let us know, so we can mitigate the issues.

# Contribute

Please open an issue if you find some missing docs, errors in the source code, or a divergence between the book and the source code.

For more information, check out the [Code of conduct](CODE_OF_CONDUCT.md).

# Get to Know the Author

**Carl-Hugo Marcotte** is a software craftsman who has developed digital products professionally since 2005, while his coding journey started around 1989 for fun. He has a bachelor’s degree in computer science.

He has acquired a solid background in software architecture and expertise in ASP.NET Core through creating a wide range of web and cloud applications, from custom e-commerce websites to enterprise applications. He served many customers as an independent consultant, taught programming, and is now a Principal Architect at Export Development Canada.

Passionate about C#, ASP.NET Core, AI, automation, and Cloud computing, he fosters collaboration and the open-source ethos, sharing his expertise with the tech community.
