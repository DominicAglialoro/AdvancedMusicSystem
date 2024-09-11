using System;

namespace Celeste.Mod.AdvancedMusicSystem;

public class AdvancedMusicSystemModule : EverestModule {
    public static AdvancedMusicSystemModule Instance { get; private set; }

    public static AdvancedMusicSystemSession Session => (AdvancedMusicSystemSession) Instance._Session;

    public static AdvancedMusicSystemSettings Settings => (AdvancedMusicSystemSettings) Instance._Settings;

    public override Type SessionType => typeof(AdvancedMusicSystemSession);

    public override Type SettingsType => typeof(AdvancedMusicSystemSettings);

    public AdvancedMusicSystemModule() {
        Instance = this;
#if DEBUG
        Logger.SetLogLevel(nameof(AdvancedMusicSystemModule), LogLevel.Verbose);
#else
        Logger.SetLogLevel(nameof(AdvancedMusicSystemModule), LogLevel.Info);
#endif
    }

    public override void Load() { }

    public override void Unload() { }
}