using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace KoteMod
{
    public class KoteMod
    {
        public static void Main()
        {
            CategoryBuilder.Create("KoteM", "KoteMod Category", ModAPI.LoadSprite("CategoryThumb.png"));

            //Human 
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Human"),
                    NameOverride = "Kto-To",
                    NameToOrderByOverride = "Human18",
                    DescriptionOverride = "Love bebra",
                    CategoryOverride = ModAPI.FindCategory("KoteM"),
                    ThumbnailOverride = ModAPI.LoadSprite("FMT.png"),
                    AfterSpawn = (Instance) =>
                    {
                        var skin = ModAPI.LoadTexture("skinlayerPV1.png");
                        var flesh = ModAPI.LoadTexture("flesh.png");
                        var bone = ModAPI.LoadTexture("bone.png");
                        Instance.GetComponent<PersonBehaviour>().SetBodyTextures(skin, flesh, bone);


                        var person = Instance.GetComponent<PersonBehaviour>();


                        person.SetBodyTextures(skin, flesh, bone, 1);


                        person.SetBruiseColor(126, 64, 113);
                        person.SetSecondBruiseColor(131, 34, 50);
                        person.SetThirdBruiseColor(48, 49, 135);
                        person.SetBloodColour(108, 0, 4);
                        person.SetRottenColour(147, 255, 251);
                        foreach (var body in person.Limbs)
                        {
                            body.BaseStrength *= 1.5f;
                            body.Health *= 1.5f;
                            body.BreakingThreshold *= 1.5f;
                        }
                    }
                }
            );
            //Human 
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Human"),
                    NameOverride = "ATT4NXBAX",
                    NameToOrderByOverride = "Human19",
                    DescriptionOverride = "I love Mishka cat",
                    CategoryOverride = ModAPI.FindCategory("KoteM"),
                    ThumbnailOverride = ModAPI.LoadSprite("TQM.png"),
                    AfterSpawn = (Instance) =>
                    {
                        var skin = ModAPI.LoadTexture("skinlayerQM.png");
                        var flesh = ModAPI.LoadTexture("flesh.png");
                        var bone = ModAPI.LoadTexture("bone.png");
                        Instance.GetComponent<PersonBehaviour>().SetBodyTextures(skin, flesh, bone);


                        var person = Instance.GetComponent<PersonBehaviour>();


                        person.SetBodyTextures(skin, flesh, bone, 1);


                        person.SetBruiseColor(126, 64, 113);
                        person.SetSecondBruiseColor(131, 34, 50);
                        person.SetThirdBruiseColor(48, 49, 135);
                        person.SetBloodColour(108, 0, 4);
                        person.SetRottenColour(147, 255, 251);
                        foreach (var body in person.Limbs)
                        {
                            body.BaseStrength *= 1.5f;
                            body.Health *= 1.5f;
                            body.BreakingThreshold *= 1.5f;
                        }
                    }
                }
             );
            
           
           
            //Human 
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Human"),
                    NameOverride = "Kot",
                    NameToOrderByOverride = "Human20",
                    DescriptionOverride = "Naruto throws a flurry of punches at his enemies.",
                    CategoryOverride = ModAPI.FindCategory("KoteM"),
                    ThumbnailOverride = ModAPI.LoadSprite("W.png"),
                    AfterSpawn = (Instance) =>
                    {
                        var skin = ModAPI.LoadTexture("skinlayerW.png");
                        var flesh = ModAPI.LoadTexture("flesh.png");
                        var bone = ModAPI.LoadTexture("bone.png");
                        Instance.GetComponent<PersonBehaviour>().SetBodyTextures(skin, flesh, bone);


                        var person = Instance.GetComponent<PersonBehaviour>();


                        person.SetBodyTextures(skin, flesh, bone, 1);


                        person.SetBruiseColor(126, 64, 113);
                        person.SetSecondBruiseColor(131, 34, 50);
                        person.SetThirdBruiseColor(48, 49, 135);
                        person.SetBloodColour(108, 0, 4);
                        person.SetRottenColour(147, 255, 251);
                        foreach (var body in person.Limbs)
                        {
                            body.BaseStrength *= 1.5f;
                            body.Health *= 1.5f;
                            body.BreakingThreshold *= 1.5f;
                        }
                    }
                }
            );
            //taser
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Rod"),
                    NameOverride = "Shocker",
                    DescriptionOverride = "trahnet tokom!",
                    NameToOrderByOverride = "!Taser",
                    CategoryOverride = ModAPI.FindCategory("KoteM"),
                    ThumbnailOverride = ModAPI.LoadSprite("TaserView.png"),
                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("TaserSprite.png", 1f);
                        Instance.FixColliders();

                        Instance.GetOrAddComponent<TaserBehaviour>();
                        var phys = Instance.GetComponent<PhysicalBehaviour>();
                        phys.Properties = ModAPI.FindPhysicalProperties("Weapon");
                        phys.TrueInitialMass = 0.3f;
                        phys.InitialMass = 0.3f;
                        phys.rigidbody.mass = 0.3f;
                        phys.HoldingPositions = new[]
                        {
                            new Vector3(-0.1f, -0.05f)
                        };
                    }
                });
            //pistol
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("9mm Pistol"),
                    NameOverride = "KotePistol",
                    DescriptionOverride = "I KILL U!",
                    CategoryOverride = ModAPI.FindCategory("KoteM"),
                    ThumbnailOverride = ModAPI.LoadSprite("makarovthmb.png"),
                    AfterSpawn = (Instance) =>
                    {
                        ModAPI.KeepExtraObjects();

                        Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("PMCol.png", 10);
                        foreach (var c in Instance.GetComponents<Collider2D>())
                        {
                            GameObject.Destroy(c);
                        }
                        Instance.FixColliders();

                        Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("makarov.png");

                        var Slide = Instance.transform.Find("Slide");
                        Slide.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("makarovslide.png");

                        var firearm = Instance.GetComponent<FirearmBehaviour>();

                        Cartridge customCartridge = ModAPI.FindCartridge("9mm");
                        customCartridge.name = "9x18mm Makarov";
                        customCartridge.Damage *= 1.2f;
                        customCartridge.StartSpeed *= 2f;
                        customCartridge.Recoil *= 0.5f;
                        customCartridge.PenetrationRandomAngleMultiplier *= 0.25f;
                        customCartridge.ImpactForce *= 0.8f;

                        firearm.Cartridge = customCartridge;

                        firearm.ShotSounds = new AudioClip[]
                        {
                ModAPI.LoadSound("pm_1.wav"),
                ModAPI.LoadSound("pm_2.wav"),
                ModAPI.LoadSound("pm_3.wav")
                        };
                    }
                }
);
            //knife
            ModAPI.Register(
                    new Modification()
                    {
                        OriginalItem = ModAPI.FindSpawnable("Sword"),
                        NameOverride = "BigKnife",
                        DescriptionOverride = "Knife's fingers in your ass ;)",
                        CategoryOverride = ModAPI.FindCategory("KoteM"),
                        ThumbnailOverride = ModAPI.LoadSprite("machetethmb.png"),
                        AfterSpawn = (Instance) =>
                        {
                            Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("machete.png");
                            Instance.FixColliders();
                        }
                    }
);
            //Human 
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Human"),
                    NameOverride = "TTuTTcNk",
                    NameToOrderByOverride = "Human20",
                    DescriptionOverride = "NDN cyda moi cladenkiy)",
                    CategoryOverride = ModAPI.FindCategory("KoteM"),
                    ThumbnailOverride = ModAPI.LoadSprite("my/pupsikava.png"),
                    AfterSpawn = (Instance) =>
                    {
                        var skin = ModAPI.LoadTexture("my/pupsik.png");
                        var flesh = ModAPI.LoadTexture("flesh.png");
                        var bone = ModAPI.LoadTexture("my/pupsik.png");
                        Instance.GetComponent<PersonBehaviour>().SetBodyTextures(skin, flesh, bone);


                        var person = Instance.GetComponent<PersonBehaviour>();


                        person.SetBodyTextures(skin, flesh, bone, 1);


                        person.SetBruiseColor(126, 64, 113);
                        person.SetSecondBruiseColor(131, 34, 50);
                        person.SetThirdBruiseColor(48, 49, 135);
                        person.SetBloodColour(108, 0, 4);
                        person.SetRottenColour(147, 255, 251);
                        foreach (var body in person.Limbs)
                        {
                            body.BaseStrength *= 1.5f;
                            body.Health *= 20000f;
                            body.BreakingThreshold *= 1.5f;
                        }
                    }
                }
            );


            //M4
            ModAPI.Register(
    new Modification()
    {
        OriginalItem = ModAPI.FindSpawnable("Rifle"),
        NameOverride = "M4",
        DescriptionOverride = "EMKA",
        CategoryOverride = ModAPI.FindCategory("KoteM"),
        ThumbnailOverride = ModAPI.LoadSprite("video/m14thmd.png"),
        AfterSpawn = (Instance) =>
        {
            ModAPI.KeepExtraObjects();

            Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("video/M14Col.png", 10);
            foreach (var c in Instance.GetComponents<Collider2D>())
            {
                GameObject.Destroy(c);
            }
            Instance.FixColliders();

            Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("video/m14.png");

            var Slide = Instance.transform.Find("Slide");
            Slide.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("video/m14slide.png");

            var firearm = Instance.GetComponent<FirearmBehaviour>();

            Cartridge customCartridge = ModAPI.FindCartridge("7.62x51mm");
            customCartridge.name = "7,62 × 51 mm NATO";
            customCartridge.Damage *= 1.5f;
            customCartridge.StartSpeed *= 1.2f;
            customCartridge.Recoil *= 0.2f;
            customCartridge.PenetrationRandomAngleMultiplier *= 0f;
            customCartridge.ImpactForce *= 5f;

            firearm.Cartridge = customCartridge;

            firearm.ShotSounds = new AudioClip[]
            {
                ModAPI.LoadSound("video/m14_1.wav"),
                ModAPI.LoadSound("video/m14_2.wav"),
                ModAPI.LoadSound("video/m14_3.wav"),
                ModAPI.LoadSound("video/m14_4.wav")
            };

            Instance.GetComponent<PhysicalBehaviour>().ContextMenuOptions.Buttons.Add(new ContextMenuButton("automatic", "Full-Auto", "Fully-Automatic Mode", new UnityAction[1]
            {
            (UnityAction) (() =>
            {
            var firearmauto = Instance.GetComponent<FirearmBehaviour>();

            Cartridge customCartridgeauto = ModAPI.FindCartridge("7.62x51mm");
            customCartridgeauto.name = "7,62 × 51 mm NATO";
            customCartridgeauto.Damage *= 1.5f;
            customCartridgeauto.StartSpeed *= 1.2f;
            customCartridgeauto.Recoil *= 0.4f;
            customCartridgeauto.PenetrationRandomAngleMultiplier *= 0.5f;
            customCartridgeauto.ImpactForce *= 5f;


            firearmauto.Cartridge = customCartridgeauto;
            firearmauto.InitialInaccuracy = 0.08f;

            Instance.GetComponent<FirearmBehaviour>().AutomaticFireInterval = 0.11f;
            Instance.GetComponent<FirearmBehaviour>().Automatic = true;


        })
        }));

            Instance.GetComponent<PhysicalBehaviour>().ContextMenuOptions.Buttons.Add(new ContextMenuButton("semi-automatic", "Semi-Auto", "Semi-Automatic Mode", new UnityAction[1]
            {
            (UnityAction) (() =>
            {
            var firearmsemi = Instance.GetComponent<FirearmBehaviour>();

            Cartridge customCartridgesemi = ModAPI.FindCartridge("7.62x51mm");
            customCartridgesemi.name = "7,62 × 51 mm NATO";
            customCartridgesemi.Damage *= 1.5f;
            customCartridgesemi.StartSpeed *= 1.2f;
            customCartridgesemi.Recoil *= 0.2f;
            customCartridgesemi.PenetrationRandomAngleMultiplier *= 0f;
            customCartridgesemi.ImpactForce *= 5f;

            firearmsemi.Cartridge = customCartridgesemi;
            firearmsemi.InitialInaccuracy = 0f;

            Instance.GetComponent<FirearmBehaviour>().Automatic = false;
        })
        }));
        }

    }
);



        }
    }
}


