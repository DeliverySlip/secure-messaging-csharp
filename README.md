# Secure Messaging C# Sample SDK
The Secure Messaging C# SDK allows clients to create applications to communicate with the DeliverySlip Secure
Messaging API. The client currently offers the most popular features and primarily is a reference for users
looking for guidelines on how to use the DeliverySlip API.

The SDK is being expanded to eventually encompass all features offered by the Secure Messaging API

# Prerequisites
The project has been soley developed on Windows and for Visual Studio. It is recommended you have
at mimum Visual Studio Community 2015 installed on a Windows machine.

# Setup
Compile the project simply by opening the solution file in Visual Studio and building from there. The
binaries will then be compiled into the `bin` folder at the project root

# Usage
For examples on how to use the supported features in the C# SDK, see the `CSharpMessengerTests` project. The
Unit test files demonstrate how to use the main features of the SDK. This includes examples on logging in,
sending messages, searching messages and uploading attachments.

# Testing
To run the unit tests yourself within Visual Studio, you will need to create an app.config and place it in the root
of the CSharpMessengerTests project. Then copy the following into it:
```
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <appSettings>
    <add key="ServiceCode" value="<servicecode>"/>
    <add key="Username" value="<username>"/>
    <add key="Password" value="<password>"/>
    <add key="RecipientEmail" value="<recipientemail>"/>
  </appSettings>
</configuration>
```
The ServiceCode is the unique code representing your portal. You can find this by logging into your account at
[https://w.deliveryslip.com](https://w.deliveryslip.com). Once you have logged in, copy the service code that will
have been appended onto the url after you have completed logging in. For additional help, contact support or your
portal admin.
The Username and Password are then the login credentials you use to access your secure email account. Add a recipient
email so that the mailing tests know where to send, this could be your same email if you would like, or another
email already part of the same portal.

# Developer Notes
* July 13, 2018 - Release of the C# client is just underway. The current code is stable but the priority is
currently on documentation of the code and repository to improve ease of access and involvement by others.
More to come!