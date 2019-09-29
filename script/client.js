"use strict";
var TWITCH_EMOTE_URL = "http://static-cdn.jtvnw.net/emoticons/v1/{EMOTE_ID}/3.0";

function initializeUI() {
	// $(":root")
	// 	.css("--link-color", `${settings.LinkColor || "rgba(230,126,34,1)"}`)
	// 	.css("--name-color", `${settings.UserColor || "rgba(255, 0, 0, 1)"}`);
}


function connectWebsocket() {
	//-------------------------------------------
	//  Create WebSocket
	//-------------------------------------------
	let socket = new WebSocket("ws://127.0.0.1:3337/streamlabs");

	//-------------------------------------------
	//  Websocket Event: OnOpen
	//-------------------------------------------
	socket.onopen = function () {
		// AnkhBot Authentication Information
		let auth = {
			author: "DarthMinos",
			website: "darthminos.tv",
			api_key: API_Key,
			events: [
				"EVENT_EP_SETTINGS",
				"EVENT_EP_CLEARALL",
				"EVENT_EP_EVENTMESSAGE"
			]
		};

		// Send authentication data to ChatBot ws server

		socket.send(JSON.stringify(auth));
	};

	//-------------------------------------------
	//  Websocket Event: OnMessage
	//-------------------------------------------
	socket.onmessage = function (message) {
		console.log(message);
		// Parse message
		let socketMessage = JSON.parse(message.data);
		let eventName = socketMessage.event;
		console.log(socketMessage);
		// if data is empty, just leave
		if (!socketMessage.data) {
			console.log("EXIT BECAUSE NO DATA");
			return;
		}

		let eventData = typeof socketMessage.data === "string" ? JSON.parse(socketMessage.data || "{}") : socketMessage.data;

		// if Message is null, leave;
		if (!eventData) {
			console.log("EXIT BECAUSE NULL");
			return;
		}

		switch (eventName) {
			case "EVENT_EP_EVENTMESSAGE":
				if (!eventData.Message) {
					return;
				}


				switch (eventData.Type) {
					case "chat":
						if (settings.ChatEnableEmotes) {
							for (var eid = 0; eid <= eventData.Message.Emotes.length; ++eid) {
								var id = eventData.Message.Emotes[eid];
								if (id) {
									var url = TWITCH_EMOTE_URL.replace(/\{EMOTE_ID\}/gi, id + "");
									var options = {
										image: url,
										count: (eventData.Message.EmoteCount || 1),
										size: 112,
										scale: (settings.ImageScale / 100) * 3.33,
										max: settings.MaxItems,
										ttl: eventData.Message.TTL || settings.GlobalObjectTTL || 0
									};
									WorldCanvas.addObject(options);
								}
							}
						}
						if (settings.ChatEnableEmoji) {
							if (eventData.Message.Message) {
								var n = $(`<div>${eventData.Message.Message}</div>`);
								twemoji.parse(n.get(0), { size: 72, ext: ".png" });
								var imgs = $("img.emoji", n);
								var urls = [];
								$.each(imgs, function (idx, item) {
									var lu = $(item).attr("src");
									console.log(lu);
									var options = {
										image: lu,
										count: (eventData.Message.EmojiCount || 1),
										size: 72,
										scale: (settings.ImageScale / 100) * 4.5,
										max: settings.MaxItems,
										ttl: eventData.Message.TTL || settings.GlobalObjectTTL || 0
									};
									WorldCanvas.addObject(options);
									urls.push(lu);
								});
							}
						}
						break;
				}

				var name = eventData.Message.FromId || eventData.Message.FromName || eventData.Message.Gifter || eventData.Message.Name
				$.ajax({
					type: 'GET',
					url: 'https://decapi.me/twitch/avatar/' + name,
					success: function (data) {
						var percent = 100;
						var scale = 10;
						switch (eventData.Type) {
							case "raid":
							case "host":
								scale = settings.ImageScale;
								percent = settings.HostPercent || 10;
								break;
							case "bits":
								scale = settings.ImageScale;
								percent = settings.CheerPercent || 10;
								break;
							case "donation":
								scale = settings.ImageScale;
								percent = settings.DonationPercent || 10;
								break;
							case "follow":
								scale = settings.ImageScale;
								percent = 100;
								break;
							case "resub":
							case "subMysteryGift":
							case "subscription":
								if (eventData.For === "youtube_account") {
									scale = settings.ImageScale;
									percent = 100;
									break;
								}
								percent = settings.SubscriptionPercent || 100;
								break;
							case "chat":
								if(!settings.EnableChat) {
									return;
								}
								break;
							default:
								scale = settings.ImageScale;
								percent = 100;
								break;
						}
						percent = percent / 100;
						if (data) {
							var val = (eventData.Message.Count || 1);
							WorldCanvas.addObject({
								image: data,
								count: Math.ceil(val * percent),
								scale: scale / 100,
								max: settings.MaxItems,
								ttl: eventData.Message.TTL || settings.GlobalObjectTTL || 0
							});
						}
					},
					error: function (err) {
						console.error(err);
					}
				});
				break;
			case "EVENT_EP_CLEARALL":
				console.log("clear all things");
				WorldCanvas.clear();
				break;
			case "EVENT_EP_SETTINGS":
				window.settings = eventData;
				if (validateInit()) {
					initializeUI();
					// if (settings.ScreenMap === "custom") {
					// 	WorldCanvas.loadCustomMap(settings.CustomMapName);
					// } else {
					// 	WorldCanvas.loadMap(settings.ScreenMap);
					// }
				}
				break;
			default:
				console.log(eventName);
				break;
		}
	};

	//-------------------------------------------
	//  Websocket Event: OnError
	//-------------------------------------------
	socket.onerror = function (error) {
		console.error(`Error: ${error}`);
	};

	//-------------------------------------------
	//  Websocket Event: OnClose
	//-------------------------------------------
	socket.onclose = function () {
		console.log("close");
		// Clear socket to avoid multiple ws objects and EventHandlings
		socket = null;
		// Try to reconnect every 5s
		setTimeout(function () { connectWebsocket(); }, 5000);
	};

}


function validateSettings() {
	let hasApiKey = typeof API_Key !== "undefined";
	let hasSettings = typeof settings !== "undefined";

	return {
		isValid: hasApiKey && hasSettings,
		hasSettings: hasSettings,
		hasApiKey: hasApiKey
	};
}

function validateInit() {
	// verify settings...
	let validatedSettings = validateSettings();

	// Connect if API_Key is inserted
	// Else show an error on the overlay
	if (!validatedSettings.isValid) {
		$("#config-messages").removeClass("hidden");
		$("#config-messages .settings").removeClass(validatedSettings.hasSettings ? "valid" : "hidden");
		$("#config-messages .api-key").removeClass(validatedSettings.hasApiKey ? "valid" : "hidden");
		return false;
	}
	return true;
}


jQuery(document).ready(function () {
	if (validateInit()) {
		initializeUI();
		connectWebsocket();
	} else {
		console.log("Invalid");
	}
});
