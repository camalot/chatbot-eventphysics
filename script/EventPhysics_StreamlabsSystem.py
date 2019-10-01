# -*- coding: utf-8 -*-
#---------------------------------------
#   Import Libraries
#---------------------------------------
import sys
import clr
import json
import codecs
import os
import datetime
import random

# point at lib folder for classes / references
sys.path.append(os.path.join(os.path.dirname(__file__), "..\libs"))
clr.AddReferenceToFileAndPath(os.path.join(os.path.dirname(
    os.path.realpath(__file__)), "libs\StreamlabsEventReceiver.dll"))
from StreamlabsEventReceiver import StreamlabsEventClient

clr.AddReference("IronPython.SQLite.dll")
clr.AddReference("IronPython.Modules.dll")

#---------------------------------------
#   [Required] Script Information
#---------------------------------------
ScriptName = "EventPhysics Overlay"
Website = "https://github.com/camalot/chatbot-eventphysics"
Description = "An overlay that will rain down user icons for events that occur in your stream."
Creator = "DarthMinos"
Version = "1.0.0-snapshot"
Repo = "camalot/chatbot-eventphysics"

DonateLink = "https://paypal.me/camalotdesigns"
ReadMeFile = "https://github.com/" + Repo + "/blob/develop/ReadMe.md"
SettingsFile = os.path.join(os.path.dirname(os.path.realpath(__file__)), "settings.json")
TestAccounts = [
    "DarthMinos",
    "PhillyRampage",
    "Sabborn",
    "KneeKappa",
    "OneEyeWonders",
    "MoMorific",
    "PAD__E",
    "ItzOpie",
    "iZirc",
    "Twooff",
    "Stuntmustard"]

SLConnected = False
ScriptSettings = None
EventReceiver = None
Debug = False
LAST_PARSED = 1
Initialized = False

def JsonConverter(o):
    if isinstance(o, datetime.datetime):
        return o.__str__()


class Settings(object):
    """ Class to hold the script settings, matching UI_Config.json. """

    def __init__(self, settingsfile=None):
        """ Load in saved settings file if available else set default values. """
        try:
            self.StreamlabsToken = ""
            self.TwitchClientId = ""
            self.GlobalObjectTTL = 30
            self.ImageScale = 10
            self.ItemModel = "circle"
            self.MaxItems = 100
            self.GlobalMaxItems = 500
            self.ScreenMap = "none"
            self.CustomMapName = ""
            self.HorizontalForce = 1.5
            self.VerticalForce = 1
            self.ItemDensity = 1
            self.ItemFriction = 3
            self.ItemAirFriction = 5
            self.ItemRestitution = 70
            self.ScreenHeight = 1080
            self.ScreenWidth = 1920

            self.EnableChat = False
            self.ChatObjectTTL = 0
            self.UseGlobalChatCooldown = False
            self.ChatCooldown = 10
            self.ChatMultiplier = 1

            self.ChatEnableEmotes = True
            self.ChatEmoteMultiplier = 1

            self.ChatEnableEmoji = True
            self.ChatEmojiMultiplier = 1

            self.EnableFollow = True
            self.FollowObjectTTL = 0
            self.FollowMultiplier = 1
            self.FollowCooldown = 0

            self.EnableCheer = True
            self.CheerObjectTTL = 0
            self.CheerPercent = 100
            self.CheerMultiplier = 1
            self.CheerMinimum = 100
            self.CheerCooldown = 0

            self.EnableDonation = True
            self.DonationObjectTTL = 0
            self.DonationPercent = 100
            self.DonationMultiplier = 1
            self.DonationCooldown = 0
            self.DonationMinimum = 1

            self.EnableHost = True
            self.HostObjectTTL = 0
            self.HostPercent = 100
            self.HostMinimum = 1
            self.HostCooldown = 0
            self.HostMultiplier = 1

            self.EnableSubscribe = True
            self.SubscriptionObjectTTL = 0
            self.SubscriptionPercent = 100
            self.SubscriptionMultiplier = 1
            self.SubscriptionCooldown = 0
            self.SubscriptionMinimum = 1
            self.SubscriptionGiftedMinimum = 1

            with codecs.open(settingsfile, encoding="utf-8-sig", mode="r") as f:
                fileSettings = json.load(f, encoding="utf-8")
                self.__dict__.update(fileSettings)
        except Exception as e:
            Parent.Log(ScriptName, str(e))

    def Reload(self, jsonData):
        fileLoadedSettings = json.loads(jsonData, encoding="utf-8")
        self.__dict__.update(fileLoadedSettings)




