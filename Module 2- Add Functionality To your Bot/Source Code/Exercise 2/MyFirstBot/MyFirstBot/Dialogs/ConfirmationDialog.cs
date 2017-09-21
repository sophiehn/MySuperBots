using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;


namespace MyFirstBot.Dialogs
{
    [Serializable]
    public class ConfirmationDialog : IDialog<string>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            if (message.Text.ToLower().Contains("yes"))
                context.Done("Yes");
            else
                context.Done("No");
        }
        
    }
}