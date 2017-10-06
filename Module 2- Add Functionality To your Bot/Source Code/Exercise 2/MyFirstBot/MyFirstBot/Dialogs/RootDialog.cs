using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Threading;
using System.Diagnostics;

namespace MyFirstBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        protected int count = 1;
        string methodCalled;

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
                await this.RequestResetAsync(context);
            }
            else if (message.Text.ToLower().Contains("subscribe"))
            {
                await this.RequestSubscribeAsync(context);
            }
            else
            {
                await context.PostAsync($"{count++}: You said {message.Text}");
                context.Wait(MessageReceivedAsync);
            }
            
        }
        
        private async Task RequestResetAsync(IDialogContext context)
        {
            await context.PostAsync("Are you sure you want to reset the count? Type yes or no");
            methodCalled = "RequestResetAsync";
            context.Call(new ConfirmationDialog(), this.ResumeAfterConfirmation);           
        }
        
        private async Task RequestSubscribeAsync(IDialogContext context)
        {
            await context.PostAsync("Are you sure you want to subscribe? Type yes or no");
            methodCalled = "RequestSubscribeAsync";
            context.Call(new ConfirmationDialog(), this.ResumeAfterConfirmation);
        }

        private async Task ResumeAfterConfirmation(IDialogContext context, IAwaitable<string> result)
        {
            var resultFromConfirm = await result;
            if (resultFromConfirm.ToLower().Contains("yes"))
            {
                switch (methodCalled)
                {
                    
                    case "RequestResetAsync":
                        count = 1;
                        await context.PostAsync("Reset successfully.");
                        break;
                    case "RequestSubscribeAsync":
                        await context.PostAsync("Subscribed successfully.");
                        break;
                    default:
                        await context.PostAsync("Oops, something went wrong. Gonna have to start over");
                        break;
                }
              
            }
            else
            {
                switch (methodCalled)
                {
                   
                    case "RequestResetAsync":
                        await context.PostAsync("Did not reset.");
                        break;
                    case "RequestSubscribeAsync":
                        await context.PostAsync("Did not subscribe.");
                        break;
                    default:
                        await context.PostAsync("Oops, something went wrong. Gonna have to start over");
                        break;
                }
            }
            context.Wait(MessageReceivedAsync);
        }

    }
}