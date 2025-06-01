using CommunityToolkit.Mvvm.Messaging.Messages;
using EventPlanner.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Core.Messages;

public class DeleteEventMessage : ValueChangedMessage<Event>
{
    public DeleteEventMessage(Event value) : base(value)
    {
    }
}