def SendWebsocketData(eventName, payload):
    dump = json.dumps(payload, default=JsonConverter)
    if dump != "null" and dump != "" and payload is not None and payload != "" and payload != "null":
        Parent.BroadcastWsEvent(eventName, dump)
    return
def SendClearItemsEvent():
    SendWebsocketData("EVENT_EP_CLEARALL", {"clear": True})
    return

def SendSettingsUpdate():
    SendWebsocketData("EVENT_EP_SETTINGS", ScriptSettings.__dict__)
    return
def SendEventMessageEvent(payload):
    if payload is not None:
        SendWebsocketData("EVENT_EP_EVENTMESSAGE", payload)
    return


#---------------------------------------
#   [Required] Initialize Data / Load Only
#---------------------------------------


def Init():
    global ScriptSettings
    global EventReceiver
    global SLConnected
    global Initialized
    global TestAccounts

    if Initialized:
        return

    TestAccounts.append(Parent.GetChannelName())

    ScriptSettings = Settings(SettingsFile)
    SendSettingsUpdate()

    EventReceiver = StreamlabsEventClient()
    EventReceiver.StreamlabsSocketConnected += EventReceiverConnected
    EventReceiver.StreamlabsSocketDisconnected += EventReceiverDisconnected
    EventReceiver.StreamlabsSocketEvent += EventReceiverEvent
    Parent.Log(ScriptName, "Loaded")

    if ScriptSettings.StreamlabsToken and not EventReceiver.IsConnected:
        Parent.Log(ScriptName, "Connecting")
        EventReceiver.Connect(ScriptSettings.StreamlabsToken)
    Initialized = True
    return

def Unload():
    global EventReceiver
    global Initialized
    if EventReceiver is not None:
        EventReceiver.StreamlabsSocketConnected -= EventReceiverConnected
        EventReceiver.StreamlabsSocketDisconnected -= EventReceiverDisconnected
        EventReceiver.StreamlabsSocketEvent -= EventReceiverEvent
        if EventReceiver.IsConnected:
            EventReceiver.Disconnect()
        EventReceiver = None
    Initialized = False
    return

def ScriptToggled(state):
    if state:
        Init()
    else:
        Unload()
    return

# ---------------------------------------
# Chatbot Save Settings Function
# ---------------------------------------
def ReloadSettings(jsondata):
    Unload()
    Init()
    return


def Execute(data):
    if data.IsChatMessage():
        cooldownUserName = ScriptName + "-" + data.UserName
        cooldownName = ScriptName + "-chat" if ScriptSettings.UseGlobalChatCooldown else cooldownUserName

        eventPayload = CreateChatPayload(data)
        platform = "twitch_account" if data.IsFromTwitch() else "mixer_account" if data.IsFromMixer() else "youtube_account" if data.IsFromYoutube() else "discord_account"
        if eventPayload and not Parent.IsOnCooldown(ScriptName, cooldownName):
            Parent.AddCooldown(ScriptName, cooldownName, ScriptSettings.ChatCooldown)
            SendEventMessageEvent({
                "For": platform,
                "Message": eventPayload,
                "Type": "chat"
            })
    return


def Tick():
    return
