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
}
