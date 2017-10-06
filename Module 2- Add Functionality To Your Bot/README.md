
## Abstract

When you create a bot using the **Bot Builder SDK for .NET**, you can **use dialogs to model a conversation** and **manage conversation flow**. 
Each dialog is an abstraction that encapsulates its own state in a C# class that implements ```IDialog```. 
A dialog can be composed with other dialogs to maximize reuse, and a dialog context maintains the stack of dialogs that are active in the conversation at any point in time. 

**In a traditional application**, everything begins with the **main screen**. The main screen **invokes a new screen**. The new screen remains **in control until it either closes or invokes other screens**. If the new order screen closes, the user is returned to the main screen.

**In a bot, everything begins with the root dialog**. The root dialog **invokes a new dialog**. At that point, **the new dialog takes control** of the conversation and remains in control until it either closes or invokes other dialogs. If the new dialog closes, **control of the conversation is returned back to the root dialog**.

When you use dialogs in the Bot Builder SDK for .NET, conversation state (the dialog stack and the state of each dialog in the stack) is automatically stored using the Bot Framework State service. This **enables your bot to be stateless**, much like a web application that does not need to store session state in web server memory.

## Overview

In this Module you will add some basic functionality to your Bot and learn how to create and invoke dialogs and handle states.

##  Objectives

After completing this module, you should be able to:
- Create a **Dialog Class** and add Code
- Add **State and Functionality** to your Dialog 
- **Invoke a Dialog** from another Dialog
- Create a **Dialog Chain**
- Understand the **Dialog Lifecycle**

##  Prerequisites

