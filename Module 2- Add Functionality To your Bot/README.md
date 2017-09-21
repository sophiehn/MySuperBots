
## Abstract

When you create a bot using the Bot Builder SDK for .NET, you can use dialogs to model a conversation and manage conversation flow. 
Each dialog is an abstraction that encapsulates its own state in a C# class that implements ```IDialog```. 
A dialog can be composed with other dialogs to maximize reuse, and a dialog context maintains the stack of dialogs that are active in the conversation at any point in time. 

When you use dialogs in the Bot Builder SDK for .NET, conversation state (the dialog stack and the state of each dialog in the stack) is automatically stored using the Bot Framework State service. This enables your bot to be stateless, much like a web application that does not need to store session state in web server memory.

## Overview

In this Module you will add some basic functionality to your Bot and learn how to handle activities, dialogs and states.

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

1. [Create a Message-Counting Bot with State](https://github.com/sophiehn/MySuperBots/tree/master/Module%201-%20Create%20your%20First%20Bot#exercise-1-create-your-first-bot-application-in-visual-studio)
1. [Invoke a Dialog from another Dialog](https://github.com/sophiehn/MySuperBots/tree/master/Module%201-%20Create%20your%20First%20Bot#exercise-2-debug-your-application)
1. [Create a Bot with a Chain of Dialogs](https://github.com/sophiehn/MySuperBots/tree/master/Module%201-%20Create%20your%20First%20Bot#exercise-3-chat-with-your-bot)
1. [Explore the Dialog Lifecycle](https://github.com/sophiehn/MySuperBots/tree/master/Module%201-%20Create%20your%20First%20Bot#exercise-4-publish-your-bot-to-azure)

## Exercise 1: Create a Message-Counting Bot with State

In the Bot Builder SDK for .NET, the Builder library enables you to implement dialogs. 

### Task 1 

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

### Task 2

- We **Start the project** again, and connect to the **Emulator**. If we **type a message** to the Bot, we will now get a different response. 
- Go ahead and **type a few more messages**. Did you notice that the Bot is **counting the number of messages** you have sent so far? This means that it is able to transfer a **State** between the messages.
- Now let's **type 'Reset'** and after that **'Yes'** when confirmation is needed. You should get back a 'Reset count' message. Now try to **send another message** to the Bot. It should start counting from the beggining:   

[CountResetExample]()

Let's take a moment to dive in the code and understand what it does. The ```MessageReceived``` Task, instead of counting the length or the message and posting the results, now has an **if condition**. 

In case the user types in **'Reset'**, the task:
- Shows a ```PromptDialog``` for confirmation, that takes as input the ```IDialogContext context```,
- Asks the user if they **confirm the reset**,
- Invokes the ``` AfterResetAsync ``` Task that handles what happens after the reset confirmation (basically executes the reset process),
- Shows an **error message** in case the user's response was not understandable by the bot.

In any other case, the Task echos the user message with the message count in the beginning:

[MessageReceived]()

The ```AfterResetAsync``` Task takes as input the ```IDialogContext context``` and an ``` IAwaitable<bool> argument ``` which contains the **result of the reset confirmation dialog** above.
According to its value, the Task resets (or doesn't reset) the count and posts the corresponding message to the user:

[AfterReset]()

The ```IDialogContext``` interface that is passed into each dialog method provides access to the services that a dialog requires to save state and communicate with the channel. The ```IDialogContext``` interface comprises three interfaces: ```Internals.IBotData```, ```Internals.IBotToUser```, and ```Internals.IDialogStack```.

See this example with **more detailed documentation** [here](https://docs.microsoft.com/en-us/bot-framework/dotnet/bot-builder-dotnet-dialogs).

This was a simple example of how we can add user interactions within the Bot. Now let's see how we can create multiple dialogs and invoke the appropriate one, when needed.


## Exercise 2: Invoke a Dialog from another Dialog

