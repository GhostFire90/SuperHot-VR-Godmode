using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony;
using MelonLoader;
using UnityEngine;
[assembly : MelonInfo(typeof(SHVRGodMode.SHVRGodmode), "Superhot VR Godmode", "1.0", "Ghostfire04")]
[assembly : MelonGame("SUPERHOT_Team", "SUPERHOT_VR")]
namespace SHVRGodMode
{
    class SHVRGodmode : MelonMod
    {
        static bool im;
        static GameObject player;
        public override void OnLevelWasLoaded(int level)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.F3))
            {
                DebugSettings.Instance.devMenu = true;
                VrDevMenu.Init();
            }
        }
        [HarmonyPatch(typeof(VrDevMenu),"Awake", MethodType.Normal)]
        private static class VrDevMenu_Patch
        {
            private static void Prefix(VrDevMenu __instance)
            {
                DebugSettings.Instance.devMenu = true;
            }
            private static void Postfix(VrDevMenu __instance)
            {
                VrDevMenu.RemoveAction("godmode");
                VrDevMenu.AddAction("Ghostfire042's Godmode : " + im, delegate
                {
                    im = !im;
                    VrDevMenu.RelabelAction("firegodmode", "Ghostfire042's Godmode : " + im);
                }, false, "firegodmode");
            }
        }
        [HarmonyPatch(typeof(PlayerActionsVR), "OnBulletHit", MethodType.Normal)]
        private static class AntiBullet
        {
            private static bool Prefix()
            {
                if (im)
                {
                    return false;
                } 
                else
                {
                    return true;
                }
            }
        }
        [HarmonyPatch(typeof(PlayerActionsVR), "Kill", MethodType.Normal)]
        private static class AntiMelee
        {
            private static bool Prefix()
            {
                if (im)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