The following is required to complete this module:
- Complete [Module 1- Get Started With Your First Bot](https://github.com/sophiehn/MySuperBots/tree/master/Module%201-%20Create%20your%20First%20Bot)


## Exercises
This module includes the following exercises:

1. [Create a Message-Counting Bot with State](https://github.com/sophiehn/MySuperBots/tree/master/Module%202-%20Add%20Functionality%20To%20your%20Bot#exercise-1-create-a-message-counting-bot-with-state)
1. [Invoke a Dialog from another Dialog](https://github.com/sophiehn/MySuperBots/tree/master/Module%202-%20Add%20Functionality%20To%20your%20Bot#exercise-2-invoke-a-dialog-from-another-dialog)
1. [Create a Bot with a Chain of Dialogs]()
1. [Explore the Dialog Lifecycle]()

## Exercise 1: Create a Message-Counting Bot with State

In the Bot Builder SDK for .NET, the Builder library enables you to implement dialogs. 

### Task 1- Create the Reset Process 

- Let's go back to our **Project** and **replace** the ```RootDialog``` class  with the following:

```csharp
[Serializable]
public class RootDialog : IDialog<object>
{
    protected int count = 1;

    public async Task StartAsync(IDialogContext context)
    {
        context.Wait(MessageReceivedAsync);
    }

    public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
    {
        var message = await argument;
        if (message.Text == "reset")
        {
            PromptDialog.Confirm(
                context,
                AfterResetAsync,
                "Are you sure you want to reset the count?",
                "Didn't get that!",
                promptStyle: PromptStyle.None);
        }
        else
        {
            await context.PostAsync($"{this.count++}: You said {message.Text}");
            context.Wait(MessageReceivedAsync);
        }
    }

    public async Task AfterResetAsync(IDialogContext context, IAwaitable<bool> argument)
    {
        var confirm = await argument;
        if (confirm)
        {
            this.count = 1;
            await context.PostAsync("Reset count.");
        }
        else
        {
            await context.PostAsync("Did not reset count.");
        }
        context.Wait(MessageReceivedAsync);
    }
}
```

### Task 2- Test the new Code

- We **Start the project** again, and connect to the **Emulator**. If we **type a message** to the Bot, we will now get a different response. 
- Go ahead and **type a few more messages**. Did you notice that the Bot is **counting the number of messages** you have sent so far? This means that it is able to transfer a **State** between the messages.
- Now let's **type 'Reset'** and after that **'Yes'** when confirmation is needed. You should get back a 'Reset count' message. Now try to **send another message** to the Bot. It should start counting from the beggining:   

![CountResetExample](https://github.com/sophiehn/MySuperBots/blob/master/Module%202-%20Add%20Functionality%20To%20your%20Bot/Images/CountResetExample.PNG)

Let's take a moment to dive in the code and understand what it does. The ```MessageReceived``` Task, instead of counting the length or the message and posting the results, now has an **if condition**. 

In case the user types in **'Reset'**, the task:
- Shows a ```PromptDialog``` for confirmation, that takes as input the ```IDialogContext context```,
- Asks the user if they **confirm the reset**,
- Invokes the ``` AfterResetAsync ``` Task that handles what happens after the reset confirmation (basically executes the reset process),
- Shows an **error message** in case the user's response was not understandable by the bot.

In any other case, the Task echos the user message with the message count in the beginning:

![MessageReceived](https://github.com/sophiehn/MySuperBots/blob/master/Module%202-%20Add%20Functionality%20To%20your%20Bot/Images/MessageReceived.PNG)

The ```AfterResetAsync``` Task takes as input the ```IDialogContext context``` and an ``` IAwaitable<bool> argument ``` which contains the **result of the reset confirmation dialog** above.
According to its value, the Task resets (or doesn't reset) the count and posts the corresponding message to the user:

![AfterReset](https://github.com/sophiehn/MySuperBots/blob/master/Module%202-%20Add%20Functionality%20To%20your%20Bot/Images/AfterReset.PNG)

The ```IDialogContext``` interface that is passed into each dialog method provides access to the services that a dialog requires to save state and communicate with the channel. The ```IDialogContext``` interface comprises three interfaces: ```Internals.IBotData```, ```Internals.IBotToUser```, and ```Internals.IDialogStack```.

See this example with **more detailed documentation** [here](https://docs.microsoft.com/en-us/bot-framework/dotnet/bot-builder-dotnet-dialogs).

This was a simple example of how we can add user interactions within the Bot. Now let's see how we can create multiple dialogs and invoke the appropriate one, when needed.


## Exercise 2: Invoke a Dialog from another Dialog

Now that we fully understood the process of the reset count with the ```PromptDialog```, let's see how we can create a more reusable solution.

Let's assume that we have a use case where we need confirmation dialogs for various actions, not only for reset. With the above approach (PromptDialog), we would have to create a new dialog for every action we want to confirm. 

What if we could make the confirmation dialog more generic, and use it everytime we want to confirm an action?

### Task 1- Create a Generic Confirmation Dialog 

- Let's add a new Dialog to our  **Project** by **right-clicking** on it=>**Add**=> **Class** which we are going to name **ConfirmationDialog.cs**:

![ConfirmationDialog](https://github.com/sophiehn/MySuperBots/blob/master/Module%202-%20Add%20Functionality%20To%20your%20Bot/Images/ConfirmationDialog.PNG)

![ConfirmationDialog2](https://github.com/sophiehn/MySuperBots/blob/master/Module%202-%20Add%20Functionality%20To%20your%20Bot/Images/ConfirmationDialog2.PNG)

Now we have a class ready to be turned into a Bot Dialog:

- **Replace the class code** with:
``` 
[Serializable]
public class ConfirmationDialog : IDialog<string> 
{
}
```
- **Add the following** dependencies on the using area:
```
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
```
- **Hover** on the ```IDialog<string>``` , click on the LightBulb and select **Implement Interface**:

![Interface](https://github.com/sophiehn/MySuperBots/blob/master/Module%202-%20Add%20Functionality%20To%20your%20Bot/Images/Interface.PNG)

You should be at the point that your code looks like this:

![Start](https://github.com/sophiehn/MySuperBots/blob/master/Module%202-%20Add%20Functionality%20To%20your%20Bot/Images/ConfirmationDialogStart.PNG)

If so, **replace the code inside the class** with the following:

```
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

```
What this does is, it simply checks if the user confirms an action or not and returns the result to the calling method.

We are all set with the Confirmation Method, now let's put it into use in our Root Dialog.

### Task 2- Invoke the Dialog in the Root

- **Replace the Root Dialog Class** with the following:
```
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
                await this.RequestResetAsync(context);
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
            context.Call(new ConfirmationDialog(), this.ResumeAfterConfirmation);
        }

        private async Task ResumeAfterConfirmation(IDialogContext context, IAwaitable<string> result)
        {
            var resultFromConfirm = await result;
            if (resultFromConfirm.ToLower().Contains("yes"))
            {
                 count = 1;
                 await context.PostAsync("Reset successfully.");
            }
            else
                 await context.PostAsync("Did not reset.");
            context.Wait(MessageReceivedAsync);
        }
    }
```
- Now Test again your Bot. It should have the same results as with the previous code.
 
![Example](https://github.com/sophiehn/MySuperBots/blob/master/Module%202-%20Add%20Functionality%20To%20your%20Bot/Images/CountResetExample.PNG)

 But now we can use the Confirmation Dialog for other cases as well, not just for the reset. For example, another scenario that usually needs to be confirmed is a request to subscribe to a service or thread. Let's add this scenario to our existing code, using the ConfirmationDialog to ask the user if they are sure or not to subscribe.

### Task 3- Use the Confirmation Dialog in More Than One Cases

- **Replace the MessageReceivedAsync Task** code with:
```
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
```

- **Add this declaration before your of StartAsyc** Task:
```         
string methodCalled;    
```
We need this to store the name of the method that requested the Confirmation Dialog.

- **Add this method** to your class:
```
        private async Task RequestSubscribeAsync(IDialogContext context)
        {
            await context.PostAsync("Are you sure you want to subscribe? Type yes or no");
            methodCalled = "RequestSubscribeAsync";
            context.Call(new ConfirmationDialog(), this.ResumeAfterConfirmation);
        }
```

- **Replace the ResumeAfterConfirmation Task** code with:

```
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
```

- Finally, **replace the RequestResetAsync Task** with:
```
private async Task RequestResetAsync(IDialogContext context)
        {
            await context.PostAsync("Are you sure you want to reset the count? Type yes or no");
            methodCalled = "RequestResetAsync";
            context.Call(new ConfirmationDialog(), this.ResumeAfterConfirmation);
        }
```

- Test your Bot by typing **Subscribe** at any point of the conversation:

![subscribe](https://github.com/sophiehn/MySuperBots/blob/master/Module%202-%20Add%20Functionality%20To%20your%20Bot/Images/subscribe.PNG)

Now, we could add many other cases that Confirmation is needed, but let's keep it simple for now and focus on perfecting the user experience. 

Noticed anything unusual up until now? For me, the 'Type yes or no' part is not very user friendly. I would prefer to have a list of options in a form of a button to select instead of typing.
Aggree? Let's move forward to the next Module then and see how we can make this conversation flow more 'pretty'.
