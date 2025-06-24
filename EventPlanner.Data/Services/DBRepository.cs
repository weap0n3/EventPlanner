using EventPlanner.Data.Models;
using Microsoft.EntityFrameworkCore;
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
                context.Attach(e.Category); 
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
        var events = context.Events.Include(e => e.Category).ToList();
        return events;
    }

    public bool UpdateEvent(Event oldEvent, Event newEvent)
    {
        try 
        {
            using (var context= new EventContext(_dbPath))
            {
                var entity = context.Events.Find(oldEvent.Id);
                if (entity != null)
                {
                    entity.Title = newEvent.Title;
                    entity.Description = newEvent.Description;
                    entity.Date = newEvent.Date;
                    context.SaveChanges();
                }
            }
            return true;
        }
        catch (Exception err) 
        { 
            System.Diagnostics.Debug.WriteLine(err);
            return false; 
        }
    }

    public Category GetCategoryByTitle(string title)
    {
        using (var context = new EventContext(_dbPath))
        {
            return context.Categories?.FirstOrDefault(c => c.Title == title);
        }
    }
    public Category GetCategoryById(int id)
    {
        using (var context = new EventContext(_dbPath))
        {
            return context.Categories?.FirstOrDefault(c => c.CategoryId == id);
        }
    }
}
