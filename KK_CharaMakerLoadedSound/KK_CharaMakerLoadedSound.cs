﻿using BepInEx;
using Harmony;
using Illusion.Game;
/// <summary>
/// When Chara Maker starts, wait a bit for lag to stop then play a sound
/// </summary>
namespace KK_CharaMakerLoadedSound
{
    [BepInPlugin("com.deathweasel.bepinex.charamakerloadedsound", "Chara Maker Load Sound", Version)]
    public class KK_CharaMakerLoadedSound : BaseUnityPlugin
    {
        public const string Version = "1.0";
        private static int Counter = 0;

        void Main()
        {
            var harmony = HarmonyInstance.Create("com.deathweasel.bepinex.charamakerloadedsound");
            harmony.PatchAll(typeof(KK_CharaMakerLoadedSound));
        }

        private void Update()
        {
            //Wait 10 ticks for the game to stop lagging then play a sound
            if (Counter == 10)
            {
                Utils.Sound.Play(SystemSE.result_single);
                Counter = 0;
            }
            else if (Counter > 0) //Increment the counter if it has been started
            {
                Counter++;
            }
        }
        /// <summary>
        /// When Chara Maker starts, start a counter
        /// </summary>
        [HarmonyPostfix]
        [HarmonyPatch(typeof(CustomScene), "Start")]
        public static void StartPostfix()
        {
            Counter = 1;
        }
    }
}
