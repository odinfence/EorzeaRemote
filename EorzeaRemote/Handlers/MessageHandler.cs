using EorzeaRemote.Data;
using EorzeaRemote.Exceptions;
using Riptide;

namespace EorzeaRemote.Handlers
{
    internal class MessageHandler
    {
        private readonly Server Server;
        private readonly Dictionary<ushort, Player> Players;
        
        public MessageHandler(Server Server, Dictionary<ushort, Player> Players)
        {
            this.Server = Server;
            this.Players = Players;
        }

        public void Handle(MessageReceivedEventArgs e)
        {
            var PlayerId = e.FromConnection.Id;
            var Message = e.Message;
            var Type = e.MessageId;

            switch (Type)
            {
                case (ushort)MessageSendType.IssueSpeakOrder: HandleSpeakOrder(PlayerId, Message); break;
                case (ushort)MessageSendType.IssueEmoteOrder: HandleEmoteOrder(PlayerId, Message); break;
                case (ushort)MessageSendType.IssueEmoteAndSpeakOrder: HandleEmoteAndSpeakOrder(PlayerId, Message); break;
                case (ushort)MessageSendType.RegisterNameRequest: HandleRegisterName(PlayerId, Message); break;
                case (ushort)MessageSendType.AuthorizationRequest: HandleSeekAuthorization(PlayerId, Message); break;
            }
        }

        private void HandleRegisterName(ushort SenderId, Message Message)
        {
            try
            {
                var Player = GetPlayer(SenderId);

                Player.Name = IsValidName(Message.GetString());
            }
            catch (Exception)
            {
                // TODO: Handle
            }
        }

        private void HandleEmoteOrder(ushort SenderId, Message Message)
        {
            try
            {
                var Player = GetPlayer(SenderId);

                if (Player.IsAuthenticated)
                {
                    var Emote = IsValidEmote(Message.GetString());
                    var Order = Message.Create(MessageSendMode.Reliable, MessageSendType.IssueEmoteOrder);
                    Order.AddString(Emote);

                    Server.SendToAll(Order, SenderId);
                }
            }
            catch(Exception)
            {
                // TODO: Handle
            }
        }

        private void HandleSpeakOrder(ushort SenderId, Message Message)
        {
            try
            {
                var Player = GetPlayer(SenderId);

                if (Player.IsAuthenticated)
                {
                    var Channel = IsValidChannel(Message.GetString());
                    var Text = IsValidEmote(Message.GetString());

                    var Order = Message.Create(MessageSendMode.Reliable, MessageSendType.IssueEmoteOrder);
                    Order.AddString(Channel);
                    Order.AddString(Text);

                    Server.SendToAll(Order, SenderId);
                }
            }
            catch(Exception)
            {
                // TODO: Handle
            }
        }

        private void HandleEmoteAndSpeakOrder(ushort SenderId, Message Message)
        {
            try
            {
                var Player = GetPlayer(SenderId);

                if (Player.IsAuthenticated)
                {
                    var Emote = IsValidEmote(Message.GetString());
                    var Channel = IsValidChannel(Message.GetString());
                    var Text = IsValidEmote(Message.GetString());

                    var Order = Message.Create(MessageSendMode.Reliable, MessageSendType.IssueEmoteOrder);
                    Order.AddString(Emote);
                    Order.AddString(Channel);
                    Order.AddString(Text);

                    Server.SendToAll(Order, SenderId);
                }
            }
            catch (Exception)
            {
                // TODO: Handle
            }
        }

        private void HandleSeekAuthorization(ushort SenderId, Message Message)
        {
            try
            {
                var Player = GetPlayer(SenderId);
                var Secret = Message.GetString(); // TODO: Validation

                if (Secret.Equals("MySecretKey"))
                {
                    Player.IsAuthenticated = true;
                }

                var Response = Message.Create(MessageSendMode.Reliable, MessageSendType.AuthorizationResponse);
                Response.AddBool(Player.IsAuthenticated);

                Server.Send(Response, SenderId);
            }
            catch (Exception)
            {
                // TODO: Handle
            }
        }

        public Player GetPlayer(ushort SenderId) => Players.TryGetValue(SenderId, out Player Player) ? Player : throw new PlayerNotFoundException();

        public string IsValidName(string input) => input.Length < 21 ? input : throw new InvalidInputException();
        public string IsValidEmote(string input) => Emotes.Contains(input) ? input : throw new InvalidInputException();
        public string IsValidChannel(string input) => Channels.Contains(input) ? input : throw new InvalidInputException();

