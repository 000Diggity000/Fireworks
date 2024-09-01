using BepInEx;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using BoplFixedMath;
using JetBrains.Annotations;
using BepInEx.Logging;

namespace Fireworks
{
    [BepInPlugin("com.000diggity000.Fireworks", "Fireworks", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            
            Harmony harmony = new Harmony("com.000diggity000.Fireworks");
            MethodInfo original1 = AccessTools.Method(typeof(Missile), "Update");
            MethodInfo patch1 = AccessTools.Method(typeof(Patches), "Update_Missile_Plug");
            harmony.Patch(original1, new HarmonyMethod(patch1));
        }
        public class Patches
        {
            public static bool Update_Missile_Plug(Missile __instance, FixTransform ___fixTrans, BoplBody ___body, float ___spawnTime, Fix ___accel)
            {
                FixTransform.InstantiateFixed<Explosion>(__instance.onHitExplosionPrefab, new Vec2(___body.position.x, ___body.position.y + (Fix)15)).GetComponent<IPhysicsCollider>().Scale = ___fixTrans.Scale;
                __instance.gameObject.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                __instance.gameObject.gameObject.GetComponent<IPhysicsCollider>().enabled = false;

                
                return true;
            }
        }
    }
}
