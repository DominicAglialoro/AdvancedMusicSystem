using System;
using FMOD.Studio;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.AdvancedMusicSystem;

public class AdvancedMusicSystemController : Entity {
    private const int ADJUST_TOLERANCE = 50;
    private const int JUMP_TOLERANCE = 100;
    private const float MIN_PITCH_ADJUST = 0.02f;
    private const float MAX_PITCH_ADJUST = 0.1f;

    private double time;
    private string eventPath;
    private EventInstance eventInstance;

    public override void Update() {
        base.Update();

        time += Engine.DeltaTime;

        if (eventInstance == null)
            return;

        int targetPosition = (int) (1000d * time) - AdvancedMusicSystemModule.Settings.AudioOffset;

        eventInstance.getTimelinePosition(out int position);

        int error = position - targetPosition;
        int absError = Math.Abs(error);
        float pitch = Engine.TimeRate * Engine.TimeRateB;

        if (absError > JUMP_TOLERANCE)
            eventInstance.setTimelinePosition(targetPosition);
        else if (absError >= ADJUST_TOLERANCE)
            pitch -= Math.Sign(error) * MathHelper.Lerp(MIN_PITCH_ADJUST, MAX_PITCH_ADJUST, (float) (absError - ADJUST_TOLERANCE) / (JUMP_TOLERANCE - ADJUST_TOLERANCE));

        eventInstance.setPitch(pitch);
    }

    public override void Removed(Scene scene) => Stop();

    public override void SceneEnd(Scene scene) => Stop();

    public EventInstance Play(string path, double time) {
        this.time = time;

        if (path != eventPath) {
            Audio.Stop(eventInstance, false);
            eventInstance = Audio.CreateInstance(path);
        }

        eventPath = path;
        eventInstance.setTimelinePosition((int) (1000d * time) - AdvancedMusicSystemModule.Settings.AudioOffset);
        eventInstance.setPitch(Engine.TimeRate * Engine.TimeRateB);
        eventInstance.start();

        return eventInstance;
    }

    public void Stop() {
        Audio.Stop(eventInstance, false);
        eventInstance = null;
    }

    public void Resume() {
        if (eventInstance == null)
            return;

        eventInstance.setTimelinePosition((int) (1000d * time) - AdvancedMusicSystemModule.Settings.AudioOffset);
        eventInstance.setPitch(Engine.TimeRate * Engine.TimeRateB);
        Audio.Resume(eventInstance);
    }

    public void Pause() => Audio.Pause(eventInstance);
}