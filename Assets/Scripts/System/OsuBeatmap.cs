﻿using System;
using System.Collections.Generic;
using OsuParsers.Beatmaps;
using OsuParsers.Decoders;
using OsuParsers.Enums.Beatmaps;
using UnityEngine;

public class OsuBeatmap : MonoBehaviour
{
    public static OsuBeatmap instance;

    void Awake()
    {
        instance = this;
    }

    public void Import(string path)
    {
        Beatmap beatmap = BeatmapDecoder.Decode(path);

        var offset = LevelDataContainer.Instance.levelData.timings[0];

        LevelDataContainer.Instance.levelData.timings = new List<Timings>();
        LevelDataContainer.Instance.levelData.notes = new List<Notes>();

        foreach (var point in beatmap.TimingPoints)
        {
            if (point.BeatLength != -100)
            {
                double bpm = 0;
                string signature = "";
                
                if (point.TimeSignature == TimeSignature.SimpleQuadruple)
                {
                    bpm = 60 / point.BeatLength * (4 / 4) * 1000;
                    signature = "4/4";
                }
                else if (point.TimeSignature == TimeSignature.SimpleTriple)
                {
                    bpm = 60 / point.BeatLength * (3 / 4) * 1000;
                    signature = "3/4";
                }
                
                Timings timings = new Timings { beat = signature, bpm = bpm, time = point.Offset};
                LevelDataContainer.Instance.levelData.timings.Add(timings);
            }
        }

        var timingDifferent = LevelDataContainer.Instance.levelData.timings[0].time - offset.time;
        foreach (var evt in LevelDataContainer.Instance.levelData.events)
        {
            evt.time += timingDifferent;
        }
        
        
        for (int i = 0; i < beatmap.HitObjects.Count; i++)
        {
            NoteType type;
            
            if (beatmap.HitObjects[i].Position.X == 85 || beatmap.HitObjects[i].Position.X == 0)
            {
                type = NoteType.Normal;
                
                var note = new Notes
                {
                    customCurveTag = null, duration = 2, ease = "L", time = beatmap.HitObjects[i].StartTime,
                    hitSoundTag = "h1",
                    endTime = null, type = type, noteNum = (uint)(i + 1), splitEase = 1, gameBarTag = new []{"game1"}
                };
                LevelDataContainer.Instance.levelData.notes.Add(note);
            }
            else if (beatmap.HitObjects[i].Position.X == 256)
            {
                type = NoteType.Chain;
                
                var note = new Notes
                {
                    customCurveTag = null, duration = 2, ease = "L", time = beatmap.HitObjects[i].StartTime,
                    hitSoundTag = "h1",
                    endTime = beatmap.HitObjects[i].EndTime, type = type, noteNum = (uint)(i + 1), splitEase = 1, gameBarTag = new []{"game1"}
                };
                LevelDataContainer.Instance.levelData.notes.Add(note);
            }
            else if (beatmap.HitObjects[i].Position.X == 426 || beatmap.HitObjects[i].Position.X == 342)
            {
                type = NoteType.Flick;
                
                var note = new Notes
                {
                    customCurveTag = null, duration = 2, ease = "L", time = beatmap.HitObjects[i].StartTime,
                    hitSoundTag = "h1",
                    endTime = null, type = type, noteNum = (uint)(i + 1), splitEase = 1, gameBarTag = new []{"game1"}
                };
                LevelDataContainer.Instance.levelData.notes.Add(note);
            }

        }
        
        ListMaker.instance.MakeLists();
    }
}