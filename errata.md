# Errata

If you find any mistakes, then please [raise an issue in this repository](https://github.com/PacktPublishing/Architecting-ASP.NET-Core-Applications-3E/issues/new) or email me at [public@carl-hugo.ca](mailto:public@carl-hugo.ca).

## Page 97—Chapter 3 - Interface Segregation Principle (ISP)

- Reported in issue [#4](https://github.com/PacktPublishing/Architecting-ASP.NET-Core-Applications-3E/issues/4) by [trfore](https://github.com/trfore). Thanks for reporting the issue on Jan 16 2025.
- Fixed in PR #5

The comment:

```csharp
// ModifyProducts(publicProductReader); // Invalid
```

Should instead read:

```csharp
// WriteProducts(publicProductReader); // Invalid
```

## Page 237—Chapter 8 - Code smell – Control Freak

* Reported in issue [#7](https://github.com/PacktPublishing/Architecting-ASP.NET-Core-Applications-3E/issues/7) by [Dochomorka](https://github.com/Dochomorka). Thanks for reporting the issue on Sep 18 2026.

The constructor:

```csharp
public DIEnabledConsumer(Dependency dependency)
{
    _dependency = dependency;
}
```

Should instead read:

```csharp
public Consumer(Dependency dependency)
{
    _dependency = dependency;
}
```

The constructor name must match the enclosing `Consumer` class; otherwise, the code does not compile.

> **Note:** This typo is limited to the code snippet in the book. The corresponding code in the repository uses a separate `DIEnabledConsumer` class with a matching `DIEnabledConsumer` constructor and compiles correctly.
