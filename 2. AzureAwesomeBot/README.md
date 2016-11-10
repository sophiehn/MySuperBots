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

- Create a new Folder by r**ight-clicking** on the **Project=>Add=>New Folder**. **Name** the folder **Dialogs**


![bot22.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot22.png)


- In the **Dialogs Folder** create a **new Class** and name it **AzureAwesomeBotDialog.cs**

![bot23.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot23.png)

![](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot24.png)

- Repeat the **same steps** and create a **New Folder** named **Helpers** and a **New Class** inside it named **AttachmentHelpers.cs**

![bot26.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot26.png)

![bot27.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot27.png)