def CreateChatPayload(data):
    if data:
        # @badge-info=subscriber/17;badges=broadcaster/1,subscriber/12;color=#FF4700;display-name=DarthMinos;emote-only=1;emotes=1654895:11-20/1455566:84-94/300710661:116-126/34:138-146/1983580:0-9/1782816:38-48/1713825:159-169/1713813:186-198/300116344:200-210/300094809:22-29/161209:75-82/300764452:107-114/300354391:128-136/1713819:171-184/379085:31-36/300029735:62-73/1148689:96-105/624501:148-157/2126505:50-60;flags=;id=ea9fc2a7-7732-467d-9293-aca83f5cab21;mod=0;room-id=58491861;subscriber=1;tmi-sent-ts=1569256675845;turbo=0;user-id=58491861;user-type= :darthminos!darthminos@darthminos.tmi.twitch.tv PRIVMSG #darthminos :darthm7POG twooffHype bobdadGG sabDAB kneeka1Deep become1Goat become1Doggo kgtvHYPE missbe4Love itzopiHype momoriBD FightPepper PrideGive SwiftRage PurpleStar HolidayTree HolidayPresent HolidayCookie TwitchSings
        tags = dict(item.split("=") for item in data.RawData.split(" ")[0].split(";"))
        emotes = list(dict.fromkeys(d.split(':')[0] for d in tags.get('emotes').split('/')))

        return {
            "Name": str(data.UserName),
            "Emotes": emotes,
            "Message": str(data.Message),
            "Count": 1 * int(ScriptSettings.ChatMultiplier),
            "EmoteCount": 1 * int(ScriptSettings.ChatEmoteMultiplier),
            "EmojiCount": 1 * int(ScriptSettings.ChatEmojiMultiplier),
            "TTL": int(ScriptSettings.GlobalObjectTTL if ScriptSettings.ChatObjectTTL == 0 else ScriptSettings.ChatObjectTTL)
        }
    else:
        return None
def CreateHostPayload(data):
    try:
        if data:
            result = {
                "Name": str(data.Name),
                "IsTest": bool(data.IsTest),
                "IsLive": bool(data.IsLive),
                "IsRepeat": bool(data.IsRepeat),
                "Viewers": int(data.Raiders if hasattr(data, 'Raiders') else data.Viewers),
                "Count": int(data.Raiders if hasattr(data, 'Raiders') else data.Viewers) * int(ScriptSettings.HostMultiplier),
                "TTL": int(ScriptSettings.GlobalObjectTTL if ScriptSettings.HostObjectTTL == 0 else ScriptSettings.HostObjectTTL)
            }
            return result
        else:
            return None
    except Exception as e:
        Parent.Log(ScriptName, str(e))
        return None
def CreateFollowPayload(data):
    if data:
        return {
            "Name": str(data.Name),
            "IsTest": bool(data.IsTest),
            "IsLive": bool(data.IsLive),
            "IsRepeat": bool(data.IsRepeat),
            "Id": str(data.Id if hasattr(data, 'Id') else data.Name.lower()),
            "Count": int(ScriptSettings.FollowMultiplier),
            "TTL": int(ScriptSettings.GlobalObjectTTL if ScriptSettings.FollowObjectTTL == 0 else ScriptSettings.FollowObjectTTL)
        }
    else:
        return None
def CreateTwitchSubscribePayload(data):
    if data:
        return {
            "Name": str(data.Name),
            "IsTest": bool(data.IsTest),
            "IsLive": bool(data.IsLive),
            "IsRepeat": bool(data.IsRepeat),
            "DisplayName": data.DisplayName,
            "Gifter": data.Gifter,
            "Message": str(data.Message),
            "Months": str(data.Months),
            "StreakMonths": (data.StreakMonths if hasattr(data, 'StreakMonths') else 1),
            "Count":  int(data.Months if hasattr(data, 'Months') else 1) * int(ScriptSettings.SubscriptionMultiplier),
            "SubPlan": data.SubPlan,
            "SubPlanName": data.SubPlanName,
            "SubType": data.SubType,
            "TTL": int(ScriptSettings.GlobalObjectTTL if ScriptSettings.SubscriptionObjectTTL == 0 else ScriptSettings.SubscriptionObjectTTL)
        }
    else:
        return None
