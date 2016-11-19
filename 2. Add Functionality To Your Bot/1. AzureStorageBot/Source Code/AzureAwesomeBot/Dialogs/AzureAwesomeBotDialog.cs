using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using AzureAwesomeBot.Helpers;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types

namespace AzureAwesomeBot.Dialogs
{
    [Serializable()]
    public class AzureAwesomeBotDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Hello from Azure Awesome Bot!");
            context.Wait(InputGiven);

        }

        public async Task InputGiven(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("botsource");

            string image = null;

            await context.PostAsync("What can I do for you today?");
            var message = context.MakeMessage();
            message.AttachmentLayout = AttachmentLayoutTypes.Carousel;

            var att = new List<Attachment>(); 

            var actions = new List<CardAction>()
            {
               AttachmentsHelper.CreateCardAction("First Action", "First Action"),AttachmentsHelper.CreateCardAction("Second Action","Second Action")
            };
        
            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    image = blob.Uri.ToString();
                }
                var card = AttachmentsHelper.CreateHeroCardAttachment("Title", "Text", "Subtitle", image, actions);
                att.Add(card);
            }

            message.Attachments = att;
            await context.PostAsync(message);
            context.Wait(ActionSelected);
        }

        private async Task ActionSelected(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;

            if (message.Text == "First Action")
            {
                await context.PostAsync($"You selected: {message.Text}");
            }
            else if (message.Text == "Second Action")
            {
                await context.PostAsync($"You selected: {message.Text}");
            }
            else
                context.Wait(ActionSelected);
        }

    }
}