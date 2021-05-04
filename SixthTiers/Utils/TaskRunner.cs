﻿using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Map;
using Assets.Scripts.Models.Profile;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors.Attack;
using Assets.Scripts.Simulation;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.Bridge;
using Assets.Scripts.Unity.Player;
using Assets.Scripts.Unity.UI_New.InGame;
using Harmony;
using MelonLoader;
using UnhollowerRuntimeLib;

namespace SixthTiers.Utils {
    public class TaskRunner {
        [HarmonyPatch(typeof(InGame), nameof(InGame.Update))]
        public static class Update {
            [HarmonyPostfix]
            public static void Postfix(ref InGame __instance) {
                if (__instance == null) { RunLeave(); return; }
                if (__instance.bridge == null) return;

                var allTowers = __instance.bridge.GetAllTowers();
                var allSixthTiers = SixthTier.towers;
                for (var indexOfTowers = 0; indexOfTowers < allTowers.Count; indexOfTowers++) {
                    var towerToSimulation = allTowers.ToArray()[indexOfTowers];
                    for (var indexOfSixthTiers = 0; indexOfSixthTiers < allSixthTiers.Count; indexOfSixthTiers++) {
                        if (!allSixthTiers[indexOfSixthTiers].requirements(towerToSimulation)) continue;
                        if (towerToSimulation.tower.namedMonkeyName != SixthTier.towers[indexOfSixthTiers].identifier)
                            SixthTier.towers[indexOfSixthTiers].onComplete(towerToSimulation);
                        else if (towerToSimulation.tower.namedMonkeyName == SixthTier.towers[indexOfTowers].identifier)
                            SixthTier.towers[indexOfSixthTiers].recurring(towerToSimulation);
                    }
                }
            }

            private static void RunLeave() { for (var towerIndex = SixthTier.towers.Count - 1; towerIndex >= 0; towerIndex--) SixthTier.towers[towerIndex].onLeave(); }
        }
    }
}