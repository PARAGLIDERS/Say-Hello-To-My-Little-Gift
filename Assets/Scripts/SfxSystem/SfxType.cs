namespace SfxSystem {
    public enum SfxType {
        // ui
        UiButtonPress = 0,
        UiButtonSelect = 1,

        // shots
        ShotDry = 100,
        ShotPistol = 101,
        ShotUzi = 102,
        ShotShotgun = 103,
        ShotAuto = 104,
        ShotDoubleShotgun = 105,
        ShotRocketLauncher = 106,
        ShotMinigun = 107,

        // enemies
        BloodParticles = 200,
        EnemySpawn = 201,
        EnemySnowmanThrow = 202, 
        EnemyChickenExplode = 203, 

        // vfx
        VfxExplosion = 300,
        VfxGunPickup = 302,
        VfxHealPickup = 303,

        // guns
        GunScroll = 400, 
        
        // player voice
        PlayerVoice_01 = 500,
        PlayerVoice_02 = 501,
        PlayerVoice_03 = 502,
        PlayerVoice_04 = 503,
        PlayerVoice_05 = 504,
        PlayerVoice_06 = 505,
    }
}
