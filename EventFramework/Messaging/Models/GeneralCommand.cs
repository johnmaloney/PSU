﻿using System;
using System.Collections.Generic;
using System.Text;
using Universal.Contracts.Messaging;

namespace Messaging.Models
{
    public class GeneralCommand : IMessage
    {
        public Guid Id { get; set; }

        public int CorrellationId { get; set; }

        public string Type { get { return typeof(GeneralCommand).AssemblyQualifiedName; } }

        public double Version { get { return 1.0; } }

        public string Command { get; set; }

        public IEnumerable<ICommandData> CommandDataCollection { get; set; }
    }
}
