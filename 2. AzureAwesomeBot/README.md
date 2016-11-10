## Overview

## Objectives
In this module, you'll:


## Prerequisites
The following is required to complete this module:
- Complete [Module 1- Get Started With Your First Bot](https://github.com/sophiehn/MyBots/tree/master/1.%20Get%20Started%20With%20Your%20First%20Bot)


## Exercises
This module includes the following exercises:

1. [Add functionality to the Bot](https://github.com/sophiehn/MyBots/tree/master/2.%20AzureAwesomeBot#exercise-1-add-attachments-to-the-bot)
1. [Test and understand the new functionalities](https://github.com/sophiehn/MyBots/tree/master/2.%20AzureAwesomeBot#exercise-2-test-and-understand-the-new-functionalities)
1. [Publish the new Bot to Azure](https://github.com/sophiehn/MyBots/tree/master/2.%20AzureAwesomeBot#exercise-3-publish-the-new-bot-to-azure)


## Exercise 1: Add Attachments to the Bot

We are now going to change the template code that counts the characters of the message, to something more complicated.


Let's go back to the **Project in Visual Studio** and start making changes!


### Task 1
- **Open** the **MessagesController Class** and replace the **Post Method** with this:


```csharp
public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                await Conversation.SendAsync(activity, () => new AzureAwesomeBotDialog());
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
```

### Task 2
- Create a new Folder by r**ight-clicking** on the **Project=>Add=>New Folder**. **Name** the folder **Dialogs**


![bot22.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot22.png)


- In the **Dialogs Folder** create a **new Class** and name it **AzureAwesomeBotDialog.cs**

![bot23.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot23.png)

![](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot24.png)


- Open the **AzureAwesomeBotDialog.cs file** you just created and replace all the code with this:


```csharp
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
```

### Task 3
- Repeat the **above steps** to create a **New Folder** named **Helpers** and a **New Class** inside it named **AttachmentHelpers.cs**

![bot26.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot26.png)


- **Replace** the code of the **AttachmentHelpers Class** with the following:


```csharp
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureAwesomeBot.Helpers
{
    public class AttachmentsHelper
    {
        public static Attachment CreateHeroCardAttachment(string actionTitle, string actionText,
         string actionSubtitle, string picURL, IEnumerable<CardAction> actions)
        {
            var card = new HeroCard
            {
                Title = actionTitle,
            };
            if (actionText != null)
                card.Text = actionText;
            if (actionSubtitle != null)
                card.Subtitle = actionSubtitle;

            card.Images = new List<CardImage>
                         {
                                new CardImage
                                {
                                    Url = picURL
                                }
                         };
            card.Buttons = new List<CardAction>(actions);

            return card.ToAttachment();
        }

        public static CardAction CreateCardAction(string title, string value)
        {
            return new CardAction()
            {
                Title = title,
                Value = value,
                Type = ActionTypes.PostBack
            };
        }
    }
}
```

### Task 4
- Go back to the **MessageController** Class and **add the following two lines of code** on the top of the file:

before this line:
> *namespace AzureAwesomeBot{.....}*

```csharp

using Microsoft.Bot.Builder.Dialogs;
using AzureAwesomeBot.Dialogs;
```





## Exercise 2: Test and understand the new functionalities
Now let's test the changes:

- **Debug** the Bot locally from Visual Studio

![](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot30.png)

- **Start the Emulator** and **send a message** to the Bot. It should respond with two messages and an attachment as below:


![](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot28.png)

- **Click** on one of the two **buttons** and see what happens!


![](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot29.png)


## Exercise 3: Publish the new Bot to Azure




