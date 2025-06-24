using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EventPlanner.Data.Models;

public class Event
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public string Title { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;

    public string ColorKey { get; set; } = string.Empty;

    public Event()
    {
        
    }

    public Event(string title, DateTime date, int categoryId)
    {
        this.Title = title;
        this.Date = date;
        this.CategoryId = categoryId;
    }
}
