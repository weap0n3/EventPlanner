using EventPlanner.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Data.Services;

public interface IDatabase
{
    bool AddEvent(Event e);
    List<Event> GetEvents();
    bool DeleteEvent(Event EventToDelete);
    bool UpdateEvent(Event oldEvent,Event newEvent);
    Category GetCategoryByTitle(string title);
    Category GetCategoryById(int id);
}
