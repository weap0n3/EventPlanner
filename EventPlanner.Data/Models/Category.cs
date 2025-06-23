using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Data.Models;

[Table("categories")]
public class Category
{
    public int CategoryID { get; set; }
    public string Title { get; set; }

    public List<Event> Events { get; set; } = new();
}