        private readonly HashSet<string> Emotes = new()
        {
            "aback",
            "advent",
            "adventoflight",
            "airquotes",
            "alert",
            "allsaintscharm",
            "amazed",
            "angry",
            "annoy",
            "annoyed",
            "apple",
            "atease",
            "attention",
            "awe",
            "backflip",
            "balldance",
            "battlestance",
            "bdance",
            "beam",
            "beckon",
            "beesknees",
            "bflip",
            "biggrin",
            "blowkiss",
            "blush",
            "bombdance",
            "bow",
            "box",
            "boxstep",
            "bread",
            "breakfast",
            "breathcontrol",
            "broom",
            "bstance",
            "changepose",
            "charmed",
            "cheer",
            "cheerbright",
            "cheergreen",
            "cheerjump",
            "cheerjumpgreen",
            "cheeron",
            "cheeronbright",
            "cheerviolet",
            "cheerwave",
            "cheerwaveviolet",
            "choco",
            "chuckle",
            "clap",
            "clutchhead",
            "comfort",
            "concentrate",
            "confirm",
            "congratulate",
            "consider",
            "content",
            "converse",
            "cookie",
            "cpose",
            "crimsonlotus",
            "cry",
            "dance",
            "deny",
            "deride",
            "determined",
            "disappointed",
            "disturbed",
            "dote",
            "doubt",
            "doze",
            "draw",
            "earwiggle",
            "easternbow",
            "easterndance",
            "easterngreeting",
            "easternstretch",
            "eatapple",
            "eatchocolate",
            "eategg",
            "eatpizza",
            "eatpumpkincookie",
            "eatriceball",
            "ebow",
            "edance",
            "egg",
            "egreeting",
            "elucidate",
            "embrace",
            "endure",
            "estretch",
            "eureka",
            "examineself",
            "facepalm",
            "fakesmile",
            "fear",
            "fist",
            "fistbump",
            "fistpump",
            "flamedance",
            "flex",
            "flowershower",
            "frighten",
            "fume",
            "furious",
            "furrow",
            "gcsalute",
            "gcsalute",
            "gcsalute",
            "gdance",
            "getfantasy",
            "golddance",
            "goobbuedo",
            "goodbye",
            "gratuity",
            "greet",
            "grin",
            "groundsit",
            "grovel",
            "guard",
            "handover",
            "handtoheart",
            "happy",
            "harvestdance",
            "haurchefant",
            "hdance",
            "headache",
            "heeltoe",
            "hifive",
            "highfive",
            "hildibrand",
            "hildy",
            "hknight",
            "hmm",
            "hug",
            "huh",
            "hum",
            "hurray",
            "iceheart",
            "imperialsalute",
            "insist",
            "joy",
            "kneel",
            "ladance",
            "laliho",
            "lalihop",
            "laugh",
            "lean",
            "leftwink",
            "linkpearl",
            "littleladiesdance",
            "lookout",
            "lounge",
            "magictrick",
            "malevolence",
            "mandervilledance",
            "mandervillemambo",
            "mdance",
            "me",
            "megaflare",
            "mime",
            "mmambo",
            "mogdance",
            "moonlift",
            "no",
            "ouch",
            "ow",
            "pagaga",
            "paintblack",
            "paintblue",
            "paintred",
            "paintyellow",
            "panic",
            "pantomime",
            "pdead",
            "petals",
            "pizza",
            "playdead",
            "point",
            "poke",
            "ponder",
            "popotostep",
            "pose",
            "powerup",
            "pplease",
            "pray",
            "prettyplease",
            "psych",
            "puckerup",
            "pushups",
            "rally",
            "rangerpose1l",
            "rangerpose1r",
            "rangerpose2l",
            "rangerpose2r",
            "rangerpose3l",
            "rangerpose3r",
            "read",
            "reflect",
            "reprimand",
            "respect",
            "riceball",
            "rightwink",
            "ritualprayer",
            "rpose1l",
            "rpose1r",
            "rpose2l",
            "rpose2r",
            "rpose3l",
            "rpose3r",
            "sabotender",
            "sad",
            "salute",
            "scared",
            "scheme",
            "scoff",
            "sdance",
            "sheathe",
            "shh",
            "shiver",
            "shocked",
            "showleft",
            "showright",
            "shrug",
            "shush",
            "shut",
            "shuteyes",
            "sidestep",
            "simper",
            "simulationf",
            "simulationm",
            "sit",
            "situps",
            "slap",
            "smile",
            "smirk",
            "snap",
            "sneer",
            "songbird",
            "soothe",
            "spectacles",
            "spirit",
            "splash",
            "squats",
            "stagger",
            "stepdance",
            "straight",
            "straightface",
            "stretch",
            "stroke",
            "sulk",
            "sundance",
            "surprised",
            "sweat",
            "sweep",
            "taunt",
            "tdance",
            "tea",
            "thavdance",
            "think",
            "throw",
            "thumbsup",
            "toast",
            "tomestone",
            "tremble",
            "ultima",
            "upset",
            "vexed",
            "victorypose",
            "visor",
            "vpose",
            "wasshoi",
            "waterflip",
            "waterfloat",
            "wave",
            "welcome",
            "winded",
            "wink",
            "worried",
            "worry",
            "wow",
            "wringhands",
            "yes",
            "yoldance",
            "zantetsuken",
            "ztk",
        };
        private readonly HashSet<string> Channels = new()
        {
            "s",
            "y",
            "sh",
            "t",
            "p",
            "a",
            "fc",
            "ls1",
            "ls2",
            "ls3",
            "ls4",
            "ls5",
            "ls6",
            "ls7",
            "ls8",
            "cwl1",
            "cwl2",
            "cwl3",
            "cwl4",
            "cwl5",
            "cwl6",
            "cwl7",
            "cwl8",
        };
    }
}
