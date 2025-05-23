using EventPlanner.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Data.Services;

public class EventContext : DbContext
{
    public DbSet<Event> Events { get; set; }

    private string _dbPath;

    public EventContext(string dbPath)
    {
        this._dbPath = dbPath;
        SQLitePCL.Batteries_V2.Init();
        this.Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        System.Diagnostics.Debug.WriteLine(_dbPath);
        optionsBuilder.UseSqlite($"Filename={this._dbPath}");

        base.OnConfiguring(optionsBuilder);
    }
}
