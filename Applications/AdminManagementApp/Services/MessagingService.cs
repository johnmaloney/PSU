﻿using AdminManagementApp.Data;
using AdminManagementApp.Models;
using Messaging;
using Messaging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Universal.Contracts.Messaging;

namespace AdminManagementApp.Services
{
    public class MessagingService
    {
        #region Fields

        private readonly ITopicMessengerClient topicMessenger;
        private readonly ITopicObserverClient topicObserver;
        private readonly FileService fileService;
        private readonly MessageRepository repository;
        private readonly IQueueMessengerClient commandMessenger;

        #endregion

        #region Properties



        #endregion

        #region Methods

        public MessagingService(MessageRepository repository,
            IQueueMessengerClient commandMessenger,
            ITopicMessengerClient topicMessenger,
            ITopicObserverClient topicObserver)
        {
            this.repository = repository;
            this.commandMessenger = commandMessenger;
            
            this.topicMessenger = topicMessenger;
            this.topicObserver = topicObserver;
            //this.topicObserver.RegisterForNotificationOf<TopicMessage>(MessageReceiver);
        }

        public IEnumerable<IMessage> CurrentMessages()
        {
            return repository.GetAll();
        }

        public IMessage GetMessage(Guid id)
        {
            return repository.Get(id);
        }

        public async Task<bool> Generate(JobRequest request)
        {
            var message = request.GenerateMessage();           

            if (message != null)
            {
                await commandMessenger.Send(message);
                return true;
            }
            return false;
        }

        public async Task MessageReceiver(IMessage message)
        {
            if (message is TopicMessage gMessage)
            {
                var existingMessage = this.repository.Get(gMessage.Id);
                if (existingMessage == null)
                {
                    this.repository.Add(gMessage);
                }
            }
            await Task.FromResult(true);
        }

        #endregion
    }
}
