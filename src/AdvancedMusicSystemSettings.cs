namespace Celeste.Mod.AdvancedMusicSystem;

public class AdvancedMusicSystemSettings : EverestModuleSettings {
    [SettingRange(-250, 250, true)]
    public int AudioOffset { get; set; }
}