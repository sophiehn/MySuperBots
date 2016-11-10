
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

1. [Create your first Bot Application in Visual Studio](https://github.com/sophiehn/MyBots/tree/master/1.%20Get%20Started%20With%20Your%20First%20Bot#exercise-1-create-your-first-bot-application-in-visual-studio)
1. Debug your Application
1. Chat with your Bot
1. Publish your Application to Azure

## [](exercise1)Exercise 1: Create your first Bot Application in Visual Studio

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

In order to **run it** and check out what it does, we will use the **Bot Framework Emulator** to test our Bot application. The Bot Framework provides a a channel emulator that lets you test calls to your Bot as if it were being called by the Bot Framework cloud service.

## [](exercise2)Exercise 2: Debug your Application

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

## [](exercise3)Exercise 3: Chat with your Bot

### Task 1
Let's try to **chat with our Bot** to see what happens. **Type anything** in the Emulator Window and **press Enter** to see the response: 

![bot9.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot9.png) 

![bot10.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot10.png) 

Pay attention to the **JSON message** that appears in the Emulator as well as the **status of our request (202 Accepted)**. That means that our message was suggesfully delivered to the Bot and it replied with a JSON message with the text property set to **"You sent hi which was 2 characters"**. 

This response was configured to be sent via the MessageController class in Visual Studio when the user sends a text message to the Bot.

### Task 2
Let's go back to **Visual Studio** and **Stop the Debugging**:

![bot11.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot11.png) 

![bot12.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot12.png) 


## [](exercise4)Exercise 4: Publish your Application to Azure


