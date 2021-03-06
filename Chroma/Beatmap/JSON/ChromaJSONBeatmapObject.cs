﻿using SimpleJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chroma.Beatmap.JSON {

    public abstract class ChromaJSONBeatmapObject {

        public float time;
        public BeatmapEventType type;

        public T ParseJSON<T>(JSONNode eventNode, ref float beatsPerMinute, ref float shuffle, ref float shufflePeriod) where T : ChromaJSONBeatmapObject {

            JSONNode.Enumerator nodeEnum = eventNode.GetEnumerator();
            while (nodeEnum.MoveNext()) {
                string key = nodeEnum.Current.Key;
                JSONNode node = nodeEnum.Current.Value;

                switch (key) {
                    case "_time":
                        time = GetRealTimeFromBPMTime(node.AsFloat, ref beatsPerMinute, ref shuffle, ref shufflePeriod);
                        break;
                    case "_type":
                        type = (BeatmapEventType) node.AsInt;
                        break;
                    default:
                        ParseNode(key, node);
                        break;
                }
            }

            return this as T;
        }

        public abstract void ParseNode(string key, JSONNode node);

        private static float GetRealTimeFromBPMTime(float bmpTime, ref float beatsPerMinute, ref float shuffle, ref float shufflePeriod) {
            float num = bmpTime;
            if (shufflePeriod > 0f) {
                bool flag = (int)(num * (1f / shufflePeriod)) % 2 == 1;
                if (flag) {
                    num += shuffle * shufflePeriod;
                }
            }
            if (beatsPerMinute > 0f) {
                num = num / beatsPerMinute * 60f;
            }
            return num;
        }

    }

}
