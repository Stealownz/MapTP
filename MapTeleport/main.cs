using Microsoft.Xna.Framework;
using System;
using System.IO;
using System.Reflection;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using System.Timers;

namespace MapTeleport
{
    [ApiVersion(2, 1)]
    public class MapTeleport : TerrariaPlugin
    {
        public override Version Version
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version; }
        }
        public override string Author
        {
            get { return "Nova4334"; }
        }
        public override string Name
        {
            get { return "MapTeleport"; }
        }
        public override string Description
        {
            get { return "Allows players to teleport to a selected location on the map."; }
        }

        public override void Initialize()
        {
            GetDataHandlers.ReadNetModule.Register(teleport);
        }
        public MapTeleport(Main game) : base(game)
        {
            Order = 1;
        }

        public const string ALLOWED = "maptp";

        private void teleport(object unused, GetDataHandlers.ReadNetModuleEventArgs args)
        {
            if (args.Player.HasPermission(ALLOWED))
            {
                if (args.ModuleType == GetDataHandlers.NetModuleType.Ping)
                {
                    using (var reader = new BinaryReader(args.Data))
                    {
                        Vector2 pos = reader.ReadVector2();
                        args.Player.Teleport(pos.X * 16, pos.Y * 16);
                    }
                }
            }
        }
    }
}