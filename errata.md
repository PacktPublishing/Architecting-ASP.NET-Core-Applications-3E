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
