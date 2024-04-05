using System;

namespace BetterBepinConsole;

internal static class TextColour
{
    private const string AnsiReset = "\x1b[0m";
    private const string AnsiRed = "\x1b[31m";
    private const string AnsiGreen = "\x1b[32m";
    private const string AnsiBlue = "\x1b[34m";
    private const string AnsiWhite = "\x1b[37m";
    private const string AnsiBrightYellow = "\x1b[93m";
    private const string AnsiBrightCyan = "\x1b[96m";

    public const Colour Red = Colour.Red;
    public const Colour Green = Colour.Green;
    public const Colour Blue = Colour.Blue;
    public const Colour White = Colour.White;
    public const Colour BrightYellow = Colour.BrightYellow;
    public const Colour BrightCyan = Colour.BrightCyan;

    public static string ColourAs(this string @this, Colour colour)
    {
        string ansiColour = GetAnsiColour(colour);
        return ansiColour + @this + AnsiReset;
    }

    public static string FillColourAs(this string @this, Colour colour)
    {
        string ansiColour = GetAnsiColour(colour);
        return ansiColour + @this;
    } 

    private static string GetAnsiColour(Colour colour)
    {
        return colour switch
        {
            Colour.Red => AnsiRed,
            Colour.Green => AnsiGreen,
            Colour.Blue => AnsiBlue,
            Colour.White => AnsiWhite,
            Colour.BrightYellow => AnsiBrightYellow,
            Colour.BrightCyan => AnsiBrightCyan,
            _ => AnsiReset
        };
    }

    public enum Colour
    {
        None = 0,
        Red = 1,
        Green = 2,
        Blue = 3,
        White = 4,
        BrightYellow = 5,
        BrightCyan = 6
    }
}