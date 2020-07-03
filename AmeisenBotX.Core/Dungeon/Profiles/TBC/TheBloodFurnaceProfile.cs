﻿using AmeisenBotX.Core.Data.Enums;
using AmeisenBotX.Core.Dungeon.Enums;
using AmeisenBotX.Core.Dungeon.Objects;
using AmeisenBotX.Core.Jobs.Profiles;
using AmeisenBotX.Core.Movement.Pathfinding.Objects;
using System.Collections.Generic;

namespace AmeisenBotX.Core.Dungeon.Profiles.TBC
{
    public class TheBloodFurnaceProfile : IDungeonProfile
    {
        public string Author { get; } = "Jannis";

        public string Description { get; } = "Profile for the Dungeon in Outland, made for Level 59 to 63.";

        public DungeonFactionType FactionType { get; } = DungeonFactionType.Neutral;

        public int GroupSize { get; } = 5;

        public MapId MapId { get; } = MapId.TheBloodFurnace;

        public int MaxLevel { get; } = 63;

        public string Name { get; } = "[59-63] The Blood Furnace";

        public List<DungeonNode> Nodes { get; private set; } = new List<DungeonNode>()
        {
            new DungeonNode(new Vector3(-4, 15, -45)),
            new DungeonNode(new Vector3(-2, 7, -45)),
            new DungeonNode(new Vector3(-1, -1, -44)),
            new DungeonNode(new Vector3(1, -9, -43)),
            new DungeonNode(new Vector3(3, -17, -43)),
            new DungeonNode(new Vector3(4, -25, -42)),
            new DungeonNode(new Vector3(6, -33, -42)),
            new DungeonNode(new Vector3(7, -41, -41)),
            new DungeonNode(new Vector3(8, -49, -41)),
            new DungeonNode(new Vector3(3, -56, -41)),
            new DungeonNode(new Vector3(-4, -60, -41)),
            new DungeonNode(new Vector3(-11, -64, -41)),
            new DungeonNode(new Vector3(-17, -69, -41)),
            new DungeonNode(new Vector3(-18, -77, -41)),
            new DungeonNode(new Vector3(-14, -84, -41)),
            new DungeonNode(new Vector3(-7, -87, -41)),
            new DungeonNode(new Vector3(1, -86, -41)),
            new DungeonNode(new Vector3(9, -85, -41)),
            new DungeonNode(new Vector3(17, -85, -41)),
            new DungeonNode(new Vector3(25, -85, -41)),
            new DungeonNode(new Vector3(33, -86, -41)),
            new DungeonNode(new Vector3(40, -89, -40)),
            new DungeonNode(new Vector3(48, -91, -40)),
            new DungeonNode(new Vector3(56, -91, -37)),
            new DungeonNode(new Vector3(64, -91, -34)),
            new DungeonNode(new Vector3(71, -90, -31)),
            new DungeonNode(new Vector3(78, -90, -28)),
            new DungeonNode(new Vector3(85, -89, -24)),
            new DungeonNode(new Vector3(92, -89, -21)),
            new DungeonNode(new Vector3(99, -89, -18)),
            new DungeonNode(new Vector3(106, -88, -15)),
            new DungeonNode(new Vector3(113, -88, -12)),
            new DungeonNode(new Vector3(120, -88, -8)),
            new DungeonNode(new Vector3(127, -88, -5)),
            new DungeonNode(new Vector3(134, -88, -2)),
            new DungeonNode(new Vector3(141, -88, 1)),
            new DungeonNode(new Vector3(148, -88, 4)),
            new DungeonNode(new Vector3(155, -88, 7)),
            new DungeonNode(new Vector3(162, -86, 10)),
            new DungeonNode(new Vector3(170, -84, 10)),
            new DungeonNode(new Vector3(178, -84, 10)),
            new DungeonNode(new Vector3(186, -84, 10)),
            new DungeonNode(new Vector3(194, -84, 10)),
            new DungeonNode(new Vector3(202, -85, 10)),
            new DungeonNode(new Vector3(210, -87, 10)),
            new DungeonNode(new Vector3(216, -92, 10)),
            new DungeonNode(new Vector3(223, -96, 10)),
            new DungeonNode(new Vector3(230, -93, 10)),
            new DungeonNode(new Vector3(234, -86, 10)),
            new DungeonNode(new Vector3(231, -78, 10)),
            new DungeonNode(new Vector3(228, -71, 10)),
            new DungeonNode(new Vector3(234, -66, 10)),
            new DungeonNode(new Vector3(241, -63, 10)),
            new DungeonNode(new Vector3(244, -56, 10)),
            new DungeonNode(new Vector3(246, -48, 10)),
            new DungeonNode(new Vector3(249, -40, 9)),
            new DungeonNode(new Vector3(252, -33, 7)),
            new DungeonNode(new Vector3(255, -26, 7)),
            new DungeonNode(new Vector3(261, -20, 7)),
            new DungeonNode(new Vector3(266, -14, 7)),
            new DungeonNode(new Vector3(272, -8, 7)),
            new DungeonNode(new Vector3(277, -2, 8)),
            new DungeonNode(new Vector3(285, -3, 9)),
            new DungeonNode(new Vector3(293, -3, 10)),
            new DungeonNode(new Vector3(300, 1, 10)),
            new DungeonNode(new Vector3(307, 5, 10)),
            new DungeonNode(new Vector3(313, 0, 10)),
            new DungeonNode(new Vector3(318, -6, 10)),
            new DungeonNode(new Vector3(326, -4, 10)),
            new DungeonNode(new Vector3(333, -1, 10)),
            new DungeonNode(new Vector3(339, 4, 10)),
            new DungeonNode(new Vector3(337, 12, 10)),
            new DungeonNode(new Vector3(333, 19, 10)),
            new DungeonNode(new Vector3(330, 26, 10)),
            new DungeonNode(new Vector3(327, 33, 10)),
            new DungeonNode(new Vector3(327, 41, 10)),
            new DungeonNode(new Vector3(327, 49, 10)),
            new DungeonNode(new Vector3(328, 57, 10)),
            new DungeonNode(new Vector3(330, 65, 10)),
            new DungeonNode(new Vector3(323, 69, 10)),
            new DungeonNode(new Vector3(316, 66, 10)),
            new DungeonNode(new Vector3(308, 64, 10)),
            new DungeonNode(new Vector3(305, 71, 10)),
            new DungeonNode(new Vector3(308, 78, 10)),
            new DungeonNode(new Vector3(310, 86, 10)),
            new DungeonNode(new Vector3(309, 94, 10)),
            new DungeonNode(new Vector3(312, 101, 10)),
            new DungeonNode(new Vector3(320, 99, 10)),
            new DungeonNode(new Vector3(328, 99, 10)),
            new DungeonNode(new Vector3(336, 100, 10)),
            new DungeonNode(new Vector3(344, 100, 10)),
            new DungeonNode(new Vector3(348, 93, 10)),
            new DungeonNode(new Vector3(355, 96, 10)),
            new DungeonNode(new Vector3(355, 104, 10)),
            new DungeonNode(new Vector3(347, 106, 10)),
            new DungeonNode(new Vector3(339, 105, 10)),
            new DungeonNode(new Vector3(332, 108, 10)),
            new DungeonNode(new Vector3(329, 115, 10)),
            new DungeonNode(new Vector3(329, 123, 10)),
            new DungeonNode(new Vector3(328, 131, 10)),
            new DungeonNode(new Vector3(328, 139, 10)),
            new DungeonNode(new Vector3(328, 147, 10)),
            new DungeonNode(new Vector3(327, 155, 10)),
            new DungeonNode(new Vector3(325, 163, 10)),
            new DungeonNode(new Vector3(322, 170, 10)),
            new DungeonNode(new Vector3(319, 178, 10)),
            new DungeonNode(new Vector3(317, 186, 10)),
            new DungeonNode(new Vector3(325, 184, 10)),
            new DungeonNode(new Vector3(333, 186, 10)),
            new DungeonNode(new Vector3(340, 189, 10)),
            new DungeonNode(new Vector3(348, 191, 10)),
            new DungeonNode(new Vector3(356, 192, 10)),
            new DungeonNode(new Vector3(364, 192, 10)),
            new DungeonNode(new Vector3(372, 192, 10)),
            new DungeonNode(new Vector3(380, 192, 10)),
            new DungeonNode(new Vector3(388, 191, 10)),
            new DungeonNode(new Vector3(396, 191, 10)),
            new DungeonNode(new Vector3(404, 191, 10)),
            new DungeonNode(new Vector3(412, 190, 10)),
            new DungeonNode(new Vector3(420, 190, 10)),
            new DungeonNode(new Vector3(428, 191, 10)),
            new DungeonNode(new Vector3(436, 191, 10)),
            new DungeonNode(new Vector3(444, 190, 10)),
            new DungeonNode(new Vector3(452, 188, 10)),
            new DungeonNode(new Vector3(459, 184, 10)),
            new DungeonNode(new Vector3(460, 176, 10)),
            new DungeonNode(new Vector3(459, 168, 10)),
            new DungeonNode(new Vector3(457, 160, 10)),
            new DungeonNode(new Vector3(456, 152, 10)),
            new DungeonNode(new Vector3(455, 144, 10)),
            new DungeonNode(new Vector3(456, 136, 10)),
            new DungeonNode(new Vector3(462, 131, 10)),
            new DungeonNode(new Vector3(463, 123, 10)),
            new DungeonNode(new Vector3(456, 120, 10)),
            new DungeonNode(new Vector3(463, 116, 10)),
            new DungeonNode(new Vector3(471, 114, 10)),
            new DungeonNode(new Vector3(477, 109, 10)),
            new DungeonNode(new Vector3(480, 102, 10)),
            new DungeonNode(new Vector3(478, 94, 10)),
            new DungeonNode(new Vector3(470, 94, 10)),
            new DungeonNode(new Vector3(462, 94, 10)),
            new DungeonNode(new Vector3(454, 94, 10)),
            new DungeonNode(new Vector3(448, 88, 10)),
            new DungeonNode(new Vector3(441, 85, 10)),
            new DungeonNode(new Vector3(438, 78, 10)),
            new DungeonNode(new Vector3(438, 70, 10)),
            new DungeonNode(new Vector3(441, 63, 10)),
            new DungeonNode(new Vector3(449, 63, 10)),
            new DungeonNode(new Vector3(456, 66, 10)),
            new DungeonNode(new Vector3(464, 66, 10)),
            new DungeonNode(new Vector3(472, 65, 10)),
            new DungeonNode(new Vector3(469, 58, 10)),
            new DungeonNode(new Vector3(457, 54, 10), DungeonNodeType.Use, "Cell Door Lever"),
            new DungeonNode(new Vector3(457, 51, 10), DungeonNodeType.Use, "Cell Door Lever"),
            new DungeonNode(new Vector3(456, 43, 10)),
            new DungeonNode(new Vector3(456, 35, 10)),
            new DungeonNode(new Vector3(456, 27, 10)),
            new DungeonNode(new Vector3(456, 19, 10)),
            new DungeonNode(new Vector3(456, 11, 10)),
            new DungeonNode(new Vector3(456, 3, 10)),
            new DungeonNode(new Vector3(464, 3, 10)),
            new DungeonNode(new Vector3(471, 7, 10)),
            new DungeonNode(new Vector3(478, 10, 10)),
            new DungeonNode(new Vector3(486, 12, 10)),
            new DungeonNode(new Vector3(491, 6, 10)),
            new DungeonNode(new Vector3(492, -2, 10)),
            new DungeonNode(new Vector3(488, -9, 10)),
            new DungeonNode(new Vector3(483, -15, 10)),
            new DungeonNode(new Vector3(478, -22, 10)),
            new DungeonNode(new Vector3(474, -29, 10)),
            new DungeonNode(new Vector3(471, -36, 10)),
            new DungeonNode(new Vector3(468, -43, 10)),
            new DungeonNode(new Vector3(466, -51, 10)),
            new DungeonNode(new Vector3(466, -59, 10)),
            new DungeonNode(new Vector3(466, -67, 10)),
            new DungeonNode(new Vector3(464, -75, 10)),
            new DungeonNode(new Vector3(461, -82, 10)),
            new DungeonNode(new Vector3(453, -84, 10)),
            new DungeonNode(new Vector3(445, -84, 10)),
            new DungeonNode(new Vector3(437, -84, 10)),
            new DungeonNode(new Vector3(429, -84, 10)),
            new DungeonNode(new Vector3(421, -85, 10)),
            new DungeonNode(new Vector3(417, -92, 10)),
            new DungeonNode(new Vector3(415, -100, 10)),
            new DungeonNode(new Vector3(413, -108, 10)),
            new DungeonNode(new Vector3(411, -116, 9)),
            new DungeonNode(new Vector3(409, -123, 7)),
            new DungeonNode(new Vector3(407, -130, 4)),
            new DungeonNode(new Vector3(404, -136, 0)),
            new DungeonNode(new Vector3(401, -142, -4)),
            new DungeonNode(new Vector3(396, -147, -8)),
            new DungeonNode(new Vector3(390, -152, -10)),
            new DungeonNode(new Vector3(385, -157, -13)),
            new DungeonNode(new Vector3(378, -162, -16)),
            new DungeonNode(new Vector3(371, -165, -19)),
            new DungeonNode(new Vector3(364, -166, -22)),
            new DungeonNode(new Vector3(357, -165, -26)),
            new DungeonNode(new Vector3(354, -172, -26)),
            new DungeonNode(new Vector3(349, -178, -26)),
            new DungeonNode(new Vector3(344, -185, -26)),
            new DungeonNode(new Vector3(339, -191, -26)),
            new DungeonNode(new Vector3(331, -193, -26)),
            new DungeonNode(new Vector3(323, -190, -26)),
            new DungeonNode(new Vector3(317, -185, -26)),
            new DungeonNode(new Vector3(313, -178, -26)),
            new DungeonNode(new Vector3(319, -173, -26)),
            new DungeonNode(new Vector3(326, -169, -26)),
            new DungeonNode(new Vector3(328, -161, -26)),
            new DungeonNode(new Vector3(328, -153, -26)),
            new DungeonNode(new Vector3(328, -145, -25)),
            new DungeonNode(new Vector3(328, -137, -25)),
            new DungeonNode(new Vector3(328, -129, -25)),
            new DungeonNode(new Vector3(325, -122, -25)),
            new DungeonNode(new Vector3(321, -115, -25)),
            new DungeonNode(new Vector3(316, -109, -25)),
            new DungeonNode(new Vector3(311, -103, -25)),
            new DungeonNode(new Vector3(307, -96, -25)),
            new DungeonNode(new Vector3(302, -90, -25)),
            new DungeonNode(new Vector3(305, -83, -25)),
            new DungeonNode(new Vector3(308, -76, -25)),
            new DungeonNode(new Vector3(311, -68, -25)),
            new DungeonNode(new Vector3(318, -64, -25)),
            new DungeonNode(new Vector3(326, -64, -25)),
            new DungeonNode(new Vector3(334, -64, -25)),
            new DungeonNode(new Vector3(341, -67, -25)),
            new DungeonNode(new Vector3(347, -73, -25)),
            new DungeonNode(new Vector3(349, -81, -25)),
            new DungeonNode(new Vector3(350, -89, -25)),
            new DungeonNode(new Vector3(348, -97, -25)),
            new DungeonNode(new Vector3(345, -104, -25)),
            new DungeonNode(new Vector3(338, -109, -25)),
            new DungeonNode(new Vector3(330, -107, -25)),
            new DungeonNode(new Vector3(328, -99, -25)),
            new DungeonNode(new Vector3(327, -91, -25)),
            new DungeonNode(new Vector3(327, -83, -25)),
            new DungeonNode(new Vector3(323, -90, -25)),
        };

        public List<string> PriorityUnits { get; } = new List<string>();

        public int RequiredItemLevel { get; } = 62;

        public int RequiredLevel { get; } = 59;

        public Vector3 WorldEntry { get; } = new Vector3(-305, 3167, 31);

        public MapId WorldEntryMapId { get; } = MapId.Outland;
    }
}