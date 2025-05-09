using XRL.World.Parts;

namespace XRL.World.ZoneFactories {
    public class MyWorldFactory : IZoneFactory {
        public override bool CanBuildZone(ZoneRequest Request) {
            // Use BuildZone for the world map and for zones outside of the map's
            // boundaries.
            return Request.IsWorldZone
                || Request.WorldX < 37
                || Request.WorldX > 43
                || Request.WorldY < 10
                || Request.WorldY > 14;
        }   

        public override Zone BuildZone(ZoneRequest Request) {
            var zone = new Zone(80, 25);
            zone.ZoneID = Request.ZoneID;
            zone.DisplayName = "your personal world";

            if (Request.IsWorldZone) {
                zone.ForeachCell(delegate(Cell c) {
                    c.AddObject("InteriorVoid");
                    Rocky.Paint(c);
                });

                for (int i = 37; i <= 43 ; i++) {
                    for (int j = 10; j <= 14; j++) {
                        var cell = zone.GetCell(i, j);
                        cell.Clear();
                        cell.AddObject("TerrainJungle");
                    }
                }

                return zone;
            }


            // For zones outside of the world's boundaries, we generate a zero-width,
            // zero-height zone to prevent the player from entering.
            return new Zone(0, 0);
        }

        // ...
    }
}