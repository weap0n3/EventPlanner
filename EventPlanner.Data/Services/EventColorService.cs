using EventPlanner.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Data.Services;

public class EventColorService
{
    private readonly string[] _randomColors = { "PastelBLue", "PastelLightYellow", "PastelRed", "PastelLightRed" }; // Example
    private string _lastColorKey;

    private readonly Random _random = new Random();

    public Event AddColor(Event e)
    {
        string newColor;
        do
        {
            newColor = _randomColors[_random.Next(_randomColors.Length)];
        } while (newColor == _lastColorKey && _randomColors.Length > 1);

        e.ColorKey = newColor;
        _lastColorKey = newColor;
        return e;
    }
}
