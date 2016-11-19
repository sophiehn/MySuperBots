## Overview


## Objectives
In this module, you'll:

- Create a **Dialog Class**
- Create a **Helper Class** for the **Attachments**
- Create One **Hero Card Attachment** 
- **Display** the Attachment with two **Card Actions**


## Prerequisites
The following is required to complete these modules:
- [Module 1- Create Your First Bot](https://github.com/sophiehn/MySuperBots/tree/master/1.%20Create%20your%20First%20Bot)
- [Module 2- 2. Add Functionality To Your Bot/1. AzureStorageBot/](https://github.com/sophiehn/MySuperBots/tree/master/2.%20Add%20Functionality%20To%20Your%20Bot/1.%20AzureStorageBot)

## Exercises
This module includes the following exercises:

1. [Add functionality to the Bot](https://github.com/sophiehn/MySuperBots/tree/master/1.%20Create%20your%20First%20Bot/2.%20AzureAwesomeBot#exercise-1-add-attachments-to-the-bot)



## Exercise 1: 

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


## Next Steps

- [Add Functionality To Your Bot](https://github.com/sophiehn/MySuperBots/tree/master/2.%20Add%20Functionality%20To%20Your%20Bot)
- [Publish Your Bot](https://github.com/sophiehn/MySuperBots/tree/master/3.%20Publish%20Your%20Bot)