def CreateTwitchMysterySubscriptionPayload(data):
    if data:
        return {
            "Name": str(data.Name),
            "IsTest": bool(data.IsTest),
            "IsLive": bool(data.IsLive),
            "IsRepeat": bool(data.IsRepeat),
            "Amount": int(data.Amount),
            "Count": int(data.Amount) * int(ScriptSettings.SubscriptionMultiplier),
            "Gifter": data.Gifter,
            "GifterDisplayName": data.GifterDisplayName,
            "SubPlan": data.SubPlan,
            "SubType": data.SubType,
            "TTL": int(ScriptSettings.GlobalObjectTTL if ScriptSettings.SubscriptionObjectTTL == 0 else ScriptSettings.SubscriptionObjectTTL)
        }
    else:
        return None
def CreateTwitchCheerPayload(data):
    if data:
        return {
            "Name": str(data.Name),
            "IsTest": bool(data.IsTest),
            "IsLive": bool(data.IsLive),
            "IsRepeat": bool(data.IsRepeat),
            "Amount": int(data.Amount),
            "Count": int(data.Amount) * int(ScriptSettings.CheerMultiplier),
            "Message": data.Message,
            "TTL": int(ScriptSettings.GlobalObjectTTL if ScriptSettings.CheerObjectTTL == 0 else ScriptSettings.CheerObjectTTL)
        }
    else:
        return None
def CreateDonationPayload(data):
    if data:
        Parent.Log(ScriptName, "Creating donation payload")
        pl = {
            "Name": str(data.Name),
            "IsTest": bool(data.IsTest),
            "IsLive": bool(data.IsLive),
            "IsRepeat": bool(data.IsRepeat),
            "Amount": float(data.Amount if hasattr(data, 'Amount') else 1),
            "Count": float(data.Amount if hasattr(data, 'Amount') else 1) * float(ScriptSettings.DonationMultiplier),
            "Currency": str(data.Currency),
            "FromId": data.FromId,
            "FromName": data.FromName,
            "Message": str(data.Message),
            "PaymentSource": data.PaymentSource,
            "TTL": int(ScriptSettings.GlobalObjectTTL if ScriptSettings.DonationObjectTTL == 0 else ScriptSettings.DonationObjectTTL)
        }
        return pl
    else:
        return None
###############################
#   EVENT RECEIVER HANDLERS   #
###############################
def EventReceiverConnected(sender, args):
    Parent.Log(ScriptName, "Streamlabs event websocket connected")
    return


def EventReceiverDisconnected(sender, args):
    Parent.Log(ScriptName, "Streamlabs event websocket disconnected")
    return


