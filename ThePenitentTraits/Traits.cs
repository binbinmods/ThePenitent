﻿using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using Obeliskial_Content;
using UnityEngine;
using static UnityEngine.Mathf;
using System.Text.RegularExpressions;
using static UnityEngine.Object;
using static ThePenitent.Plugin;
using static ThePenitent.CustomFunctions;
using System.Collections;
using System.Runtime.CompilerServices;


namespace ThePenitent
{
    [HarmonyPatch]
    internal class Traits
    {
        // list of your trait IDs
        public static string heroName = "cain";

        public static string subclassname = "penitent";

        public static string[] simpleTraitList = ["trait0", "trait1a", "trait1b", "trait2a", "trait2b", "trait3a", "trait3b", "trait4a", "trait4b"];

        public static string[] myTraitList = (string[])simpleTraitList.Select(trait => subclassname + trait); // Needs testing

        public static int level5ActivationCounter = 0;
        public static int level5MaxActivations = 3;

        // public static int petCainCounter = 0;

        // private static Coroutine petCainCoroutine;


        public static string trait0 = myTraitList[0];
        public static string trait2a = myTraitList[3];
        public static string trait2b = myTraitList[4];
        public static string trait4a = myTraitList[7];
        public static string trait4b = myTraitList[8];

        public static int nInjuries = 0;


