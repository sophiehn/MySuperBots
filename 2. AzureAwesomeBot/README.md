## Overview

## Objectives
In this module, you'll:


## Prerequisites
The following is required to complete this module:
-Complete [Module 1- Get Started With Your First Bot](https://github.com/sophiehn/MyBots/tree/master/1.%20Get%20Started%20With%20Your%20First%20Bot)

## Exercises
This module includes the following exercises:
1. Change the main functionality of the Bot
1. Add Attachments to the Bot
1. Publish the new Bot to Azure

## Exercise 1: Change the main functionality of the Bot

We are now going to change the template code that counts the characters of the message, to something more complicated.

Let's go back to the **Project in Visual Studio**, **Open** the **MessagesController Class** and replace the **Post Method** with this:

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