def EventReceiverEvent(sender, args):
    global ScriptSettings
    global LAST_PARSED
    evntdata = args.Data
    if LAST_PARSED == evntdata.GetHashCode() or evntdata is None:
        return  # Fixes a strange bug where Chatbot registers to the DLL multiple times
    LAST_PARSED = evntdata.GetHashCode()
    Parent.Log(ScriptName, "type: " + evntdata.Type)

    eventPayload = None
    cooldownName = ScriptName
    cooldownTime = 0
    isOnCooldown = False
    meetsMinimum = True
    for message in evntdata.Message:
        if message:
            if evntdata.Type == "follow" or (evntdata.Type == "subscription" and evntdata.For == "youtube_account"):
                if ScriptSettings.EnableFollow:
                    cooldownName = ScriptName + "-follow"
                    cooldownTime = ScriptSettings.FollowCooldown
                    eventPayload = CreateFollowPayload(message)
                    meetsMinimum = True
            elif (evntdata.Type == "subscription" or evntdata.Type == "resub") and evntdata.For == "twitch_account":
                if ScriptSettings.EnableSubscribe:
                    cooldownName = ScriptName + "-subscription"
                    cooldownTime = ScriptSettings.SubscriptionCooldown
                    eventPayload = CreateTwitchSubscribePayload(message)
                    meetsMinimum = eventPayload['Months'] >= ScriptSettings.SubscriptionMinimum
            elif evntdata.Type == "subMysteryGift":
                if ScriptSettings.EnableSubscribe:
                    cooldownName = ScriptName + "-subscription"
                    cooldownTime = ScriptSettings.SubscriptionCooldown
                    eventPayload = CreateTwitchMysterySubscriptionPayload(message)
                    meetsMinimum = eventPayload['Amount'] >= ScriptSettings.SubscriptionGiftedMinimum
            elif evntdata.Type == "bits":
                if message.Amount >= ScriptSettings.CheerMinimum and ScriptSettings.EnableCheer:
                    cooldownName = ScriptName + "-cheer"
                    cooldownTime = ScriptSettings.CheerCooldown
                    eventPayload = CreateTwitchCheerPayload(message)
                    meetsMinimum = eventPayload['Amount'] >= ScriptSettings.CheerMinimum
            elif evntdata.Type == "host" or evntdata.Type == "raid":
                if ScriptSettings.EnableHost:
                    cooldownName = ScriptName + "-host"
                    cooldownTime = ScriptSettings.HostCooldown
                    eventPayload = CreateHostPayload(message)
                    meetsMinimum = eventPayload['Viewers'] >= ScriptSettings.HostMinimum
            elif evntdata.Type == "donation":
                if ScriptSettings.EnableDonation:
                    cooldownName = ScriptName + "-donation"
                    cooldownTime = ScriptSettings.DonationCooldown
                    eventPayload = CreateDonationPayload(message)
                    meetsMinimum = eventPayload['Amount'] >= ScriptSettings.DonationMinimum


    isOnCooldown = Parent.IsOnCooldown(ScriptName, cooldownName)
    if eventPayload and not isOnCooldown and meetsMinimum:
        Parent.AddCooldown(ScriptName, cooldownName, cooldownTime)
        SendEventMessageEvent({
            "For": evntdata.For,
            "Message": eventPayload,
            "Type": evntdata.Type
        })
    return


def OpenOverlayInBrowser():
    os.startfile(os.path.realpath(os.path.join(os.path.dirname(__file__), "overlay.html")))
    return
def OpenScriptUpdater():
    currentDir = os.path.realpath(os.path.dirname(__file__))
    chatbotRoot = os.path.realpath(os.path.join(currentDir, "../../../"))
    libsDir = os.path.join(currentDir, "libs/updater")
    try:
        src_files = os.listdir(libsDir)
        tempdir = tempfile.mkdtemp()
        Parent.Log(ScriptName, tempdir)
        for file_name in src_files:
            full_file_name = os.path.join(libsDir, file_name)
            if os.path.isfile(full_file_name):
                Parent.Log(ScriptName, "Copy: " + full_file_name)
                shutil.copy(full_file_name, tempdir)
        updater = os.path.join(tempdir, "ChatbotScriptUpdater.exe")
        updaterConfigFile = os.path.join(tempdir, "update.manifest")
        repoVals = Repo.split('/')
        updaterConfig = {
            "path": os.path.realpath(os.path.join(currentDir,"../")),
            "version": Version,
            "name": ScriptName,
            "requiresRestart": True,
            "kill": [],
            "execute": {
                "before": [],
                "after": []
            },
            "chatbot": os.path.join(chatbotRoot, "Streamlabs Chatbot.exe"),
            "script": os.path.basename(os.path.dirname(os.path.realpath(__file__))),
            "website": Website,
            "repository": {
                "owner": repoVals[0],
                "name": repoVals[1]
            }
        }
        configJson = json.dumps(updaterConfig)
        with open(updaterConfigFile, "w+") as f:
            f.write(configJson)
        os.startfile(updater)
    except OSError as exc: # python >2.5
        raise

def OpenFollowOnTwitchLink():
    os.startfile("https://twitch.tv/DarthMinos")
    return

def OpenReadMeLink():
    os.startfile(ReadMeFile)
    return
def OpenDonateLink():
    os.startfile(DonateLink)
    return