        public static void DoCustomTrait(string _trait, ref Trait __instance)
        {
            // get info you may need
            Enums.EventActivation _theEvent = Traverse.Create(__instance).Field("theEvent").GetValue<Enums.EventActivation>();
            Character _character = Traverse.Create(__instance).Field("character").GetValue<Character>();
            Character _target = Traverse.Create(__instance).Field("target").GetValue<Character>();
            int _auxInt = Traverse.Create(__instance).Field("auxInt").GetValue<int>();
            string _auxString = Traverse.Create(__instance).Field("auxString").GetValue<string>();
            CardData _castedCard = Traverse.Create(__instance).Field("castedCard").GetValue<CardData>();
            Traverse.Create(__instance).Field("character").SetValue(_character);
            Traverse.Create(__instance).Field("target").SetValue(_target);
            Traverse.Create(__instance).Field("theEvent").SetValue(_theEvent);
            Traverse.Create(__instance).Field("auxInt").SetValue(_auxInt);
            Traverse.Create(__instance).Field("auxString").SetValue(_auxString);
            Traverse.Create(__instance).Field("castedCard").SetValue(_castedCard);
            TraitData traitData = Globals.Instance.GetTraitData(_trait);
            List<CardData> cardDataList = [];
            List<string> heroHand = MatchManager.Instance.GetHeroHand(_character.HeroIndex);
            Hero[] teamHero = MatchManager.Instance.GetTeamHero();
            NPC[] teamNpc = MatchManager.Instance.GetTeamNPC();

            // activate traits
            // I don't know how to set the combatLog text I need to do that for all of the traits

            if (_trait == trait0)
            { // TODO trait 0
              // Weak does not reduce this hero’s damage, but Powerful does. Gain 5% damage for each unique Curse on this hero.
              // Done in GACM and GetTraitDamagePercentModifiers
                string traitId = _trait;

            }


            else if (_trait == trait2a)
            { // TODO trait 2a
                string traitId = _trait;
                // Draw 2 cards, gain 1 Energy, and gain 1 Vitality when you play an Injury (3x/turn)
                if (CanIncrementTraitActivations(traitId) && _castedCard.HasCardType(Enums.CardType.Injury) && IsLivingHero(_character))
                {
                    DrawCards(2);
                    GainEnergy(ref _character, 1, traitData: traitData);
                    _character.SetAuraTrait(_character, "vitality", 1);
                    IncrementTraitActivations(traitId);
                }

            }



            else if (_trait == trait2b)
            { // TODO trait 2b
                string traitId = _trait;
                // All Resists -10%. Gain 3 Zeal at the start of each round. When you apply apply Vitality to a different hero, steal 1 curse from them. 
                // Done in Begin Round

                if (IsLivingHero(_character) && IsLivingHero(_target) && _auxString == "vitality" && _character != _target)
                {
                    LogDebug($"Trait: {_trait}: attempting to steal curses");

                    StealAuraCurses(ref _character, ref _target, 1, IsAuraOrCurse.Curse);
                }

            }

            else if (_trait == trait4a)
            { // TODO trait 4a
                string traitId = _trait;
                // Zeal increases all damage by 1 per charge. Increases by 1 for each injury in your starting deck. 
                // done in GACM

            }

            else if (_trait == trait4b)
            { // TODO trait 4b
                string traitId = _trait;
                // Once per turn, when you heal a hero, apply Vitality equal to 10% of all curses on this hero. Increase this by 3% for every injury in your starting deck.
                LogDebug("Trait 4b - 1");
                if (CanIncrementTraitActivations(traitId) && IsLivingHero(_character) && IsLivingHero(_target))
                {
                    LogDebug("Trait 4b - 2");
                    int nCurseCharges = CountAllACOnCharacter(_character, IsAuraOrCurse.Curse);
                    float multiplier = 0.10f + 0.03f * nInjuries;
                    int toApply = FloorToInt(nCurseCharges * multiplier);
                    _target.SetAuraTrait(_character, "vitality", toApply);
                    IncrementTraitActivations(traitId);

                }
            }

        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Trait), nameof(Trait.DoTrait))]
        public static bool DoTrait(Enums.EventActivation _theEvent, string _trait, Character _character, Character _target, int _auxInt, string _auxString, CardData _castedCard, ref Trait __instance)
        {
            if ((UnityEngine.Object)MatchManager.Instance == (UnityEngine.Object)null)
                return false;
            Traverse.Create(__instance).Field("character").SetValue(_character);
            Traverse.Create(__instance).Field("target").SetValue(_target);
            Traverse.Create(__instance).Field("theEvent").SetValue(_theEvent);
            Traverse.Create(__instance).Field("auxInt").SetValue(_auxInt);
            Traverse.Create(__instance).Field("auxString").SetValue(_auxString);
            Traverse.Create(__instance).Field("castedCard").SetValue(_castedCard);
            if (Content.medsCustomTraitsSource.Contains(_trait) && myTraitList.Contains(_trait))
            {
                DoCustomTrait(_trait, ref __instance);
                return false;
            }
            return true;
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(AtOManager), nameof(AtOManager.GlobalAuraCurseModificationByTraitsAndItems))]
        [HarmonyPriority(Priority.Low)]
        public static void GlobalAuraCurseModificationByTraitsAndItemsPostfix(ref AtOManager __instance,
                                                                            ref AuraCurseData __result,
                                                                            string _type,
                                                                            string _acId,
                                                                            Character _characterCaster,
                                                                            Character _characterTarget)
        {
            // trait0: Weak does not reduce this hero’s damage, but Powerful does. Gain 5% damage for each unique Curse on this hero.

            // trait4a: Zeal on this hero increases all damage by 1 per charge. Increases by 1 for each injury in your starting deck. 
            Character characterOfInterest = _type == "set" ? _characterTarget : _characterCaster;
            switch (_acId)
            {
                case "zeal":
                    if (IfCharacterHas(characterOfInterest, CharacterHas.Trait, trait4a, AppliesTo.ThisHero))
                    {
                        LogDebug($"penitent has {trait0} with {nInjuries} injuries");

                        __result.AuraDamageType = Enums.DamageType.All;
                        __result.AuraDamageIncreasedPerStack = 1 + nInjuries;
                        __result.HealDonePerStack = 1+nInjuries;
                    }
                    break;
                case "powerful":
                    if (IfCharacterHas(characterOfInterest, CharacterHas.Trait, trait0, AppliesTo.ThisHero))
                    {
                        LogDebug($"penitent has {trait0} for powerful");
                        __result.AuraDamageIncreasedPercentPerStack = -5;
                    }
                    break;
                case "weak":
                    if (IfCharacterHas(characterOfInterest, CharacterHas.Trait, trait0, AppliesTo.ThisHero))
                    {
                        LogDebug($"penitent has {trait0} for weak");
                        __result.AuraDamageType = Enums.DamageType.None;
                        __result.AuraDamageIncreasedPercent = 0;
                        __result.HealDonePercent=0;
                    }
                    break;
                case "vitality":
                    if (IfCharacterHas(characterOfInterest, CharacterHas.Trait, trait4b, AppliesTo.ThisHero))
                    {
                        LogDebug($"penitent has {trait4b}");
                        __result.AuraDamageType = Enums.DamageType.All;
                        // __result.AuraDamageIncreasedPerStack = FloorToInt(characterOfInterest.GetAuraCharges("vitality")*0.14286f);
                        __result.AuraDamageIncreasedPerStack = FloorToInt(characterOfInterest.GetAuraCharges("vitality")*10);
                    }
                    break;
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Character), nameof(Character.GetTraitDamagePercentModifiers))]
        public static void GetTraitDamagePercentModifiersPostfix(ref Character __instance, ref float __result, Enums.DamageType DamageType)
        {
            // trait0: Gain 5% damage for each unique Curse on this hero.
            if (IsLivingHero(__instance) && AtOManager.Instance.CharacterHaveTrait(__instance.SubclassName, trait0)&& MatchManager.Instance!=null)
            {
                LogDebug("GetTraitDamagePercentModifiersPostfix");
                int nCurses = __instance.GetCurseList().Count();
                LogDebug("GetTraitDamagePercentModifiersPostfix - nCurses = " + nCurses);
                int modifierPer = 5;
                __result += nCurses * modifierPer;
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Character), nameof(Character.SetEvent))]
        public static void SetEventPostfix(ref Character __instance,
                Enums.EventActivation theEvent,
                Character target = null,
                int auxInt = 0,
                string auxString = "")
        {
            // Count the number of injuries
            if (theEvent == Enums.EventActivation.BeginCombat && MatchManager.Instance != null && Globals.Instance != null && __instance != null && __instance.Id == subclassname)
            {
                LogDebug("Counting Injuries");
                nInjuries = GetDeck(__instance).Count(card => Globals.Instance.GetCardData(card).HasCardType(Enums.CardType.Injury));
                LogDebug($"nInjuries: {nInjuries} and total cards in deck: {__instance.Cards.Count()}");
            }
        }

        // [HarmonyPostfix]
        // [HarmonyPatch(typeof(Character), nameof(Character.BeginRound))]
        // public static void BeginRoundPostfix(ref Character __instance)
        // {
        //     // trait2b: Gain 3 Zeal at start of round
        //     LogDebug("BeginRoundPostfix for penitent");
        //     if (__instance.HasEffect(trait2b)&&IsLivingHero(__instance))
        //     {
        //         __instance.SetAuraTrait(__instance, "zeal", 3);
        //     }
        // }
        // [HarmonyPostfix]
        // [HarmonyPatch(typeof(CharacterItem), nameof(CharacterItem.fOnMouseEnter))]
        // public static void fOnMouseEnterPostfix(ref CharacterItem __instance)
        // {
        //     if (AlertManager.Instance.IsActive() || SettingsManager.Instance.IsActive())
        //         return;

        //     if (MatchManager.Instance.CardDrag)
        //         return;

        //     Hero _hero = Traverse.Create(__instance).Field("_hero").GetValue<Hero>(); ;
        //     if (_hero != null)
        //     {
        //         LogDebug("fOnMouseEnterPostfix attempting to set Cain Text PetCain");
        //         MatchManager.Instance.combatTarget.SetTargetTMP((Character)_hero);
        //         if (_hero.SourceName == "Cain")
        //         {
        //             PetCain(__instance, _hero);

        //         }
        //     }
        // }


        // public static void PetCain(CharacterItem __instance, Hero _hero)
        // {
        //     LogDebug("PetCain");
        //     if (!(bool)(UnityEngine.Object)MatchManager.Instance)
        //         return;
        //     ++petCainCounter;
            
        //     Animator anim = Traverse.Create(__instance).Field("anim").GetValue<Animator>(); ;
        //     anim.ResetTrigger("pet");
        //     anim.SetTrigger("pet");
        //     // petCainCounter = 0;
        //     if (petCainCounter%9 == 2 )
        //     {
        //         string text = "Pie Iesu Domine.";
        //         LogDebug("PetCain " + text);
        //         MatchManager.Instance.DoComic((Character)_hero, text, 2f);
        //     }
        //     else if (petCainCounter%9 == 5)
        //     {
        //         string text = "Dona Eis Requiem.";
        //         LogDebug("PetCain " + text);
        //         MatchManager.Instance.DoComic((Character)_hero, text, 2f);
        //     }
        //     else if (petCainCounter%9 == 8)
        //     {
        //         string text = "*thunk*";
        //         LogDebug("PetCain " + text);
        //         MatchManager.Instance.DoComic((Character)_hero, text, 2f);
        //     }
            
        //     if (petCainCoroutine != null)
        //         __instance.StopCoroutine(petCainCoroutine);
        //     petCainCoroutine = __instance.StartCoroutine(PetCainStop());
        // }

        // private static IEnumerator PetCainStop()
        // {
        //     yield return (object)Globals.Instance.WaitForSeconds(1.5f);
        //     petCainCounter = 0;
        // }


    }
}
