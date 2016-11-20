using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using AzureAwesomeBot.Helpers;
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
            await context.PostAsync("What can I do for you today?");
            var message = context.MakeMessage();
            message.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            var att = new List<Attachment>();
            var actions = new List<CardAction>()
            {
               AttachmentsHelper.CreateCardAction("First Action", "First Action"),AttachmentsHelper.CreateCardAction("Second Action","Second Action")
            };
            var card = AttachmentsHelper.CreateHeroCardAttachment("Title", "Text", "Subtitle", "http://i292.photobucket.com/albums/mm38/iCe-quEen99/athens.jpg", actions);
            att.Add(card);
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