def OpenTwitchClientIdLink():
    os.startfile("https://dev.twitch.tv/console/apps/create")
    return
def OpenSLAPISettingsLink():
    os.startfile("https://streamlabs.com/dashboard#/settings/api-settings")
    return

def ClearAllItems():
    SendClearItemsEvent()

### TEST EVENTS
def SendChatEvent():
    name = random.choice(TestAccounts)
    payload = {
        "Message": {
            "Name": name,
            "Emotes": [],
            "IsTest": True,
            "IsLive": False,
            "IsRepeat": False,
            "Message": "Woot!!!!",
            "Count": ScriptSettings.ChatMultiplier * 1,
            "TTL": ScriptSettings.GlobalObjectTTL if ScriptSettings.ChatObjectTTL == 0 else ScriptSettings.ChatObjectTTL
        },
        "Type": "chat",
        "For": "twitch_account"
    }
    SendEventMessageEvent(payload)

def SendTestEmoteEvent():
    name = random.choice(TestAccounts)
    payload = {
        "Message": {
            "Emotes": [
                "300094809",
                "300764452",
                "1782816",
                "1713819",
                "624501",
                "300116344",
                "300354391",
                "300710661",
                "1713813",
                "1148689",
                "2126505",
                "34",
                "1654895",
                "300029735",
                "1983580",
                "161209",
                "1713825",
                "379085",
                "1455566"
            ],
            "Name": name,
            "IsTest": True,
            "IsLive": False,
            "IsRepeat": False,
            "Message": "Fake Message That Should Have All The Emotes",
            "EmoteCount": ScriptSettings.ChatEmoteMultiplier * 1,
            "EmojiCount": ScriptSettings.ChatEmojiMultiplier * 1,
            "Count": ScriptSettings.ChatMultiplier * 1,
            "TTL": ScriptSettings.GlobalObjectTTL if ScriptSettings.ChatObjectTTL == 0 else ScriptSettings.ChatObjectTTL
        },
        "Type": "chat",
        "For": "twitch_account"
    }
    SendEventMessageEvent(payload)

def SendTestEmojiEvent():
    name = random.choice(TestAccounts)
    payload = {
        "Message": {
            "Emotes": [],
            "Name": name,
            "IsTest": True,
            "IsLive": False,
            "IsRepeat": False,
            "Message": "🚲🏴‍☠️✈🍪🌮🍑🍔😎🔔🖱😘✂👀🎍💩🏆🏷🎟🤔👅🍆🐠♻👍✅💤😴😉",
            "EmoteCount": ScriptSettings.ChatEmoteMultiplier * 1,
            "EmojiCount": ScriptSettings.ChatEmojiMultiplier * 1,
            "Count": ScriptSettings.ChatMultiplier * 1,
            "TTL": ScriptSettings.GlobalObjectTTL if ScriptSettings.ChatObjectTTL == 0 else ScriptSettings.ChatObjectTTL
        },
        "Type": "chat",
        "For": "twitch_account"
    }
    SendEventMessageEvent(payload)

def SendTwitchFollowEvent():
    name = random.choice(TestAccounts)
    payload = {
        "Message": {
            "Name": name,
            "IsTest": True,
            "IsLive": False,
            "IsRepeat": False,
            "CreatedAt": datetime.datetime.now(),
            "Id": name,
            "Count": ScriptSettings.FollowMultiplier * 1,
            "TTL": ScriptSettings.GlobalObjectTTL if ScriptSettings.FollowObjectTTL == 0 else ScriptSettings.FollowObjectTTL
        },
        "Type": "follow",
        "For": "twitch_account"
    }
    SendEventMessageEvent(payload)
