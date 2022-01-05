namespace GodlyTowers.Towers {
    class Grim_Reaper {
        public static string name = "GrimReaper";

        public static UpgradeModel[] GetUpgrades() {
            return new UpgradeModel[]
            {
                    new("Stronger Souls", 750, 0, new("StrongerSouls"), 0, 0, 0, "", "Stronger Souls"),
                    new("Infernal Flames", 1500, 0, new("HellishFlames"), 0, 1, 0, "", "Infernal Flames"),
                    new("Fast Death", 8000, 0, new("FastDeath"), 0, 2, 0, "", "Fast Death"),
                    new("Bring Back The Dead", 13450, 0, new("BringBackTheDead"), 0, 3, 0, "", "Bring Back The Dead"),
                    new("Rage of The Fallen", 666666, 0, new("RageOfFallen"), 0, 4, 0, "", "Rage of The Fallen")
            };
        }

        public static (TowerModel, ShopTowerDetailsModel, TowerModel[], UpgradeModel[]) GetTower(GameModel gameModel) {
            var grimreaperDetails = gameModel.towerSet[0].Clone().Cast<ShopTowerDetailsModel>();
            grimreaperDetails.towerId = name;
            grimreaperDetails.towerIndex = 35;

            if (!LocalizationManager.Instance.textTable.ContainsKey("GrimReaper Description"))
                LocalizationManager.Instance.textTable.Add("GrimReaper Description", "The Grim Reaper is personified frequently as the bringer of death. To bloons he is a bridge to the otherworld. He is ready at any moment to take any number of them across to their final destination.");

            if (!LocalizationManager.Instance.textTable.ContainsKey("GrimReaper"))
                LocalizationManager.Instance.textTable.Add("GrimReaper", "The Grim Reaper");

            string[] upgradeNames = new[] { "Stronger Souls", "Infernal Flames", "Fast Death", "Bring Back The Dead", "Rage of The Fallen" };
            string[] upgradeDescriptions = new[] { "Reaper gains 30% more speed.", "Reaper can target camo bloons with the flames from the otherworld.", "Instantly spike down the strongest bloon nearby!", "Summon bloons from the \"Unpopped Army\".", "Boost all nearby towers with your crazed screams." };

            for (int i = 0; i < upgradeNames.Length; i++) {
                if (!LocalizationManager.Instance.textTable.ContainsKey(upgradeNames[i] + " Description"))
                    LocalizationManager.Instance.textTable.Add(upgradeNames[i] + " Description", upgradeDescriptions[i]);
            }

            return (GetT0(gameModel), grimreaperDetails, new[] { GetT0(gameModel), GetT1(gameModel), GetT2(gameModel), GetT3(gameModel), GetT4(gameModel), GetT5(gameModel) }, GetUpgrades());
        }


        public static TowerModel GetT0(GameModel gameModel) {
            var ttu = gameModel.towers.First(tower => tower.name.Equals("SniperMonkey")).Clone().Cast<TowerModel>();
            ttu.mods = new ApplyModModel[0];
            ttu.name = name;
            ttu.baseId = name;
            ttu.portrait = new SpriteReference("GrimReaperIcon");
            ttu.icon = new SpriteReference("GrimReaperIcon");
            ttu.emoteSpriteLarge = new SpriteReference("Dark");
            ttu.display = "GrimReaper0";
            ttu.towerSet = "Magic";
            ttu.dontDisplayUpgrades = false;
            ttu.cost = 1250;
            ttu.upgrades = new UpgradePathModel[] { new UpgradePathModel("Stronger Souls", name + "-100") };

            for (int i = 0; i < ttu.behaviors.Length; i++) {
                if (ttu.behaviors[i].GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = ttu.behaviors[i].Cast<AttackModel>();
                    att.weapons[0].rate = 0.95f;
                    att.weapons[0].rate *= 5;
                    att.weapons[0].behaviors = new WeaponBehaviorModel[0];
                    att.weapons[0].projectile.filters = new FilterModel[0];
                    att.range = ttu.range;
                    att.weapons[0].projectile.pierce = 99999999;
                    att.weapons[0].projectile.ignorePierceExhaustion = true; // Might be redundant

                    CreateEffectOnContactModel ceoefm = new("ceoefm_", new EffectModel("em_", "fd5b2424c798c7444bc526c186a3f6ec", 1, 1, false, false, false, false, false, false, false));

                    att.weapons[0].projectile.behaviors = att.weapons[0].projectile.behaviors.Add(ceoefm);

                    for (int j = 0; j < att.weapons[0].projectile.behaviors.Length; j++) {
                        if (att.weapons[0].projectile.behaviors[j].GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                            att.weapons[0].projectile.behaviors[j].Cast<DamageModel>().damage = 9999999999999999999999999999;
                        }
                    }
                }


                if (ttu.behaviors[i].GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                    ttu.behaviors[i].Cast<DisplayModel>().display = ttu.display;
                }
            }

            return ttu;
        }

        public static TowerModel GetT1(GameModel gameModel) {
            TowerModel ttu = GetT0(gameModel).Clone().Cast<TowerModel>();
            ttu.behaviors.FirstOrDefault(display => display.GetIl2CppType() == Il2CppType.Of<DisplayModel>()).Cast<DisplayModel>().display = "GrimReaper1";
            ttu.name = name + "-100";
            ttu.tier = 1;
            ttu.tiers = new int[] { 1, 0, 0 };
            ttu.display = "GrimReaper1";
            ttu.appliedUpgrades = new string[] { "Stronger Souls" };
            ttu.upgrades = new UpgradePathModel[] { new UpgradePathModel("Infernal Flames", name + "-200") };

            for (int i = 0; i < ttu.behaviors.Length; i++) {
                if (ttu.behaviors[i].GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = ttu.behaviors[i].Cast<AttackModel>();
                    att.weapons[0].rate *= 0.7f;
                }
            }

            return ttu;
        }

        public static TowerModel GetT2(GameModel gameModel) {
            TowerModel ttu = GetT1(gameModel).Clone().Cast<TowerModel>();
            ttu.behaviors.FirstOrDefault(display => display.GetIl2CppType() == Il2CppType.Of<DisplayModel>()).Cast<DisplayModel>().display = "GrimReaper2";
            ttu.name = name + "-200";
            ttu.tier = 2;
            ttu.tiers = new int[] { 2, 0, 0 };
            ttu.display = "GrimReaper2";
            ttu.appliedUpgrades = new string[] { "Stronger Souls", "Infernal Flames" };
            ttu.upgrades = new UpgradePathModel[] { new UpgradePathModel("Fast Death", name + "-300") };
            ttu.behaviors = ttu.behaviors.Add(new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true));

            return ttu;
        }

        public static TowerModel GetT3(GameModel gameModel) {
            AbilityModel abm = gameModel.towers.FirstOrDefault(tower => tower.name.Contains("MonkeyBuccaneer-250")).behaviors.FirstOrDefault(ability => ability.name.Contains("Takedown")).Clone().Cast<AbilityModel>();
            TowerModel ttu = GetT2(gameModel).Clone().Cast<TowerModel>();
            ttu.behaviors.FirstOrDefault(display => display.GetIl2CppType() == Il2CppType.Of<DisplayModel>()).Cast<DisplayModel>().display = "GrimReaper3";
            ttu.name = name + "-300";
            ttu.tier = 3;
            ttu.tiers = new int[] { 3, 0, 0 };
            ttu.display = "GrimReaper3";
            ttu.appliedUpgrades = new string[] { "Stronger Souls", "Infernal Flames", "Fast Death" };
            ttu.upgrades = new UpgradePathModel[] { new UpgradePathModel("Bring Back The Dead", name + "-400") };
            ttu.behaviors = ttu.behaviors.Add(abm);

            return ttu;
        }

        public static TowerModel GetT4(GameModel gameModel) {
            TowerModel ttu = GetT3(gameModel).Clone().Cast<TowerModel>();
            ttu.behaviors.FirstOrDefault(display => display.GetIl2CppType() == Il2CppType.Of<DisplayModel>()).Cast<DisplayModel>().display = "GrimReaper4";
            ttu.name = name + "-400";
            ttu.tier = 4;
            ttu.tiers = new int[] { 4, 0, 0 };
            ttu.display = "GrimReaper4";
            ttu.appliedUpgrades = new string[] { "Stronger Souls", "Infernal Flames", "Fast Death", "Bring Back The Dead" };
            ttu.upgrades = new UpgradePathModel[] { new UpgradePathModel("Rage of The Fallen", name + "-500") };
            AttackModel am = gameModel.towers.FirstOrDefault(tower => tower.name.Contains("WizardMonkey-025")).behaviors.FirstOrDefault(behavior => behavior.name.Contains("Necromancer_")).Clone().Cast<AttackModel>();
            foreach (var tmp in am.weapons)
                tmp.animation = 0;
            ttu.behaviors = ttu.behaviors.Add(am);
            ttu.behaviors = ttu.behaviors.Add(gameModel.towers.FirstOrDefault(tower => tower.name.Contains("WizardMonkey-025")).behaviors.FirstOrDefault(behavior => behavior.name.Contains("NecromancerZoneModel_")).Clone());

            return ttu;
        }

        public static TowerModel GetT5(GameModel gameModel) {
            TowerModel ttu = GetT4(gameModel).Clone().Cast<TowerModel>();
            ttu.behaviors.FirstOrDefault(display => display.GetIl2CppType() == Il2CppType.Of<DisplayModel>()).Cast<DisplayModel>().display = "GrimReaper5";
            ttu.name = name + "-500";
            ttu.tier = 5;
            ttu.tiers = new int[] { 5, 0, 0 };
            ttu.display = "GrimReaper5";
            ttu.appliedUpgrades = new string[] { "Stronger Souls", "Infernal Flames", "Fast Death", "Bring Back The Dead", "Rage of The Fallen" };
            ttu.upgrades = new UpgradePathModel[] { };

            for (int i = 0; i < ttu.behaviors.Length; i++) {
                if (ttu.behaviors[i].GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = ttu.behaviors[i].Cast<AttackModel>();
                    att.weapons[0].rate *= 0.2785f;
                }
            }



            return ttu;
        }
    }

    [HarmonyPatch(typeof(Factory), nameof(Factory.FindAndSetupPrototypeAsync))]
    public class PrototypeUDN_Patch {
        public static Dictionary<string, UnityDisplayNode> protos = new();

        [HarmonyPrefix]
        public static bool Prefix(Factory __instance, string objectId, Il2CppSystem.Action<UnityDisplayNode> onComplete) {
            if (!protos.ContainsKey(objectId)) {
                if (objectId.Equals("GrimReaper0")) {
                    UnityDisplayNode udn = null;
                    __instance.FindAndSetupPrototypeAsync("191cc21b4fb5dfa4ba4b81565d2a5d4c", new Action<UnityDisplayNode>(btdUdn => {
                        var instance = Object.Instantiate(btdUdn, __instance.PrototypeRoot);
                        instance.name = objectId + "(Clone)";
                        instance.RecalculateGenericRenderers();
                        foreach (Renderer r in instance.genericRenderers)
                            try {
                                r.material.mainTexture = LoadTextureFromBytes(Properties.Resources.GrimReaper0);
                            } catch {
                            }

                        udn = instance;
                        onComplete.Invoke(udn);
                    }));
                    return false;
                }
                if (objectId.Equals("GrimReaper1")) {
                    UnityDisplayNode udn = null;
                    __instance.FindAndSetupPrototypeAsync("191cc21b4fb5dfa4ba4b81565d2a5d4c", new Action<UnityDisplayNode>(btdUdn => {
                        var instance = Object.Instantiate(btdUdn, __instance.PrototypeRoot);
                        instance.name = objectId + "(Clone)";
                        instance.RecalculateGenericRenderers();
                        foreach (Renderer r in instance.genericRenderers)
                            if (r.name.Contains("SpookyFarmer"))
                                try {
                                    r.material.mainTexture = LoadTextureFromBytes(Properties.Resources.GrimReaper1);
                                } catch {
                                }

                        udn = instance;
                    }));
                    udn.name = objectId;
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }
                if (objectId.Equals("GrimReaper2")) {
                    UnityDisplayNode udn = null;
                    __instance.FindAndSetupPrototypeAsync("191cc21b4fb5dfa4ba4b81565d2a5d4c", new Action<UnityDisplayNode>(btdUdn => {
                        var instance = Object.Instantiate(btdUdn, __instance.PrototypeRoot);
                        instance.name = objectId + "(Clone)";
                        instance.RecalculateGenericRenderers();
                        foreach (Renderer r in instance.genericRenderers)
                            if (r.name.Contains("SpookyFarmer"))
                                try {
                                    r.material.mainTexture = LoadTextureFromBytes(Properties.Resources.GrimReaper2);
                                } catch {
                                }

                        udn = instance;
                    }));
                    udn.name = objectId;
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }
                if (objectId.Equals("GrimReaper3")) {
                    UnityDisplayNode udn = null;
                    __instance.FindAndSetupPrototypeAsync("191cc21b4fb5dfa4ba4b81565d2a5d4c", new Action<UnityDisplayNode>(btdUdn => {
                        var instance = Object.Instantiate(btdUdn, __instance.PrototypeRoot);
                        instance.name = objectId + "(Clone)";
                        instance.RecalculateGenericRenderers();
                        foreach (Renderer r in instance.genericRenderers)
                            if (r.name.Contains("SpookyFarmer"))
                                try {
                                    r.material.mainTexture = LoadTextureFromBytes(Properties.Resources.GrimReaper3);
                                } catch {
                                }

                        udn = instance;
                    }));
                    udn.name = objectId;
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }
                if (objectId.Equals("GrimReaper4")) {
                    UnityDisplayNode udn = null;
                    __instance.FindAndSetupPrototypeAsync("191cc21b4fb5dfa4ba4b81565d2a5d4c", new Action<UnityDisplayNode>(btdUdn => {
                        var instance = Object.Instantiate(btdUdn, __instance.PrototypeRoot);
                        instance.name = objectId + "(Clone)";
                        instance.RecalculateGenericRenderers();
                        foreach (Renderer r in instance.genericRenderers)
                            if (r.name.Contains("SpookyFarmer"))
                                try {
                                    r.material.mainTexture = LoadTextureFromBytes(Properties.Resources.GrimReaper4);
                                } catch {
                                }

                        udn = instance;
                    }));
                    udn.name = objectId;
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }
                if (objectId.Equals("GrimReaper5")) {
                    UnityDisplayNode udn = null;
                    __instance.FindAndSetupPrototypeAsync("191cc21b4fb5dfa4ba4b81565d2a5d4c", new Action<UnityDisplayNode>(btdUdn => {
                        var instance = Object.Instantiate(btdUdn, __instance.PrototypeRoot);
                        instance.name = objectId + "(Clone)";
                        instance.RecalculateGenericRenderers();
                        foreach (Renderer r in instance.genericRenderers)
                            if (r.name.Contains("SpookyFarmer"))
                                try {
                                    r.material.mainTexture = LoadTextureFromBytes(Properties.Resources.GrimReaper5);
                                } catch {
                                }

                        udn = instance;
                    }));
                    udn.name = objectId;
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }
            }

            if (protos.ContainsKey(objectId)) {
                onComplete.Invoke(protos[objectId]);
                return false;
            }

            return true;
        }


        private static Texture2D LoadTextureFromBytes(byte[] FileData) {
            Texture2D Tex2D = new(2, 2);
            Tex2D.filterMode = FilterMode.Trilinear;
            if (ImageConversion.LoadImage(Tex2D, FileData)) return Tex2D;

            return null;
        }
    }

    [HarmonyPatch(typeof(ResourceLoader), nameof(ResourceLoader.LoadSpriteFromSpriteReferenceAsync))]
    public sealed class ResourceLoader_Patch {
        [HarmonyPostfix]
        public static void Postfix(SpriteReference reference, ref Image image) {
            if (reference != null && reference.guidRef.Equals("GrimReaperIcon"))
                try {
                    var text = LoadTextureFromBytes(Properties.Resources.ReaperPortrait).Cast<Texture2D>();
                    image.canvasRenderer.SetTexture(text);
                    image.sprite = LoadSprite(text);
                } catch { }
            if (reference != null && reference.guidRef.Equals("StrongerSouls"))
                try {
                    var text = LoadTextureFromBytes(Properties.Resources.GrimReaperStrongerSouls).Cast<Texture2D>();
                    image.canvasRenderer.SetTexture(text);
                    image.sprite = LoadSprite(text);
                } catch { }
            if (reference != null && reference.guidRef.Equals("HellishFlames"))
                try {
                    var text = LoadTextureFromBytes(Properties.Resources.GrimReaperHellishFlames).Cast<Texture2D>();
                    image.canvasRenderer.SetTexture(text);
                    image.sprite = LoadSprite(text);
                } catch { }
            if (reference != null && reference.guidRef.Equals("FastDeath"))
                try {
                    var text = LoadTextureFromBytes(Properties.Resources.GrimReaperFastDeath).Cast<Texture2D>();
                    image.canvasRenderer.SetTexture(text);
                    image.sprite = LoadSprite(text);
                } catch { }
            if (reference != null && reference.guidRef.Equals("BringBackTheDead"))
                try {
                    var text = LoadTextureFromBytes(Properties.Resources.GrimReaperBringBackTheDead).Cast<Texture2D>();
                    image.canvasRenderer.SetTexture(text);
                    image.sprite = LoadSprite(text);
                } catch { }
            if (reference != null && reference.guidRef.Equals("RageOfFallen"))
                try {
                    var text = LoadTextureFromBytes(Properties.Resources.GrimReaperRageOfFallen).Cast<Texture2D>();
                    image.canvasRenderer.SetTexture(text);
                    image.sprite = LoadSprite(text);
                } catch { }
        }


        private static Texture2D LoadTextureFromBytes(byte[] FileData) {
            Texture2D Tex2D = new(2, 2);
            Tex2D.filterMode = FilterMode.Trilinear;
            if (ImageConversion.LoadImage(Tex2D, FileData)) return Tex2D;

            return null;
        }

        private static Sprite LoadSprite(Texture2D text) {
            return Sprite.Create(text, new(0, 0, text.width, text.height), new());
        }
    }
}
