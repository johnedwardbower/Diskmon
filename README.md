# Diskmon
Monitoring Diskspace and reporting to Slack channels

In Slack:
  Go to App the app section
  Create the webhook using Add configuration
  Grab the webHook URL

In Visual Studio
  Create a new windows forms project
  Add Newtonsoft.Json
  copy in the code from progam.cs but change the WebHook URL to yours
  I have set the code to report when diskspace is less than 10GB
  Also checks in on Saturdays to let you know the server is reporting


Add the executable and supporting files to a server that you want to monitor
  Make sure the server has .NETFramework,Version=v4.5
  Add to scheduled tasks, you do one and then export the task and import on the others
  
Ref: lots of this code was taken from Slack's help page here https://api.slack.com/incoming-webhooks

