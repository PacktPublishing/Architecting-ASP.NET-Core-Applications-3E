namespace RawStringLiteral;

public class RawStringLiteralTests
{
    [Fact]
    public void The_whitespaces_are_not_considered()
    {
        var rawStringLiteral = """
            This
            is
            a
            raw
            string
            literal
            """;
        var normalString = $"This{Environment.NewLine}is{Environment.NewLine}a{Environment.NewLine}raw{Environment.NewLine}string{Environment.NewLine}literal";
        Assert.Equal(normalString, rawStringLiteral);

        var multiLineString = @"This
is
a
raw
string
literal";
        Assert.Equal(multiLineString, rawStringLiteral);
    }

    [Fact]
    public void Supports_interpolation1()
    {
        var variable = 123;
        var rawStringLiteral = $"""
            This is a raw string literal
            with a variable ({variable}) inside!
            """;
        var normalString = $"This is a raw string literal{Environment.NewLine}with a variable ({variable}) inside!";
        Assert.Equal(normalString, rawStringLiteral);
    }
    [Fact]
    public void Supports_interpolation2()
    {
        var variable = 123;
        var rawStringLiteral = $$"""
            This is a raw string literal
            with a variable ({{variable}}) inside!
            """;
        var normalString = $"This is a raw string literal{Environment.NewLine}with a variable ({variable}) inside!";
        Assert.Equal(normalString, rawStringLiteral);
    }
    [Fact]
    public void Supports_interpolation3()
    {
        var variable = 123;
        var rawStringLiteral = $$$"""
            This is a raw string literal
            with a variable ({{{variable}}}) inside!
            """;
        var normalString = $"This is a raw string literal{Environment.NewLine}with a variable ({variable}) inside!";
        Assert.Equal(normalString, rawStringLiteral);
    }

    [Fact]
    public void Can_use_quotes()
    {
        var rawStringLiteral = """
            This is a raw string literal
            with "quotes"!
            """;
        var normalString = $"This is a raw string literal{Environment.NewLine}with \"quotes\"!";
        Assert.Equal(normalString, rawStringLiteral);
    }
}