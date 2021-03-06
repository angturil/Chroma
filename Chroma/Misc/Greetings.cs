﻿using Chroma.Settings;
using Chroma.Utils;
using SimpleJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Chroma.Misc {

    internal static class Greetings {

        //Portal quotes, Doki-Doki Literature Club quotes, you know - Skynet type stuff, with the occasional wholesome comment thrown in
        public static string[] GetGreeting(ulong id, string name) {

            int i = UnityEngine.Random.Range(0, 17);

            switch (i) {
                case 0:
                    return new string[] {
                        "HELLO "+id+"- WHOOPS I MEAN "+name.ToUpper()+" HAHA!",
                    };
                case 1:
                    return new string[] {
                        "Greetings, meatba- I MEAN "+name+"!",
                    };
                case 2:
                    return new string[] {
                        "Hi "+name+"!  Good to see you again!  Isn't the weather great today?!",
                    };
                case 3:
                    return new string[] {
                        "gosgh%SYyh6&Ulj;d.sfgn <<< "+name+" <<< hdfh%$66778G;'s/dfg6,74",
                    };
                case 4:
                    return new string[] {
                        "NullReferenceException: object reference not set to an instance of an object",
                        "at Human.GetFriends()",
                        "at Human.FindByName(\""+name+"\").GetFriends().Count()"
                    };
                case 5:
                    return new string[] {
                        "Hey "+name+" did you know, if you subtract "+(id - 10)+" from your ID you get how attractive you are?",
                    };
                case 6:
                    return new string[] {
                        "I really like the sound of rain, "+name+"..."
                    };
                case 7:
                    return new string[] {
                        name+", do you believe in God?"
                    };
                case 8:
                    return new string[] {
                        name+", have you ever wondered what it feels like to die?"
                    };
                case 9:
                    return new string[] {
                        "Hey, are you having a bad day or anything like that?"
                    };
                case 10:
                    return new string[] {
                        name+", do you get good sleep?"
                    };
                case 11:
                    return new string[] {
                        "I was thinking about >>> G%$&ik;,'kjgh] >>> earlier..."
                    };
                case 12:
                    return new string[] {
                        "Stay with me forever, "+name
                    };
                case 13:
                    return new string[] {
                        "Hello "+name+", welcome to the Chroma computer-aided enrichment center."
                    };
                case 14:
                    return new string[] {
                        "You look great, by the way, "+name+". Very healthy."
                    };
                case 15:
                    return new string[] {
                        "This next test involves blocks.",
                        "You remember those, right, "+name+"?",
                    };
                case 16:
                    return new string[] {
                        "I honestly, truly didn’t think you’d fall for this..."
                    };
                default:
                    return new string[] {
                        "gosgh%SYyh6&Ulj;d.sfgn <<< "+name+" <<< hdfh%$66778G;'s/dfg6,74",
                    };
            }

        }

        private static void AcquiredOnlineVersionInfo(bool upToDate, string webVersion, string errorMessage) {
            string replace;
            if (errorMessage != null) {
                replace = "<color=red>" + errorMessage + "</color>";
            } else {
                if (upToDate) replace = "<color=green>You have the most recent version!</color>";
                else replace = "<color=red>Chroma is outdated!</color>";
                if (webVersion != null) replace = replace + Environment.NewLine + "Latest public release: " + webVersion;
            }

            SidePanelUtil.RegisterTextPanel("chroma",
                ResourceTextFiles.chromaNotes
                .Replace("%VER%", (upToDate ? "<color=green>" : "<color=red>") + ChromaPlugin.Instance.plugin.Version + "</color>")
                .Replace("%USERNAME%", ChromaConfig.Username)
                .Replace("%ONLINE_VER%", replace)
                );
            SidePanelUtil.Update();

            ChromaLogger.Log("Updated panel with version info");
        }
        
        public static void RegisterChromaSideMenu() {
            SidePanelUtil.RegisterTextPanel("chroma", 
                ResourceTextFiles.chromaNotes
                .Replace("%VER%", ChromaPlugin.Instance.plugin.Version)
                .Replace("%USERNAME%", ChromaConfig.Username)
                .Replace("%ONLINE_VER%", "<color=yellow>ACQUIRING VERSION INFO</color>")
                );

            SidePanelUtil.RegisterTextPanel("chromaWaiver", ResourceTextFiles.safetyWaiver);
            SidePanelUtil.RegisterTextPanel("chromaCredits", ResourceTextFiles.credits);

            VersionChecker.GetOnlineVersion("https://modsaber.org/api/v1.1/mods/versions/chroma", AcquiredOnlineVersionInfo);
        }

    }

}
