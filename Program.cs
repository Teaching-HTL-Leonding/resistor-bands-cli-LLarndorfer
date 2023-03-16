//*Resistor coler code deetector
//*Lukas Larndorfer
Console.OutputEncoding = System.Text.Encoding.Default;

if (args.Length != 1)
{
    Console.WriteLine("Wrong number of arguments!");
    return;
}
if (args[0].Length != 15 && args[0].Length != 19)
{
    Console.WriteLine("Invalid length of argument");
    return;
}


if (args[0].Length == 15)
{
if (args[0][3] != '-' || args[0][7] != '-' || args[0][11] != '-')
{
    System.Console.WriteLine("Missing hypen");
    return;
}
}
else if (args[0].Length == 19)
{
    if (args[0][3] != '-' || args[0][7] != '-' || args[0][11] != '-' || args[0][15] != '-')
{
    System.Console.WriteLine("Missing hypen");
    return;
}
}
if (args[0].Length == 15)
{
    if (TryDecode4ColorBands(
        args[0].Substring(0, 3),
        args[0].Substring(4, 3),
        args[0].Substring(8, 3),
        args[0].Substring(12, 3),
        out double resistorValue,
        out double tolerance
    ))
    {
        string formattedResistorValue = resistorValue.ToString("N");
        formattedResistorValue = resistorValue.ToString("N2");
        System.Console.WriteLine($"Resistance: {formattedResistorValue} Ω");
        System.Console.WriteLine($"Tolerance: ±{tolerance}%");
    }
    else
    {
        System.Console.WriteLine("Invalid color");
    }
}

else if (args[0].Length == 19)
{
    if (TryDecode5ColorBands(
        args[0].Substring(0, 3),
        args[0].Substring(4, 3),
        args[0].Substring(8, 3),
        args[0].Substring(12, 3),
        args[0].Substring(16, 3),
        out double resistorValue,
        out double tolerance
    ))
    {
        string formattedResistorValue = resistorValue.ToString("N");
        formattedResistorValue = resistorValue.ToString("N2");
        System.Console.WriteLine($"Resistance: {formattedResistorValue} Ω");
        System.Console.WriteLine($"Tolerance: ±{tolerance}%");
    }
    else
    {
        System.Console.WriteLine("Invalid color");
    }
}
bool TryConvertColorToDigit(string color, out int digit)
{
    switch (color)
    {
        case "Bla": digit = 0; break;
        case "Bro": digit = 1; break;
        case "Red": digit = 2; break;
        case "Ora": digit = 3; break;
        case "Yel": digit = 4; break;
        case "Gre": digit = 5; break;
        case "Blu": digit = 6; break;
        case "Vio": digit = 7; break;
        case "Gra": digit = 8; break;
        case "Whi": digit = 9; break;
        default: digit = -1; return false;
    }

    return true;
}

bool TryGetMultiplierFromColor(string color, out double multiplier)
{
    switch (color)
    {
        case "Gol": multiplier = 0.1d; break;
        case "Sil": multiplier = 0.01d; break;
        default:
            int digit;
            if (!TryConvertColorToDigit(color, out digit))
            {
                multiplier = -1d;
                return false;
            }

            multiplier = Math.Pow(10, digit);
            break;
    }
    return true;
}

bool TryGetToleranceFromColor(string toleranceColor, out double tolerance)
{
    switch (toleranceColor)
    {
        case "Bro": tolerance = 1; break;
        case "Red": tolerance = 2; break;
        case "Gre": tolerance = 0.5; break;
        case "Blu": tolerance = 0.25; break;
        case "Vio": tolerance = 0.1; break;
        case "Gra": tolerance = 0.05; break;
        case "Gol": tolerance = 5; break;
        case "Sil": tolerance = 10; break;
        default: tolerance = -1; return false;
    }

    return true;
}

bool TryDecode4ColorBands(string color1, string color2, string color3, string color4, out double resistorValue, out double tolerance)
{
    resistorValue = -1;
    tolerance = -1;

    if (!TryConvertColorToDigit(color1, out int digit1)) { return false; }
    if (!TryConvertColorToDigit(color2, out int digit2)) { return false; }
    if (!TryGetMultiplierFromColor(color3, out double multiplier)) { return false; }
    if (!TryGetToleranceFromColor(color4, out tolerance)) { return false; }

    resistorValue = (digit1 * 10 + digit2) * multiplier;
    return true;
}

bool TryDecode5ColorBands(string color1, string color2, string color3, string color4, string color5, out double resistorValue, out double tolerance)
{
    resistorValue = -1;
    tolerance = -1;

    if (!TryConvertColorToDigit(color1, out int digit1)) { return false; }
    if (!TryConvertColorToDigit(color2, out int digit2)) { return false; }
    if (!TryConvertColorToDigit(color3, out int digit3)) { return false; }
    if (!TryGetMultiplierFromColor(color4, out double multiplier)) { return false; }
    if (!TryGetToleranceFromColor(color4, out tolerance)) { return false; }

    resistorValue = (digit1 * 100 + digit2 * 10 + digit3) * multiplier;
    return true;
}