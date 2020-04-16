﻿using AmeisenBotX.Core.Data.Enums;
using AmeisenBotX.Core.Dungeon.Enums;
using AmeisenBotX.Core.Dungeon.Objects;
using AmeisenBotX.Core.Jobs.Profiles;
using AmeisenBotX.Core.Movement.Pathfinding.Objects;
using System.Collections.Generic;

namespace AmeisenBotX.Core.Dungeon.Profiles.WotLK
{
    public class UtgardeKeepProfile : IDungeonProfile
    {
        public string Author { get; } = "Jannis";

        public string Description { get; } = "Profile for the Dungeon in the Howling Fjord, made for Level 68 to 80.";

        public DungeonFactionType FactionType { get; } = DungeonFactionType.Neutral;

        public int GroupSize { get; } = 5;

        public MapId MapId { get; } = MapId.UtgardeKeep;

        public int MaxLevel { get; } = 80;

        public string Name { get; } = "[68-80] Utgarde Keep";

        public List<DungeonNode> Path { get; private set; } = new List<DungeonNode>()
        {
            new DungeonNode(new Vector3(157, -85, 13), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(165, -83, 13), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(173, -80, 13), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(181, -78, 14), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(188, -76, 17), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(195, -73, 19), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(202, -71, 21), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(209, -68, 25), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(217, -66, 25), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(224, -63, 25), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(230, -58, 25), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(237, -53, 25), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(244, -48, 25), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(251, -44, 25), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(257, -49, 25), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(261, -56, 25), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(268, -59, 25), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(274, -54, 25), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(280, -49, 25), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(286, -44, 25), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(292, -39, 25), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(299, -36, 25), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(307, -35, 25), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(315, -36, 23), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(322, -40, 23), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(330, -41, 23), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(338, -43, 23), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(346, -45, 23), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(354, -47, 23), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(361, -50, 23), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(369, -51, 23), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(377, -48, 23), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(385, -45, 23), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(392, -41, 23), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(397, -35, 23), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(401, -28, 23), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(398, -21, 23), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(392, -15, 23), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(390, -7, 23), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(388, 1, 23), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(383, 8, 23), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(376, 13, 23), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(372, 20, 23), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(373, 28, 23), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(374, 36, 25), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(374, 44, 25), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(375, 52, 25), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(376, 60, 25), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(377, 68, 28), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(378, 75, 31), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(379, 83, 31), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(378, 91, 31), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(376, 99, 31), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(374, 107, 31), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(371, 114, 31), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(368, 121, 31), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(365, 129, 31), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(362, 136, 31), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(357, 143, 31), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(353, 150, 31), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(350, 157, 31), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(350, 165, 31), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(350, 173, 31), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(349, 181, 31), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(348, 189, 31), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(345, 197, 31), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(342, 205, 31), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(340, 213, 31), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(337, 220, 31), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(335, 228, 31), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(332, 236, 31), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(331, 244, 31), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(329, 252, 31), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(322, 255, 31), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(315, 252, 31), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(308, 249, 31), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(300, 246, 32), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(293, 244, 35), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(286, 241, 37), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(279, 238, 40), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(272, 236, 43), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(265, 233, 43), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(258, 230, 43), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(250, 227, 43), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(243, 225, 41), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(235, 226, 41), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(227, 228, 41), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(219, 229, 41), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(211, 230, 41), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(203, 228, 41), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(197, 222, 41), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(193, 215, 41), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(191, 207, 41), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(191, 199, 41), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(184, 202, 41), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(182, 210, 41), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(179, 217, 41), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(176, 224, 41), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(174, 232, 43), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(171, 239, 43), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(166, 246, 43), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(161, 253, 43), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(157, 260, 43), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(152, 266, 43), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(147, 272, 43), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(140, 276, 43), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(132, 273, 43), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(126, 268, 43), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(121, 262, 43), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(115, 257, 43), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(109, 252, 43), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(103, 246, 43), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(98, 240, 43), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(94, 233, 46), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(91, 226, 49), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(89, 218, 49), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(87, 210, 49), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(85, 202, 49), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(85, 194, 49), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(84, 186, 49), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(84, 178, 49), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(84, 170, 50), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(84, 163, 53), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(85, 156, 56), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(87, 149, 59), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(90, 142, 63), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(92, 135, 66), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(95, 128, 66), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(97, 120, 65), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(100, 113, 65), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(102, 105, 65), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(105, 98, 65), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(110, 92, 66), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(117, 88, 66), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(124, 84, 66), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(130, 79, 66), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(134, 72, 66), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(133, 64, 66), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(130, 57, 66), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(124, 51, 66), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(116, 50, 66), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(108, 50, 66), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(102, 56, 66), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(100, 64, 66), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(98, 71, 68), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(97, 78, 73), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(95, 84, 78), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(92, 91, 82), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(91, 97, 87), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(88, 104, 87), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(87, 112, 87), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(93, 117, 87), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(101, 119, 87), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(109, 121, 87), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(115, 116, 87), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(116, 108, 88), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(118, 102, 93), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(120, 95, 97), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(121, 88, 102), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(124, 82, 107), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(126, 75, 109), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(129, 68, 109), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(126, 61, 109), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(119, 58, 109), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(112, 55, 109), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(105, 52, 109), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(97, 50, 109), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(90, 47, 109), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(83, 45, 111), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(76, 43, 114), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(69, 40, 115), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(61, 38, 115), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(54, 35, 115), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(47, 32, 115), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(40, 29, 115), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(34, 23, 115), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(34, 15, 115), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(37, 8, 115), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(40, 1, 116), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(44, -6, 119), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(47, -13, 119), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(51, -20, 119), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(56, -26, 119), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(62, -31, 119), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(69, -34, 119), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(77, -36, 119), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(85, -37, 119), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(93, -37, 119), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(101, -38, 119), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(109, -38, 119), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(117, -37, 119), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(124, -35, 121), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(131, -33, 124), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(138, -31, 127), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(144, -28, 131), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(151, -26, 134), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(159, -23, 135), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(167, -20, 135), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(175, -17, 135), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(183, -14, 135), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(190, -11, 135), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(196, -6, 135), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(202, -1, 135), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(208, 5, 135), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(214, 10, 135), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(221, 14, 135), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(228, 17, 135), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(236, 17, 135), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(242, 12, 135), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(242, 4, 135), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(239, -4, 135), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(234, -10, 135), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(227, -13, 135), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(220, -16, 136), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(213, -18, 140), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(207, -21, 145), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(201, -24, 149), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(194, -25, 153), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(187, -25, 157), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(179, -25, 157), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(172, -22, 157), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(170, -14, 157), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(173, -7, 157), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(180, -4, 157), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(186, -1, 161), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(193, 1, 166), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(200, 3, 170), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(206, 5, 175), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(213, 6, 178), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(221, 9, 179), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(229, 10, 179), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(233, 3, 179), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(233, -5, 179), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(236, -12, 179), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(240, -19, 179), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(243, -27, 179), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(245, -34, 181), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(247, -41, 183), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(249, -48, 185), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(252, -55, 187), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(254, -62, 191), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(257, -69, 191), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(260, -77, 190), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(262, -85, 190), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(265, -92, 190), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(268, -99, 190), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(271, -106, 190), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(273, -114, 190), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(275, -122, 190), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(275, -130, 190), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(274, -138, 190), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(270, -145, 190), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(264, -150, 190), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(258, -155, 190), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(251, -158, 190), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(243, -161, 190), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(235, -161, 190), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(227, -160, 190), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(220, -157, 190), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(212, -155, 190), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(204, -154, 190), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(197, -157, 190), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(190, -161, 187), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(183, -164, 185), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(176, -167, 182), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(169, -170, 181), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(162, -173, 181), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(157, -179, 181), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(161, -186, 180), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(165, -193, 180), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(169, -200, 180), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(173, -207, 180), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(177, -214, 181), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(181, -221, 180), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(184, -228, 181), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(188, -235, 181), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(192, -242, 180), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(196, -249, 181), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(200, -256, 181), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(204, -263, 180), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(208, -270, 180), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(212, -277, 180), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(216, -284, 180), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(219, -291, 180), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(223, -298, 180), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(227, -305, 180), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(231, -312, 180), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(234, -319, 180), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(238, -326, 180), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(242, -333, 180), DungeonNodeType.Normal),
            new DungeonNode(new Vector3(241, -325, 180), DungeonNodeType.Normal),
        };

        public List<string> PriorityUnits { get; } = new List<string>() { "Frost Tomb" };

        public int RequiredItemLevel { get; } = 100;

        public int RequiredLevel { get; } = 68;

        public Vector3 WorldEntry { get; } = new Vector3(1238, -4860, 41);

        public MapId WorldEntryMapId { get; } = MapId.Northrend;
    }
}