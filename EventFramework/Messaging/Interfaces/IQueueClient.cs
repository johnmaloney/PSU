﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Interfaces
{
    public interface IQueueClient
    {
        Task Send(IMessage message);
    }
}
