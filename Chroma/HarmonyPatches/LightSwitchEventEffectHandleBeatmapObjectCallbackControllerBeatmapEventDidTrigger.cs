﻿using Chroma.Beatmap.ChromaEvents;
using Chroma.Beatmap.Events;
using Chroma.Beatmap.JSON;
using Chroma.Beatmap.Z_Testing.ChromaEvents;
using Chroma.Extensions;
using Chroma.Settings;
using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Chroma.HarmonyPatches {

    [HarmonyPriority(Priority.High)]
    [HarmonyPatch(typeof(LightSwitchEventEffect))]
    [HarmonyPatch("HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger")]
    class LightSwitchEventEffectHandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger {

        public static void ResetRandom() {
            ChromaLogger.Log("Resetting techniLightRandom Random 408");
            techniLightRandom = new System.Random(408);
        }

        private static System.Random techniLightRandom = new System.Random(408);

        static bool Prefix(LightSwitchEventEffect __instance, ref BeatmapEventData beatmapEventData, ref BeatmapEventType ____event) {

            if (beatmapEventData.value == ChromaJSONEventData.GLOBAL_DO_NOTHING_VALUE) return false;

            try {

                // https://docs.google.com/spreadsheets/d/1vCTlDvx0ZW8NkkZBYW6ecvXaVRxDUKX7QIoah9PCp_c/edit#gid=0
                if (ColourManager.TechnicolourLights && (int)____event <= 4) { //0-4 are actual lighting events, we don't want to bother with anything else like ring spins or custom events
                                                                               //System.Random noteRandom = new System.Random(Mathf.FloorToInt(beatmapEventData.time * 408));
                    if (techniLightRandom.NextDouble() < ChromaConfig.TechnicolourLightsFrequency) {
                        if (beatmapEventData.value <= 3) { //Blue events are 1, 2 and 3
                            switch (ChromaConfig.TechnicolourLightsGrouping) {
                                case ColourManager.TechnicolourLightsGrouping.ISOLATED:
                                    //LightsIsolatedTechnicolour.Activate(__instance, ____event, ChromaConfig.TechnicolourLightsStyle, false, beatmapEventData.time);
                                    MayhemEvent.ActivateTechnicolour(beatmapEventData, __instance, ____event);
                                    return false;
                                case ColourManager.TechnicolourLightsGrouping.ISOLATED_GROUP:
                                    __instance.SetLightingColourB(ColourManager.GetTechnicolour(false, beatmapEventData.time, ChromaConfig.TechnicolourLightsStyle));
                                    break;
                                default:
                                    ColourManager.RecolourAllLights(Color.clear, ColourManager.GetTechnicolour(false, beatmapEventData.time, ChromaConfig.TechnicolourLightsStyle));
                                    break;
                            }
                        } else {
                            switch (ChromaConfig.TechnicolourLightsGrouping) {
                                case ColourManager.TechnicolourLightsGrouping.ISOLATED:
                                    //LightsIsolatedTechnicolour.Activate(__instance, ____event, ChromaConfig.TechnicolourLightsStyle, true, beatmapEventData.time);
                                    MayhemEvent.ActivateTechnicolour(beatmapEventData, __instance, ____event);
                                    return false;
                                case ColourManager.TechnicolourLightsGrouping.ISOLATED_GROUP:
                                    __instance.SetLightingColourA(ColourManager.GetTechnicolour(true, beatmapEventData.time, ChromaConfig.TechnicolourLightsStyle));
                                    break;
                                default:
                                    ColourManager.RecolourAllLights(ColourManager.GetTechnicolour(true, beatmapEventData.time, ChromaConfig.TechnicolourLightsStyle), Color.clear);
                                    break;
                            }
                        }
                    }
                }

            } catch (Exception e) {
                ChromaLogger.Log("Exception handling technicolour lights!", ChromaLogger.Level.WARNING);
                ChromaLogger.Log(e);
            }

            try { 

                if (ChromaEvent.SimpleEventActivate(__instance, ref beatmapEventData, ref ____event)) return false;

                if (beatmapEventData.type == ____event) {
                    //CustomLightBehaviour customLight = CustomLightBehaviour.GetCustomLightColour(beatmapEventData);
                    ChromaEvent customEvent = ChromaEvent.GetChromaEvent(beatmapEventData);
                    if (customEvent != null) {
                        if (customEvent.RequiresColourEventsEnabled && !ChromaConfig.CustomColourEventsEnabled) return false;
                        if (customEvent.RequiresSpecialEventsEnabled && !ChromaConfig.CustomSpecialEventsEnabled) return false;
                        customEvent.Activate(ref __instance, ref beatmapEventData, ref ____event);
                        return false;
                    }

                    ChromaJSONEventData chromaEvent = ChromaJSONEventData.GetChromaEvent(beatmapEventData);
                    if (chromaEvent != null) {
                        chromaEvent.Activate(beatmapEventData, __instance, ____event);
                    }
                }

            } catch (Exception e) {
                ChromaLogger.Log("Exception handling lights!", ChromaLogger.Level.WARNING);
                ChromaLogger.Log(e);
            }

            return true;
        }

    }

}
