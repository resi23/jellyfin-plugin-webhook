﻿using System.Threading.Tasks;
using Jellyfin.Data.Events.Users;
using Jellyfin.Plugin.Webhook.Destinations;
using Jellyfin.Plugin.Webhook.Helpers;
using MediaBrowser.Common;
using MediaBrowser.Controller.Events;

namespace Jellyfin.Plugin.Webhook.Notifiers
{
    /// <summary>
    /// User password changed notifier.
    /// </summary>
    public class UserPasswordChangedNotifier : IEventConsumer<UserPasswordChangedEventArgs>
    {
        private readonly IApplicationHost _applicationHost;
        private readonly IWebhookSender _webhookSender;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserPasswordChangedNotifier"/> class.
        /// </summary>
        /// <param name="applicationHost">Instance of the <see cref="IApplicationHost"/> interface.</param>
        /// <param name="webhookSender">Instance of the <see cref="IWebhookSender"/> interface.</param>
        public UserPasswordChangedNotifier(
            IApplicationHost applicationHost,
            IWebhookSender webhookSender)
        {
            _applicationHost = applicationHost;
            _webhookSender = webhookSender;
        }

        /// <inheritdoc />
        public async Task OnEvent(UserPasswordChangedEventArgs eventArgs)
        {
            var dataObject = DataObjectHelpers
                .GetBaseDataObject(_applicationHost, NotificationType.UserPasswordChanged)
                .AddUserData(eventArgs.Argument);

            await _webhookSender.SendNotification(NotificationType.UserPasswordChanged, dataObject);
        }
    }
}