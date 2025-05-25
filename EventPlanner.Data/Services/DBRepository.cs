using EventPlanner.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Data.Services;

public class DBRepository : IDatabase
{
    private string _dbPath;

    public DBRepository(string dbPath)
    {
        this._dbPath = dbPath;
    }
    public bool AddEvent(Event e)
    {
        try
        {
            using(var context = new EventContext(_dbPath)) 
            { 
                context.Add(e);
                context.SaveChanges();
            }
            return true;
        } 
        catch(Exception err) 
        {
            System.Diagnostics.Debug.WriteLine(err);
            return false;
        }
    }

    public bool DeleteEvent(Event eventToDelete)
    {
        try
        {
            using(var context = new EventContext(_dbPath))
            { 
                context.Remove(eventToDelete);
                context.SaveChanges();
            }
            return true;
        }
        catch (Exception err)
        {
            System.Diagnostics.Debug.WriteLine(err); 
            return false;
        }
    }

    public List<Event> GetEvents()
    {
        var context = new EventContext(_dbPath);
        var events = (from ev in context.Events
                     select ev).ToList();
        return events;
    }
}
