
##  Overview

In this Module you will set up your pc with all the prerequisites and create your first Bot Application, using the template provided by the Microsoft Bot Framework.


##  Objectives

In this module, you'll:
- Create a **Bot Application**
- **Debug** your Bot Application locally 
- Use the Microsoft Bot Framework Channel Emulator to **chat with your Bot** and **understand how the communication works**
- **Publish your Bot** to an Azure **App Service**

##  Prerequisites

The following is required to complete this module:

- [Visual Studio Community 2017](https://www.visualstudio.com/downloads/) or greater - **Important:** Please [update all VS extensions](https://docs.microsoft.com/en-us/visualstudio/extensibility/how-to-update-a-visual-studio-extension) to their latest versions via Tools->Extensions and Updates->Updates
- [Microsoft Bot Framework Channel Emulator](https://aka.ms/bf-bc-emulator)
- The [Bot Application](http://aka.ms/bf-bc-vstemplate), [Bot Controller](http://aka.ms/bf-bc-vscontrollertemplate) and [Bot Dialog](http://aka.ms/bf-bc-vsdialogtemplate) templates - **Download and Save the zip files** to your Visual Studio 2017 templates directory which is traditionally in **"%USERPROFILE%\Documents\Visual Studio 2017\Templates\ProjectTemplates\Visual C#\"**
- An active [Azure subscription](https://azure.microsoft.com/en-us/free/)

## Exercises
This module includes the following exercises:

1. [Create your first Bot Application in Visual Studio](https://github.com/sophiehn/MySuperBots/tree/master/Module%201-%20Create%20your%20First%20Bot#exercise-1-create-your-first-bot-application-in-visual-studio)
1. [Debug your Application](https://github.com/sophiehn/MySuperBots/tree/master/Module%201-%20Create%20your%20First%20Bot#exercise-2-debug-your-application)
1. [Chat with your Bot](https://github.com/sophiehn/MySuperBots/tree/master/Module%201-%20Create%20your%20First%20Bot#exercise-3-chat-with-your-bot)
1. [Publish your Bot to Azure](https://github.com/sophiehn/MySuperBots/tree/master/Module%201-%20Create%20your%20First%20Bot#exercise-4-publish-your-bot-to-azure)

## Exercise 1: Create your first Bot Application in Visual Studio

### Task 1 
Open Visual Studio 2017 and Create a **New Project** by selecting **Visual C#=>Bot Application** from the Templates. **Name** your Project **MyFirstBot**: 

![bot1.png](https://github.com/sophiehn/MySuperBots/blob/master/Module%201-%20Create%20your%20First%20Bot/Images/CreateProject.PNG) 

The template is a fully functional Echo Bot that takes the user's text utterance as input and returns it as output.
 
### Task 2 
Let's take a look for a moment on what the template has created for us: Check the **Solution Explorer** to see the resources  created    

![bot2.png](https://github.com/sophiehn/MySuperBots/blob/master/Module%201-%20Create%20your%20First%20Bot/Images/SolutionExplorer.PNG)

### Task 3 
**Open** the **Controllers Folder** and double-click on the **MessagesController.cs** file. There you can see the MessagesController Class with an async **Post Task**.    

![bot3.png](https://github.com/sophiehn/MySuperBots/blob/master/Module%201-%20Create%20your%20First%20Bot/Images/MessageController.PNG)  

The **Post Task** within Controllers\MessagesController.cs receives the message from the user and invokes the Root dialog. The **BotAuthentication** decoration on the method is used to validate your Bot Connector credentials over HTTPS. 

The **HandleSystemMessage** method is used to handle some cases other than sending a message to the bot, like what happends when the user adds or removes the bot from their contact list (useful for sending a greet or goodbye message to the user), members being added and removed to the conversation, if the user is typing a message, etc. We will focus on the Post method for now and come back to these later.

Now let's see what is inside this dialog. **Open** the **Dialogs Folder** and double-click on the **RootDialog.cs** file. There you can see the RootDialog Class with a **StartAsync** and a **MessageReceivedAsync** Task. The root dialog processes the message and generates a response. 

![bot34.png](https://github.com/sophiehn/MySuperBots/blob/master/Module%201-%20Create%20your%20First%20Bot/Images/RootDialog.PNG)

The **StartAsync** Task calls the **MessageReceivedAsync** Task everytime the RootDialog is being called by the MessageController (in this case, every time the user posts something to the conversation). 

![bot35.png](https://github.com/sophiehn/MySuperBots/blob/master/Module%201-%20Create%20your%20First%20Bot/Images/RootDialog2.png)

![bot36.png](https://github.com/sophiehn/MySuperBots/blob/master/Module%201-%20Create%20your%20First%20Bot/Images/RootDialog3.png)

The **MessageReceivedAsync** Task within Dialogs\RootDialog.cs sends a reply that echos back the user's message, prefixed with the text 'You sent' and ending in the text 'which was ## characters', where ## represents the number of characters in the user's message.

## Exercise 2: Debug your Application
In order to **run the application** and check out what it does, we will use the **Bot Framework Emulator** to test our Bot application. The Bot Framework provides a a channel emulator that lets you test calls to your Bot as if it were being called by the Bot Framework cloud service. You can find detailed documenation on how debuging with the Bot Framework Emulator works [here](https://docs.microsoft.com/en-us/bot-framework/debug-bots-emulator).

### Task 1
First, **start your Bot in Visual Studio** using a browser as the application host   

![bot4.png](https://github.com/sophiehn/MySuperBots/blob/master/Module%201-%20Create%20your%20First%20Bot/Images/StartProject.PNG) 

When the application is built and deployed, your **web browser will open and display the application default.htm file** (which is part of the Bot Application project). Feel free to modify the default.htm file to match the name and description of your Bot Application.

### Task 2
Here's the **Bot Application default.htm file in our Default Browser**: 

![bot5.png](https://github.com/sophiehn/MySuperBots/blob/master/Module%201-%20Create%20your%20First%20Bot/Images/MyFirstBotRunning.PNG)  

When using the emulator to test your Bot application, **make note of the port that the application is running on**, which in this example is port 3978. You will need this information to run the Bot Framework Emulator.  

![bot6.png](https://github.com/sophiehn/MySuperBots/blob/master/Module%201-%20Create%20your%20First%20Bot/Images/PortNumber.png)

### Task 3
Now open the **Bot Framework Emulator**: 

![bot7.png](https://github.com/sophiehn/MySuperBots/blob/master/Module%201-%20Create%20your%20First%20Bot/Images/Emulator.PNG) 

When working with the emulator with a bot running locally, you need:
- The **Url for your Bot** to set the **localhost:xxxx** pulled from the last step. > **Note: will need to add the path "/api/messages" to your URL** when using the Bot Application template.
- Leave the **Microsoft App Id** field **empty** for now
- Leave the **Microsoft App Password** field **empty** as well
- This will only work with the emulator running locally; in the cloud you would instead have to specify the appropriate URL and authentication values 

![bot8.png](https://github.com/sophiehn/MySuperBots/blob/master/Module%201-%20Create%20your%20First%20Bot/Images/Emulator2.png)
![bot81.png](https://github.com/sophiehn/MySuperBots/blob/master/Module%201-%20Create%20your%20First%20Bot/Images/Emulator3.PNG)
![bot82.png](https://github.com/sophiehn/MySuperBots/blob/master/Module%201-%20Create%20your%20First%20Bot/Images/EmulatorLog.PNG)

## Exercise 3: Chat with your Bot
Let's try to **chat with our Bot** to see what happens. 

### Task 1
**Type anything** in the Emulator Window and **press Enter** to see the response: 

![bot9.png](https://github.com/sophiehn/MySuperBots/blob/master/Module%201-%20Create%20your%20First%20Bot/Images/EmulatorMessage.PNG)

![bot10.png](https://github.com/sophiehn/MySuperBots/blob/master/Module%201-%20Create%20your%20First%20Bot/Images/EmulatorReply.PNG) 

![bot101.png](https://github.com/sophiehn/MySuperBots/blob/master/Module%201-%20Create%20your%20First%20Bot/Images/EmulatorReplyLog.png) 

Pay attention to the **JSON message** that appears in the Emulator as well as the **POST(200)**. That means that our message was suggesfully delivered to the Bot and it replied with a JSON message with the text property set to **"You sent hi which was 2 characters"**. 

This response was configured to be sent via the MessageController class in Visual Studio when the user sends a text message to the Bot.

### Task 2
Let's go back to **Visual Studio** and click the **Stop** button.

## Exercise 4: Publish your Bot to Azure
We are now going to publish our application to the cloud instead of running it locally. In order to do that, we will need an  **App Service running on Azure**, which we can create and deploy with many ways. Let's try the most simple one for this case, via Visual Studio.

### Task 1
Go to your solution in **Visual Studio** and:
- **Right Click** on the **Project=>Publish** 

![bot13.png](https://github.com/sophiehn/MySuperBots/blob/master/Module%201-%20Create%20your%20First%20Bot/Images/PublishToAzure.png) 

- In the **Publish Wizard** select the **Microsoft Azure App Service** , **Create new** and then click **Publish** 

![bot14.png](https://github.com/sophiehn/MySuperBots/blob/master/Module%201-%20Create%20your%20First%20Bot/Images/PublishToAzure2.png) 

- At this point, you may be promted to **Sign in to your Azure Subscription**. After doing so, you will notice what the Wizard has created an **App Name** for you. Since the name is not that important for now, just leave it as it is.

![bot18.png](https://github.com/sophiehn/MySuperBots/blob/master/Module%201-%20Create%20your%20First%20Bot/Images/PublishToAzure3.png) 

- Select the correct **Subcription** from the dropdown list (in case you have more than one) and after a few seconds you will be able to select the **Resource Group** you want your app to be deployed to. More details on the *importance of the Resource Groups* can be found [here](https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-group-overview). 
If you don't have a Resource Group already, go ahead and create one. Then select the **Resrouce Group** you just created:

![bot20.png](https://github.com/sophiehn/MySuperBots/blob/master/Module%201-%20Create%20your%20First%20Bot/Images/PublishToAzure4.png) 

- Now for the **App Service Plan**, the wizard has also created one for you. Click the **New** Button to configure it:

![bot21.png](https://github.com/sophiehn/MySuperBots/blob/master/Module%201-%20Create%20your%20First%20Bot/Images/PublishToAzure5.png) 

In this case I selected **North Europe** as a Region and **Free** as tier, so we don't get any excessive charges in our subscription. 

- We are ready to Publish our Bot to the Azure App Service we just created. 

![bot211.png](https://github.com/sophiehn/MySuperBots/blob/master/Module%201-%20Create%20your%20First%20Bot/Images/PublishToAzure6.png) 

- Once we hit that **Create** button, the deployment will start automatically, so we just sit back and wait:

![bot212.png](https://github.com/sophiehn/MySuperBots/blob/master/Module%201-%20Create%20your%20First%20Bot/Images/Deploying.png) 

![bot213.png](https://github.com/sophiehn/MySuperBots/blob/master/Module%201-%20Create%20your%20First%20Bot/Images/Publishing.png)

In the **Output Window** we can monitor the progress of the publishing process. If you don't see the Output Window, go to **View=>Output**.

- When the deployment is done, **a browser window will pop up** like the Debug step in the previous exercise. Now the difference is that **the application is no longer running on localhost**, it is hosted on a Web App Service on Azure: 

![bot214.png](https://github.com/sophiehn/MySuperBots/blob/master/Module%201-%20Create%20your%20First%20Bot/Images/Published.png)

And that was it. We now have a Bot with a public endpoint published on Azure and ready to be connected with a channel. But let's leave this part for later and **go back to Visual Studio to add some functionality to our Bot**.

## Next Step


- [Add Basic Functionality to your Bot]()
