
##  Overview

In this Module you will set up your pc with all the prerequisites and create your first Bot Application, using the template provided by the Microsoft Bot Framework.


##  Objectives

In this module, you'll:
- Create a **Bot Application**
- **Debug** your Bot Application locally 
- Use the Microsoft Bot Framework Channel Emulator to **chat with your Bot**
- **Publish your Bot** to an Azure **App Service**

##  Prerequisites

The following is required to complete this module:

- [Visual Studio Community 2015](https://www.visualstudio.com/products/visual-studio-community-vs) or greater - **Important:** Please update all VS extensions to their latest versions Tools->Extensions and Updates->Updates
- [Microsoft Bot Framework Channel Emulator](https://download.botframework.com/bf-v3/tools/emulator/publish.htm)
- [Bot Application template](http://aka.ms/bf-bc-vstemplate) - **Download and Save the zip file** to your Visual Studio 2015 templates directory which is traditionally in **"%USERPROFILE%\Documents\Visual Studio 2015\Templates\ProjectTemplates\Visual C#\"**
- An active Azure subscription

## Exercises
This module includes the following exercises:

1. [Create your first Bot Application in Visual Studio](https://github.com/sophiehn/MySuperBots/tree/master/1.%20Get%20Started%20With%20Your%20First%20Bot#exercise-1-create-your-first-bot-application-in-visual-studio)
1. [Debug your Application](https://github.com/sophiehn/MySuperBots/blob/master/1.%20Get%20Started%20With%20Your%20First%20Bot#exercise-2-debug-your-application)
1. [Chat with your Bot](https://github.com/sophiehn/MySuperBots/blob/master/1.%20Get%20Started%20With%20Your%20First%20Bot#exercise-3-chat-with-your-bot)
1. [Publish your Bot to Azure](https://github.com/sophiehn/MySuperBots/blob/master/1.%20Get%20Started%20With%20Your%20First%20Bot#exercise-4-publish-your-bot-to-azure)

## Exercise 1: Create your first Bot Application in Visual Studio

### Task 1 
Open Visual Studio 2015 and Create a **New Project** by selecting **Visual C#=>Bot Application** from the Templates. **Name** your Project **AzureAwesomeBot**: 

![bot1.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot1.png) 

The template is a fully functional Echo Bot that takes the user's text utterance as input and returns it as output.
 
### Task 2 
Let's take a look for a moment on what the template has created for us: Check the **Solution Explorer** to see the resources that were created    

![bot2.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot2.png)


### Task 3 
**Open** the **Controllers Folder** and double-click on the **MessagesController.cs** file. There you can see the MessagesController Class with an aync **Post Method**.    

![bot3.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot3.png)  

This method contains all the core functionality of the Bot Template App. In this case the code takes the message text for the user, then creates a reply message using the CreateReplyMessage function. The BotAuthentication decoration on the method is used to validate your Bot Connector credentials over HTTPS. 


## Exercise 2: Debug your Application

In order to **run it** and check out what it does, we will use the **Bot Framework Emulator** to test our Bot application. The Bot Framework provides a a channel emulator that lets you test calls to your Bot as if it were being called by the Bot Framework cloud service.

### Task 1
First, **start your Bot in Visual Studio** using a browser as the application host   

![bot4.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot4.png) 

When the application is built and deployed the **web browser will open and display the application Default.htm file** (which is part of the Bot Application project). Feel free to modify the Default.html file to match the name and description of your Bot Application.

### Task 2
Here's the **Bot Application Default.htm file in our Default Browser**: 

![bot5.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot5.png)  

When using the emulator to test your Bot application, **make note of the port that the application is running on**, which in this example is port 3978. You will need this information to run the Bot Framework Emulator.  

![bot6.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot6.png)

### Task 3
Now **open the Bot Framework Emulator**. When working with the emulator with a bot running locally, you need:
- The **Url for your Bot** to set the **localhost:xxxx** pulled from the last step. > **Note: will need to add the path "/api/messages" to your URL** when using the Bot Application template.
- Leave the **Microsoft App Id** field **empty** for now
- Leave the **Microsoft App Password** field **empty** as well
- This will only work with the emulator running locally; in the cloud you would instead have to specify the appropriate URL and authentication values 

![bot7.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot7.png) 
![bot8.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot8.png)

## Exercise 3: Chat with your Bot

Let's try to **chat with our Bot** to see what happens. 

### Task 1
**Type anything** in the Emulator Window and **press Enter** to see the response: 

![bot9.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot9.png) 

![bot10.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot10.png) 

Pay attention to the **JSON message** that appears in the Emulator as well as the **status of our request (202 Accepted)**. That means that our message was suggesfully delivered to the Bot and it replied with a JSON message with the text property set to **"You sent hi which was 2 characters"**. 

This response was configured to be sent via the MessageController class in Visual Studio when the user sends a text message to the Bot.

### Task 2
Let's go back to **Visual Studio** and **Stop the Debugging**:

![bot11.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot11.png) 

![bot12.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot12.png) 


## Exercise 4: Publish your Bot to Azure

We are now going to publish our application to the cloud and stop running it locally.

### Task 1
- Go to [Azure Management Portal](http://www.portal.azure.com) and **Sign In** with your Azure Account. 
- **Create** a new **App Service** by selecting **+NEW=>Web+Mobile=>Web App** and filling the form with the nesseccary fields:


![bot15.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot15.png) 

- For the **App Name** use a **unique** name for your web app
- **Create a new Resource Group** for your app (or use an existing one, it is totaly up to you)


![bot16.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot16.png) 

- **Create a new Service Plan**, choose the **Location most close to you** (in this case North Europe) and the **Basic Pricing Tier** and click **OK**


![bot17.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot17.png) 

- Now all the parameters are set up and we are ready to create our Web App. Click the **Pin to Dashboard checkbox** and then **OK**

- If everything goes OK, in a few seconds you will be able to **see your Web App** in the main **Dashboard**:


![bot19.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot19.png)

## Task 2

Let's go back to **Visual Studio** and **publish** our Bot to the Web App we just created:
- **Right Click on the Project=>Publish** 
![bot13.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot13.png) 

- As **Publish Target** select the **Microsoft Azure App Service** and click **Next** 

![bot14.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot14.png) 

- Select your **Subcription** and after a few seconds you will be able to see the Resource Group you created for this app
- Select the **Resrouce Group** and click **OK** 
![bot18.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot18.png) 

- In the **Connection Tab** leave everything as it is and click **Publish** 

![bot20.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot20.png) 

- After a while **a browser will open** with your Application like the Debug step in the previous exercise. Now the difference is that **the application is no longer running on localhost**, it is hosted on a Web App on Azure. **You can see it by looking at the URL of the page**: 

![bot21.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot21.png)

And that was it. We now have a Bot published on Azure and ready to be connected with a channel. 

## Next Steps

- [Add Functionality to your Bot](https://github.com/sophiehn/MySuperBots/blob/master/2.%20AzureAwesomeBot)
- [Register your Bot with the Microsoft Bot Framework](https://github.com/sophiehn/MySuperBots/tree/master/3.%20Register%20your%20Bot%20with%20the%20Microsoft%20Bot%20Framework)
- [Configure your Bot Channels](https://github.com/sophiehn/MySuperBots/tree/master/4.%20Configure%20your%20Bot%20Channels)