def SendTwitchCheerEvent():
    amnt = random.randint(1, 25000)
    name = random.choice(TestAccounts)
    payload = {
        "Message": {
            "Name": name,
            "Amount": amnt,
            "Message": "cheer" + str(amnt) + " Test Cheer Kappa",
            "IsTest": True,
            "CreatedAt": datetime.datetime.now(),
            "Id": name,
            "Count": amnt * ScriptSettings.CheerMultiplier,
            "TTL": ScriptSettings.GlobalObjectTTL if ScriptSettings.CheerObjectTTL == 0 else ScriptSettings.CheerObjectTTL
        },
        "Type": "bits",
        "For": "twitch_account"
    }
    SendEventMessageEvent(payload)
def SendHostEvent():
    viewers = random.randint(1, 3000)
    name = random.choice(TestAccounts)
    SendEventMessageEvent({
        "For": "twitch_account",
        "Message": {
            "Name": name,
            "Viewers": viewers,
            "Count": viewers * ScriptSettings.HostMultiplier,
            "TTL": ScriptSettings.GlobalObjectTTL if ScriptSettings.HostObjectTTL == 0 else ScriptSettings.HostObjectTTL
        },
        "Type": "host"
    })
def SendDonationEvent():
    amnt = random.randint(1, 5000)
    name = random.choice(TestAccounts)
    SendEventMessageEvent({
        "For": "streamlabs",
        "Message": {
            "Name": name,
            "Amount": amnt,
            "Currency": "USD",
            "FromId": name,
            "FromName": name,
            "Message": "Don't spend it all in one place Kappa",
            "PaymentSource": "paypal",
            "Count": amnt * ScriptSettings.DonationMultiplier,
            "TTL": ScriptSettings.GlobalObjectTTL if ScriptSettings.DonationObjectTTL == 0 else ScriptSettings.DonationObjectTTL
        },
        "Type": "donation"
    })
def SendSubscriptionEvent():
    amnt = random.randint(1, 96)
    name = random.choice(TestAccounts)
    SendEventMessageEvent({
        "For": "streamlabs",
        "Message": {
            "Name": name,
            "IsTest": True,
            "IsLive": False,
            "IsRepeat": False,
            "DisplayName": name,
            "Gifter": "",
            "Message": "",
            "Months": amnt,
            "StreakMonths": 1,
            "Count":  amnt * ScriptSettings.SubscriptionMultiplier,
            "SubPlan": "",
            "SubPlanName": "",
            "SubType": "",
            "TTL": ScriptSettings.GlobalObjectTTL if ScriptSettings.SubscriptionObjectTTL == 0 else ScriptSettings.SubscriptionObjectTTL
        },
        "Type": "donation"
    })
def SendGiftSubscriptionEvent():
    amnt = random.randint(1, 96)
    name = random.choice(TestAccounts)
    name2 = random.choice(TestAccounts)
    SendEventMessageEvent({
        "For": "streamlabs",
        "Message": {
            "Name": name,
            "IsTest": True,
            "IsLive": False,
            "IsRepeat": False,
            "DisplayName": name,
            "Gifter": name2,
            "Message": "",
            "Months": amnt,
            "StreakMonths": 1,
            "Count":  amnt * ScriptSettings.SubscriptionMultiplier,
            "SubPlan": "",
            "SubPlanName": "",
            "SubType": "",
            "TTL": ScriptSettings.GlobalObjectTTL if ScriptSettings.SubscriptionObjectTTL == 0 else ScriptSettings.SubscriptionObjectTTL
        },
        "Type": "donation"
    })
def SendCommunityGiftSubscriptionEvent():
    amnt = random.randint(1, 100)
    name = random.choice(TestAccounts)
    SendEventMessageEvent({
        "For": "streamlabs",
        "Message": {
            "Name": name,
            "IsTest": True,
            "IsLive": False,
            "IsRepeat": False,
            "Amount": amnt,
            "Count": amnt * ScriptSettings.SubscriptionMultiplier,
            "Gifter": name,
            "GifterDisplayName": name,
            "SubPlan": "",
            "SubType": "",
            "TTL": ScriptSettings.GlobalObjectTTL if ScriptSettings.SubscriptionObjectTTL == 0 else ScriptSettings.SubscriptionObjectTTL
        },
        "Type": "donation"
    })
