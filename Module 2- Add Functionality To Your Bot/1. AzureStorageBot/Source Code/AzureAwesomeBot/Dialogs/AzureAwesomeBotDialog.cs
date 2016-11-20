using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using AzureAwesomeBot.Helpers;
using Microsoft.Azure; 
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Net.Http;

namespace AzureAwesomeBot.Dialogs
{
    [Serializable()]
    public class AzureAwesomeBotDialog : IDialog<object>
    {

        string image = null;
        string name = null;

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
               AttachmentsHelper.CreateCardAction("Delete Image", this.name), AttachmentsHelper.CreateCardAction("Upload New Image", "Upload")
            };
                var card = AttachmentsHelper.CreateHeroCardAttachment(name, null, null, image, actions);
                att.Add(card);
            }

            message.Attachments = att;
            await context.PostAsync(message);
            context.Wait(ActionSelected);
        }

        private async Task ActionSelected(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("botsource");

            if (message.Text == this.name)
            {
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(this.name);
                blockBlob.Delete();
                await context.PostAsync($"You deleted the file: {this.name}");
            }

            else
            {
                await context.PostAsync($"Please upload your file");
                context.Wait(UploadSelected);
            }
        }


        private async Task UploadSelected(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("botsource");

            var message = await argument;
            foreach (var attachment in message.Attachments)
            {
                string decodedURL = HttpUtility.UrlDecode(attachment.ContentUrl);
                string imageName = decodedURL.Substring(decodedURL.LastIndexOf("\\") + 1);
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(imageName);
                blockBlob.Properties.ContentType = attachment.ContentType;
                try
                {
                    HttpClient client = new HttpClient();
                    using (client)
                    {

                        using (var stream=await client.GetStreamAsync(decodedURL))
                        {
                           await blockBlob.UploadFromStreamAsync(stream);
                        } 
                    }
                    await context.PostAsync("Successful");
                }
                catch (Exception)
                {
                    
                }

            }
        }
    }
}