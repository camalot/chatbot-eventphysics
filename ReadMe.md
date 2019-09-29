<!-- https://imgur.com/a/2wtlAkL -->

# CHATBOT-EVENTPHYSICS

A script + overlay for streamlabs chatbot and OBS/SLOBS/XSPLIT and twitch/mixer/youtube that "rains down" user profile images when events occur.

### SUPPORTED EVENTS

- TWITCH
  - HOST / RAID
  - CHEER
  - FOLLOW
  - SUBSCRIPTION (new/gift/resub)
  - CHAT MESSAGE
- MIXER
  - FOLLOW
  - CHAT MESSAGE
- YOUTUBE
  - SUBSCRIBE
  - CHATMESSAGE
- STREAMLABS
  - DONATION


## REQUIREMENTS

- Install [StreamLabs Chatbot](https://streamlabs.com/chatbot)
- [Enable Scripts in StreamLabs Chatbot](https://github.com/StreamlabsSupport/Streamlabs-Chatbot/wiki/Prepare-&-Import-Scripts)
- [Microsoft .NET Framework 4.7.2 Runtime](https://dotnet.microsoft.com/download/dotnet-framework/net472) or later

## INSTALL

- Download the latest zip file from [Releases](https://github.com/camalot/chatbot-shoutout/releases/latest)
- Right-click on the downloaded zip file and choose `Properties`
- Click on `Unblock`  
[![](https://i.imgur.com/vehSSn7l.png)](https://i.imgur.com/vehSSn7.png)  
  > **NOTE:** If you do not see `Unblock`, the file is already unblocked.
- In Chatbot, Click on the import icon on the scripts tab.  
  ![](https://i.imgur.com/16JjCvR.png)
- Select the downloaded zip file
- Right click on `Shoutout Overlay` and select `Insert API Key`. Click yes on the dialog.  
[![](https://i.imgur.com/AWmtHKFl.png)](https://i.imgur.com/AWmtHKF.png)  

## CONFIGURATION

Make sure the script is enabled  
[![](https://i.imgur.com/ejTRvJGl.png)](https://i.imgur.com/ejTRvJG.png)  

Click on the script in the list to bring up the configuration.

### GENERAL SETTINGS  

[![](https://i.imgur.com/SrnPnx9l.png)](https://i.imgur.com/SrnPnx9.png)  

| ITEM | DESCRIPTION | DEFAULT | 
| ---- | ----------- | ------- | 
| `Image Scale` | The profile image is 300x300, this is the percentage scale of that. | `10` |  
| `Item Shape` | The underlying body shape of the image | `circle` |  
| `Screen Map` | Screen map that adds objects for the items to bounce off of | `none` |  
| `Maximum Items Per Event` | The maximum number of items that will be generated by a given event. | `100` |  
| `Item Life Span (seconds)` | The global life | 30 |  
| `Screen Height (pixels)` | This is the Height of your stream canvas size |  1080 |  
| `Screen Width (pixels)` | This is the Width of your stream canvas size | 1080 || `CLEAR ALL ITEMS` | Removes all current items on the screen |  |  

### ADVANCED SETTINGS

[![](https://i.imgur.com/ZlJKIQOl.png)](https://i.imgur.com/ZlJKIQO.png)  

| ITEM | DESCRIPTION | DEFAULT | 
| ---- | ----------- | ------- | 
| `Show Map Bodies` | If a map is loaded, this will show the map items | `false` | 
| `Horizontal Force` | The horizontal force to apply as the item enters the screen | `1.5` | 
| `Vertical Force` | The vertical force to apply as the item enters the screen | `1.0` | 
