using EventPlanner.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Data.Services;

public class EventService
{
    IDatabase _db;
    //private readonly string[] _randomColors = { "PastelBLue", "PastelLightYellow", "PastelRed", "PastelLightRed" };
    //private string _lastColorKey;


    private readonly Random _random = new Random();

    public EventService(IDatabase db)
    {
        this._db = db;
    }

    //public Event AddColor(Event e)
    //{
    //    string newColor;
    //    do
    //    {
    //        newColor = _randomColors[_random.Next(_randomColors.Length)];
    //    } while (newColor == _lastColorKey && _randomColors.Length > 1);

    //    e.ColorKey = newColor;
    //    _lastColorKey = newColor;
    //    return e;
    //}

    //public List<Event> GetAll()
    //{
    //    return _db.GetEvents().Select(AddColor).ToList();
    //}

    //public bool Delete(Event e)
    //{ 
    //    return _db.DeleteEvent(e); 
    //}

    //public bool Update(Event oldEvent,Event newEvent)
    //{
    //    return _db.UpdateEvent(oldEvent, newEvent);
    //}
}
