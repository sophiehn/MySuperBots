using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Threading;

namespace MyFirstBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        protected int count = 1;

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            if (message.Text.ToLower().Contains("reset"))
            {
                await this.RequestConfirmationAsync(context);
            }
            else
            {
                await context.PostAsync($"{count++}: You said {message.Text}");
                context.Wait(MessageReceivedAsync);
            }
            
        }

        private async Task RequestConfirmationAsync(IDialogContext context)
        {
            await context.PostAsync("Are you sure you want to reset the count?");
            context.Call(new ConfirmationDialog(), this.ResumeAfterConfirmation);           
        }

        private async Task ResumeAfterConfirmation(IDialogContext context, IAwaitable<string> result)
        {
            var resultFromConfirm = await result;

            await context.PostAsync($"Your confirmation for reset is: {resultFromConfirm}");
            if (resultFromConfirm.ToLower().Contains("yes"))
            {
                count = 1;
                await context.PostAsync("Reset count.");
            }
            else
            {
                await context.PostAsync("Did not reset count.");
            }

            context.Wait(MessageReceivedAsync);
        }

    }
}