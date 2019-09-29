using System;
using System.Collections.Generic;

using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MapMaker {
	public partial class EventPhysicsSettings {
		[JsonProperty ( "ImageScale" )]
		public long ImageScale { get; set; } = 10;

		[JsonProperty ( "MaxItems" )]
		public long MaxItems { get; set; } = 100;

		[JsonProperty ( "GlobalObjectTTL" )]
		public long GlobalObjectTtl { get; set; } = 30;

		[JsonProperty ( "ScreenHeight" )]
		public long ScreenHeight { get; set; } = 1080;

		[JsonProperty ( "ScreenWidth" )]
		public long ScreenWidth { get; set; } = 1920;

		[JsonProperty ( "HorizontalForce" )]
		public double HorizontalForce { get; set; } = 1.5;

		[JsonProperty ( "VerticalForce" )]
		public double VerticalForce { get; set; } = 1;

		[JsonProperty ( "ItemFriction" )]
		public double ItemFriction { get; set; } = 3;

		[JsonProperty ( "ItemAirFriction" )]
		public double ItemAirFriction { get; set; } = 5;

		[JsonProperty ( "ItemRestitution" )]
		public long ItemRestitution { get; set; } = 70;

		[JsonProperty ( "EnableChat" )]
		public bool EnableChat { get; set; } = true;

		[JsonProperty ( "ChatObjectTTL" )]
		public long ChatObjectTtl { get; set; } = 0;

		[JsonProperty ( "UseGlobalChatCooldown" )]
		public bool UseGlobalChatCooldown { get; set; } = false;

		[JsonProperty ( "ChatCooldown" )]
		public long ChatCooldown { get; set; } = 10;

		[JsonProperty ( "ChatMultiplier" )]
		public long ChatMultiplier { get; set; } = 1;

		[JsonProperty ( "EnableFollow" )]
		public bool EnableFollow { get; set; } = true;

		[JsonProperty ( "FollowObjectTTL" )]
		public long FollowObjectTtl { get; set; } = 0;

		[JsonProperty ( "FollowCooldown" )]
		public long FollowCooldown { get; set; } = 0;

		[JsonProperty ( "FollowMultiplier" )]
		public long FollowMultiplier { get; set; } = 1;

		[JsonProperty ( "EnableDonation" )]
		public bool EnableDonation { get; set; } = true;

		[JsonProperty ( "DonationObjectTTL" )]
		public long DonationObjectTtl { get; set; } = 0;

		[JsonProperty ( "DonationCooldown" )]
		public long DonationCooldown { get; set; } = 0;

		[JsonProperty ( "DonationMinimum" )]
		public long DonationMinimum { get; set; } = 1;

		[JsonProperty ( "DonationMultiplier" )]
		public long DonationMultiplier { get; set; } = 1;

		[JsonProperty ( "DonationPercent" )]
		public long DonationPercent { get; set; } = 100;

		[JsonProperty ( "EnableSubscribe" )]
		public bool EnableSubscribe { get; set; } = true;

		[JsonProperty ( "SubscriptionObjectTTL" )]
		public long SubscriptionObjectTtl { get; set; } = 0;

		[JsonProperty ( "SubscriptionCooldown" )]
		public long SubscriptionCooldown { get; set; } = 0;

		[JsonProperty ( "SubscriptionMinimum" )]
		public long SubscriptionMinimum { get; set; } = 1;

		[JsonProperty ( "SubscriptionGiftedMinimum" )]
		public long SubscriptionGiftedMinimum { get; set; } = 1;

		[JsonProperty ( "SubscriptionMultiplier" )]
		public long SubscriptionMultiplier { get; set; } = 1;

		[JsonProperty ( "SubscriptionPercent" )]
		public long SubscriptionPercent { get; set; } = 100;

		[JsonProperty ( "EnableHost" )]
		public bool EnableHost { get; set; } = true;

		[JsonProperty ( "HostObjectTTL" )]
		public long HostObjectTtl { get; set; } = 0;

		[JsonProperty ( "HostMinimum" )]
		public long HostMinimum { get; set; } = 1;

		[JsonProperty ( "HostCooldown" )]
		public long HostCooldown { get; set; } = 0;

		[JsonProperty ( "HostMultiplier" )]
		public long HostMultiplier { get; set; } = 1;

		[JsonProperty ( "HostPercent" )]
		public long HostPercent { get; set; } = 100;

		[JsonProperty ( "EnableCheer" )]
		public bool EnableCheer { get; set; } = true;

		[JsonProperty ( "CheerObjectTTL" )]
		public long CheerObjectTtl { get; set; } = 0;

		[JsonProperty ( "CheerCooldown" )]
		public long CheerCooldown { get; set; } = 0;

		[JsonProperty ( "CheerMinimum" )]
		public long CheerMinimum { get; set; } = 100;

		[JsonProperty ( "CheerMultiplier" )]
		public long CheerMultiplier { get; set; } = 1;

		[JsonProperty ( "CheerPercent" )]
		public long CheerPercent { get; set; } = 100;

		[JsonProperty ( "StreamlabsToken" )]
		public string StreamlabsToken { get; set; } = "";

		[JsonProperty ( "TwitchClientId" )]
		public string TwitchClientId { get; set; } = "";

		[JsonProperty ( "ItemModel" )]
		public string ItemModel { get; set; } = "circle";

		[JsonProperty ( "ItemDensity" )]
		public long ItemDensity { get; set; } = 1;

		[JsonProperty ( "ScreenMap" )]
		public string ScreenMap { get; set; } = "none";

		[JsonProperty ( "DebugMap" )]
		public bool DebugMap { get; set; } = false;

		[JsonProperty ( "CustomMapName" )]
		public string CustomMapName { get; set; } = "";
	}

	public partial class EventPhysicsSettings {
		public static EventPhysicsSettings FromJson ( string json ) => JsonConvert.DeserializeObject<EventPhysicsSettings> ( json, JsonConverter.Settings );
		public static EventPhysicsSettings FromFile ( string file ) {
			using ( var f = new StreamReader ( file ) ) {
				using ( var jtr = new JsonTextReader ( f ) ) {
					var ser = JsonSerializer.Create ( JsonConverter.Settings );
					return ser.Deserialize<EventPhysicsSettings> ( jtr );
				}
			}
		}
	}


	public static class EventPhysicsSettingsSerializer {
		public static string ToJson ( this EventPhysicsSettings self ) => JsonConvert.SerializeObject ( self, JsonConverter.Settings );
		public static void ToFile ( this EventPhysicsSettings self, string file ) {
			using ( var f = File.CreateText ( file ) ) {
				var ser = JsonSerializer.Create ( JsonConverter.Settings );
				ser.Serialize ( f, self );
			}
		}

	}

}
