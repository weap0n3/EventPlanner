using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Core.Messages;

public class DetailsCloseMessage : ValueChangedMessage<string>
{
    public DetailsCloseMessage(string value) : base(value)
    {
    }
}
