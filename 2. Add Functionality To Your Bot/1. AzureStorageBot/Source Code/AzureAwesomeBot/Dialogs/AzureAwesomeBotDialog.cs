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

        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
        string image = null;
        string name = null;

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Hello from Azure Awesome Bot!");
            context.Wait(InputGiven);
        }

        public async Task InputGiven(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("botsource");

           
            await context.PostAsync("What can I do for you today?");
            var message = context.MakeMessage();
            message.AttachmentLayout = AttachmentLayoutTypes.Carousel;

            var att = new List<Attachment>(); 

          
        
            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    image = blob.Uri.ToString();
                    name = blob.Name;
                }
                var actions = new List<CardAction>()
            {
               AttachmentsHelper.CreateCardAction("Delete Image", this.name)
            };
                var card = AttachmentsHelper.CreateHeroCardAttachment(name,null,null, image, actions);
                att.Add(card);
            }

            message.Attachments = att;
            await context.PostAsync(message);
            context.Wait(ActionSelected);
        }

        private async Task ActionSelected(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("botsource");
            
            if (message.Text == this.name)
            {
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(this.name);
                blockBlob.Delete();
                await context.PostAsync($"You deleted the file: {this.name}");
            }
            
            else
                context.Wait(ActionSelected);
        }

    }
}