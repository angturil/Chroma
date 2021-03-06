﻿using Chroma.Beatmap.Events;
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
    [HarmonyPatch("OnDestroy")]
    class LightSwitchEventEffectOnDestroy {

        static void Postfix(LightSwitchEventEffect __instance, ref BeatmapEventType ____event) {
            LightSwitchEventEffectExtensions.LSEDestroy(__instance, ____event);
        }

    }

}
