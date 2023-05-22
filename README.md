


# An Atypical ASP.NET Core 6 Design Patterns Guide, Second Edition

<a href="https://www.packtpub.com/product/an-atypical-asp-net-core-6-design-patterns-guide-second-edition/9781803249841"><img src="cover.png?raw=true" alt="An Atypical ASP.NET Core 5 Design Patterns Guide" height="256px" align="right"></a>

This is the code repository for [An Atypical ASP.NET Core 6 Design Patterns Guide](https://www.packtpub.com/product/an-atypical-asp-net-core-6-design-patterns-guide-second-edition/9781803249841), published by Packt.

**A SOLID adventure into architectural principles and design patterns using .NET 6 and C# 10**

## What is this book about?

Thoroughly updated for ASP.NET Core 6, with further coverage of microservices, data contracts, and event-driven architecture, this book gives you the tools to build and glue reliable components together to improve your programmatic masterpiece.

This book covers the following exciting features:

- Apply the SOLID principles for building flexible and maintainable software
- Get to grasp .NET dependency Injection
- Work with GoF design patterns such as strategy, decorator, façade, and composite
- Explore the MVC patterns for designing web APIs and web applications using Razor
- Discover layering techniques and tenets of clean architecture
- Become familiar with CQRS and vertical slice architecture as an alternate to layering
- Understand microservices and when they can benefit your applications
- Build an ASP.NET user interfaces from server-side to client-side Blazor

If you feel this book is for you, get your [copy](https://www.amazon.com/Atypical-ASP-NET-Design-Patterns-Guide/dp/1803249846) today!

## Instructions and Navigations

All of the code is organized into folders. For example, Chapter02.

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

This design patterns book is for beginners as well as intermediate-level software and web developers with some knowledge of .NET who want to write flexible, maintainable, and robust code for building scalable web applications. Knowledge of C# programming and an understanding of web concepts like HTTP is necessary.

With the following software and hardware list you can run all code files present in the book (Chapter 1-18).

### Software and Hardware List

| Chapter | Software required                         | OS required                        |
| ------- | ----------------------------------------- | ---------------------------------- |
| 1-18    | .NET 6                                    | Windows, Mac OS X, and Linux (Any) |
| 1-18    | ASP.NET Core 6                            | Windows, Mac OS X, and Linux (Any) |
| 1-18    | C# 10                                     | Windows, Mac OS X, and Linux (Any) |
| 1-18    | xUnit                                     | Windows, Mac OS X, and Linux (Any) |
| 1-18    | Multiple other .NET open source libraries | Windows, Mac OS X, and Linux (Any) |

### Introduction

This repository contains the code of `An Atypical ASP.NET Core 6 Design Patterns Guide`.
It also contains pointers to help you find your way around the repository.
This repo is also there to rectify any mistake that could have been made in the book and missed during reviews.

Please open an issue if you find something missing from the instructions below or the book's instructions.

### UML Diagrams

In the book, we have UML Class diagrams, UML Sequence Diagrams, and some non-UML diagrams.
We assumed that most-developers would know about UML, so we decided not to add pages about it.

The author used [diagrams.net (draw.io)](https://draw.io) to draw the diagrams (which is free and open-source).

The following two articles should help you get started if you don't know UML:

-   [UML class-diagrams](https://net5.link/UML1)
-   [UML sequence-diagrams](https://net5.link/UML2)

### Getting Started

1. You need a copy of `An Atypical ASP.NET Core 6 Design Patterns Guide` to make sense of the code projects as many projects start with bad code and get refactored into better ones.

    - You can find [An Atypical ASP.NET Core 6 Design Patterns Guide](https://www.amazon.com/Atypical-ASP-NET-Design-Patterns-Guide/dp/1803249846) on Amazon.com

1. You need an IDE/Text Editor like Visual Studio or Visual Studio Code, but you could do without (not recommended).

    - [Download Visual Studio](https://net5.link/VS)
    - [Download Visual Studio Code](https://net5.link/VSC)

1. You need .NET 6 SDK. If you installed Visual Studio, you should be fine. Otherwise, here is the link:

    - [Download .NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

With that in place, you should be good to go!

### Build and Test

All projects and solutions can be built using the .NET CLI or Visual Studio.
You can find the most useful commands in the introduction chapter, under `Running and building your program` or online.

The two commands that you will need are:

-   `dotnet build` to build a project or solution
-   `dotnet test` to run a test project or all tests of all test projects of a solution.

### Content/Code

Throughout the book, there are many projects, and many are copies with a little update done to them, so they may look a lot alike.
Here, we will build a list that maps harder to find code to its file or directory in the Git repository.
If you find something missing, erroneous, or hard to find, please open an issue and let us know.

### Contribute

Please open an issue if you find some missing docs, errors in the source code, or a divergence between the book and the source code.

For more information, check out the [Code of conduct](CODE_OF_CONDUCT.md).

### Related products <Other books you may enjoy>

-   C# 10 and .NET 6 – Modern Cross-Platform Development, Sixth Edition [[Packt]](https://www.packtpub.com/product/c-10-and-net-6-modern-cross-platform-development-sixth-edition/9781801077361) [[Amazon]](https://www.amazon.com/10-NET-Cross-Platform-Development-websites/dp/1801077363)

-   Software Architecture with C# 10 and .NET 6, Third Edition [[Packt]](https://www.packtpub.com/product/software-architecture-with-c-10-and-net-6-third-edition/9781803235257) [[Amazon]](https://www.amazon.com/Software-Architecture-NET-solutions-microservices/dp/180323525X)

## Get to Know the Author

**Carl-Hugo Marcotte**
has been developing, designing, and architecting web applications professionally since 2005, wrote his first line of code at about eight years old, and holds a bachelor's degree in computer science.
After working at a firm for a few years, he became an independent consultant, where he developed projects of different sizes for SMEs and educational institutions. He is now a Senior Solutions Architect at Export Development Canada and is passionate about software architecture, C#, ASP.NET Core, and the Cloud.
He loves to share his knowledge, which led him to teaching programming, blogging, and creating, maintaining and contributing to multiple open-source projects.
### Download a free PDF

 <i>If you have already purchased a print or Kindle version of this book, you can get a DRM-free PDF version at no cost.<br>Simply click on the link to claim your free PDF.</i>
<p align="center"> <a href="https://packt.link/free-ebook/9781803249841">https://packt.link/free-ebook/9781803249841 </a> </p>