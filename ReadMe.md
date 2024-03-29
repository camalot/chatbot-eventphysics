<!-- https://imgur.com/a/2wtlAkL -->

# CHATBOT-EVENTPHYSICS
[![Event Physics Overlay](https://github.com/camalot/chatbot-eventphysics/actions/workflows/build.yml/badge.svg)](https://github.com/camalot/chatbot-eventphysics/actions/workflows/build.yml)


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

- Download the latest zip file from [Releases](https://github.com/camalot/chatbot-eventphysics/releases/latest)
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

### GENERAL  

[![](https://i.imgur.com/R9pGTmhl.png)](https://i.imgur.com/R9pGTmh.png)  

| ITEM | DESCRIPTION | DEFAULT | 
| ---- | ----------- | ------- | 
| `Image Scale` | The profile image is 300x300, this is the percentage scale of that. | `10` |  
| `Item Shape` | The underlying body shape of the image | `circle` |  
| `Screen Map` | Screen map that adds objects for the items to bounce off of | `none` |  
| `Maximum Items Per Event` | The maximum number of items that will be generated by a given event. | `100` |  
| `Total Maximum Items` | The maximum number of items that will be allowed on screen. | `500` |  
| `Item Life Span (seconds)` | The global life | 30 |  
| `Screen Height (pixels)` | This is the Height of your stream canvas size |  1080 |  
| `Screen Width (pixels)` | This is the Width of your stream canvas size | 1080 |  
| `CLEAR ALL ITEMS` | Removes all current items on the screen |  |  

### ADVANCED

[![](https://i.imgur.com/ZlJKIQOl.png)](https://i.imgur.com/ZlJKIQO.png)  

| ITEM | DESCRIPTION | DEFAULT | 
| ---- | ----------- | ------- | 
| `Show Map Bodies` | If a map is loaded, this will show the map items | `false` | 
| `Horizontal Force` | The horizontal force to apply as the item enters the screen | `1.5` | 
| `Vertical Force` | The vertical force to apply as the item enters the screen | `1.0` | 
| `Item Density` | Defines the density of the body, that is its mass per unit area. | `1.0` | 
| `Item Friction` | Defines the friction of the item | `3.0` | 
| `Item Air Friction` | Defines the air resistance of the item | `5.0` | 
| `Item Restitution` | Defines the elasticity of the item. | `70` | 



### CHAT

[![](https://i.imgur.com/XWTpcLzl.png)](https://i.imgur.com/XWTpcLz.png)  

| ITEM | DESCRIPTION | DEFAULT | 
| ---- | ----------- | ------- | 
| `Enable Chat` | If `true`, chat messages will trigger an item event. | `true` | 
| `Item Life Span (seconds)` | The time that the item will live on screen (Overrides the Global if not 0) | `0` | 
| `Use Global Chat Cooldown` | If `true`, cooldown is applied to all chat messages; otherwise, cooldown is applied per person. | `false` | 
| `Chat Cooldown (seconds)` | The cooldown for event to trigger from chat events. | `0` | 
| `Item Multiplier` | Multiplier to apply to the items from chat. 1 chat message = 1 user icon | `1` | 
| `Enable Chat Emotes` | If `true`, message emotes drop on screen as events. | `true` | 
| `Emote Multiplier` | Multiplier to apply to the items from chat. | `1` | 
| `Enable Chat Emoji` | If `true`, message emoji drop on screen as events. | `true` | 
| `Emoji Multiplier` | Multiplier to apply to the items from chat. | `1` | 
| `TEST CHAT MESSAGE EVENT` | Sends a chat message test event |  |  
| `TEST CHAT EMOTE EVENT` | Sends a chat emote test event |  |  
| `TEST CHAT EMOJI EVENT` | Sends a chat emoji test event |  |  


### FOLLOWERS

[![](https://i.imgur.com/7NStvbOl.png)](https://i.imgur.com/7NStvbO.png)  

| ITEM | DESCRIPTION | DEFAULT | 
| ---- | ----------- | ------- | 
| `Enable Follows` | If `true`, follows will trigger an item event. | `true` | 
| `Item Life Span (seconds)` | The time that the item will live on screen (Overrides the Global if not 0) | `0` | 
| `Cooldown (seconds)` | The cooldown for event to trigger from follow events. | `0` | 
| `Item Multiplier` | Multiplier to apply to the items from follows. 1 follow = 1 user icon | `1` | 
| `TEST TWITCH FOLLOW EVENT` | Sends a twitch follow test event |  |  

### DONATIONS

[![](https://i.imgur.com/OLhg6UWl.png)](https://i.imgur.com/OLhg6UW.png)  

| ITEM | DESCRIPTION | DEFAULT | 
| ---- | ----------- | ------- | 
| `Enable Donations` | If `true`, donations will trigger an item event. | `true` | 
| `Item Life Span (seconds)` | The time that the item will live on screen (Overrides the Global if not 0) | `0` | 
| `Cooldown (seconds)` | The cooldown for event to trigger from donation events. | `0` | 
| `Donation Minimum` | The minimum dollar amount that will trigger an event. | `1` | 
| `Item Multiplier` | Multiplier to apply to the items from donations. 1 dollar = 1 user icon | `1` | 
| `Donation Percentage` | Only count a percentage of the donation as items. | `100` | 
| `TEST STREAMLABS DONATION EVENT` | Sends a donation test event |  |  



### SUBSCRIPTIONS

[![](https://i.imgur.com/rdKdcFMl.png)](https://i.imgur.com/rdKdcFM.png)  

| ITEM | DESCRIPTION | DEFAULT | 
| ---- | ----------- | ------- | 
| `Enable Subscriptions` | If `true`, subscriptions will trigger an item event. | `true` | 
| `Item Life Span (seconds)` | The time that the item will live on screen (Overrides the Global if not 0) | `0` | 
| `Cooldown (seconds)` | The cooldown for event to trigger from subscription events. | `0` | 
| `Months Minimum` | The minimum months subscribed that will trigger an event. | `1` | 
| `Gifted Minimum` | The minimum gifted amount that will trigger an event. | `1` | 
| `Item Multiplier` | Multiplier to apply to the items from donations. 1 dollar = 1 user icon | `1` | 
| `Subscription Percentage` | Only count a percentage of the months subscribed as items. | `100` | 
| `TEST SUB/RESUB EVENT` | Sends a sub/resub test event |  |  
| `TEST GIFTED SUB EVENT` | Sends a gifted sub test event |  |  
| `TEST COMMUNITY GIFTED EVENT` | Sends a community gifted test event |  |  



### HOST/RAID

[![](https://i.imgur.com/qS4ubH0l.png)](https://i.imgur.com/qS4ubH0.png)  

| ITEM | DESCRIPTION | DEFAULT | 
| ---- | ----------- | ------- | 
| `Enable Host/Raid` | If `true`, hosts/raids will trigger an item event. | `true` | 
| `Item Life Span (seconds)` | The time that the item will live on screen (Overrides the Global if not 0) | `0` | 
| `Viewer Minimum` | The minimum number of viewers that will trigger an event. | `1` | 
| `Cooldown (seconds)` | The cooldown for event to trigger from subscription events. | `0` | 
| `Item Multiplier` | Multiplier to apply to the items from viewers. 1 dollar = 1 user icon | `1` | 
| `Host / Raid Percentage` | Only count a percentage of the viewers from the host/raid as items. | `100` | 
| `TEST HOST/RAID EVENT` | Sends a host/raid test event |  |  

### BITS/CHEER

[![](https://i.imgur.com/JkgSwpzl.png)](https://i.imgur.com/JkgSwpz.png)  

| ITEM | DESCRIPTION | DEFAULT | 
| ---- | ----------- | ------- | 
| `Enable Cheer` | If `true`, bits/cheer will trigger an item event. | `true` | 
| `Item Life Span (seconds)` | The time that the item will live on screen (Overrides the Global if not 0) | `0` | 
| `Cooldown (seconds)` | The cooldown for event to trigger from subscription events. | `0` | 
| `Cheer Minimum` | The minimum number of bits that will trigger an event. | `1` | 
| `Item Multiplier` | Multiplier to apply to the items from cheers. 1 dollar = 1 user icon | `1` | 
| `Cheer Percentage` | Only count a percentage of the bits from the host/raid as items. | `100` | 
| `TEST CHEER EVENT` | Sends a cheer test event |  |  


### API KEYS

[![](https://i.imgur.com/t8svbeEl.png)](https://i.imgur.com/t8svbeE.png)  

| ITEM | DESCRIPTION | DEFAULT | 
| ---- | ----------- | ------- | 
| `Streamlabs Socket Token` | Open the streamlabs site, go to `Settings -> API Settings -> API Tokens` and copy `Your Socket API Token` | `` | 
| `GET YOUR SOCKET TOKEN HERE` | Open streamlabs page to get token |  |  
| `Twitch Client-ID` | Register a new application, give it a unique name. Set the Redirect URL to `http://localhost`, set the category to Chat Bot. Click create and copy the `CLIENT-ID` | `` | 
| `GET YOUR TWITCH CLIENT ID HERE` | Create twitch app to get client-id |  |  


[![](https://i.imgur.com/IVc133Ul.png)](https://i.imgur.com/IVc133U.png) 

[![](https://i.imgur.com/R3VD0D8l.png)](https://i.imgur.com/R3VD0D8.png) 


### INFORMATION  

[![](https://i.imgur.com/ZyWocvRl.png)](https://i.imgur.com/ZyWocvR.png)  

| ITEM | DESCRIPTION | 
| ---- | ----------- | 
| `DONATE` | If you want to support, you can donate | 
| `FOLLOW ME ON TWITCH` | Opens a link to follow me on Twitch | 
| `OPEN README` | Opens the link to this document | 
| `CHECK FOR UPDATES` | Will Check if there are updates to the  Overlay Script. See below for more info. | 
| `SAVE SETTINGS` | Save any changes to the EventPhysics Overlay settings | 


## UPDATER

> **NOTE:** You must launch from within Streamlabs Chatbot. 

The application will open, and if there is an update it will tell you. You click on the `Download & Update` button. 

> **NOTE:** Applying the update will close down Streamlabs Chatbot. It will reopen after the update is complete.

[![](https://i.imgur.com/7VOsLXtl.png)](https://i.imgur.com/7VOsLXt.png)

## OVERLAY SETUP IN OBS / SLOBS 

- Add a new `Browser Source` in OBS / SLOBS  
[![](https://i.imgur.com/TAMQkeql.png)](https://i.imgur.com/TAMQkeq.png)
- Set as a `Local File` and choose the `overlay.html` in the `EventPhysics` script directory. You can easily get to the script directory location from right clicking on `EventPhysics` and choose `Open Script Folder`.
- Set the `width` and `height` to the resolution of your `Base (Canvas) Resolution`. 
- Add any additional custom CSS that you would like to add.
- Check both `Shutdown source when not visible` and `Refresh browser when scene becomes active`.  
[![](https://i.imgur.com/nouqPh0l.png)](https://i.imgur.com/nouqPh0.png)