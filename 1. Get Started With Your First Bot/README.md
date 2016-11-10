
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

Open Visual Studio 2015 and Create a **New Project** by selecting **Visual C#=>Bot Application** from the Templates. **Name** your Project **AzureAwesomeBot**:

![bot1.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot1.png)

The template is a fully functional Echo Bot that takes the user's text utterance as input and returns it as output.

Let's take a look for a moment on what the template has created for us:

Check the **Solution Explorer** to see the resources that were created
![bot2.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot2.png)

**Open** the **Controllers Folder** and double-click on the **MessagesController.cs** file
There you can see the MessagesController Class with an aync **Post Method**. This method contains all the core functionality of the Bot Template App. In this case the code takes the message text for the user, then creates a reply message using the CreateReplyMessage function. The BotAuthentication decoration on the method is used to validate your Bot Connector credentials over HTTPS.

![bot3.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot3.png)

In order to **run it** and check out what it does, we will use the **Bot Framework Emulator** to test our Bot application. The Bot Framework provides a a channel emulator that lets you test calls to your Bot as if it were being called by the Bot Framework cloud service. 

First, start your Bot in Visual Studio using a browser as the application host.

![bot4.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot4.png)
![bot5.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot5.png)
![bot6.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot6.png)
![bot7.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot7.png)
![bot8.png](http://i292.photobucket.com/albums/mm38/iCe-quEen99/bot8.png)

