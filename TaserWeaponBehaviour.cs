using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

namespace Mod
{
    public class TaserWeaponBehaviour : MonoBehaviour
    {
        private bool shoot = false;
        private bool running = false;
        private LimbBehaviour[] limbs;
        private AudioSource AS;
		    private Transform line;
        private GameObject electrode;

        private void Awake()
        {
            AS = gameObject.GetOrAddComponent<AudioSource>();
            Destroy();
        }

        private void Destroy()
        {
            if (GetComponent<TaserWireBehaviour>())
            {
                UnityEngine.Object.Destroy(GetComponent<TaserWireBehaviour>());
            }
            if (GetComponent<DistanceJoint2D>())
            {
                UnityEngine.Object.Destroy(GetComponent<DistanceJoint2D>());
            }
            if (electrode)
            {
                GameObject.Destroy(electrode);
            }
            shoot = false;
            running = false;
            GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("TaserSprite0.png");
        }

        private void Use()
        {
            if (!shoot)
            {
                TaserShot();
            }
            else
            {
                if (running) return;

                running = true;

                if (electrode)
                {
                    if (electrode.GetComponent<TaserElectrodeBehaviour>().connected)
                    {
                        if (electrode.GetComponent<FixedJoint2D>())
                        {
                            StartCoroutine(TaserFunction());
                        }
                        else
                        {
                            Destroy();
                        }
                    }
                }
                else
                {
                  Destroy();
                }
            }
        }

        private void PlaySound(int index)
        {
            AudioClip clip = ModAPI.LoadSound("sound" + index + ".wav");
            AS.clip = clip;
            AS.Play();
        }

        private void TaserShot()
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, 3f * transform.localScale.x, LayerMask.GetMask("Objects"));
            if (ray.transform.GetComponent<LimbBehaviour>())
            {
                limbs = ray.transform.GetComponent<LimbBehaviour>().Person.Limbs;
            }
            else
            {
                return;
            }

            PlaySound(0);
            shoot = true;

            GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("TaserSprite1.png");
            ModAPI.CreateParticleEffect("Vapor", transform.TransformPoint(new Vector2(0.2f, 0.07f)));

            CreateElectrode();

            DistanceJoint2D joint = gameObject.AddComponent<DistanceJoint2D>();
            joint.maxDistanceOnly = true;
            joint.enableCollision = false;
            joint.autoConfigureDistance = false;
            joint.distance = 3f;
            joint.breakForce = Mathf.Infinity;
            joint.breakTorque = Mathf.Infinity;
            joint.anchor = new Vector2(0.2f, 0.07f);
            joint.connectedBody = electrode.GetComponent<Rigidbody2D>();
            joint.connectedAnchor = Vector2.zero;

            TaserWireBehaviour wireBehaviour = gameObject.AddComponent<TaserWireBehaviour>();
            wireBehaviour.WireColor = Color.white;
            wireBehaviour.WireMaterial = Resources.Load<Material>("Materials/Wire");
            wireBehaviour.WireWidth = 0.001f;
            wireBehaviour.typedJoint = joint;
        }

        private void CreateElectrode()
        {
            electrode = ModAPI.CreatePhysicalObject("Electrode", ModAPI.LoadSprite("electrodeSprite.png"));
            electrode.transform.position = transform.TransformPoint(new Vector2(0.2f, 0.07f));
            electrode.transform.rotation = transform.rotation;
            electrode.FixColliders();
            electrode.GetComponent<PhysicalBehaviour>().TrueInitialMass = 0.3f;
            electrode.GetComponent<PhysicalBehaviour>().InitialMass = 0.3f;
            electrode.GetComponent<PhysicalBehaviour>().rigidbody.mass = 0.3f;
            electrode.GetComponent<PhysicalBehaviour>().Properties = ModAPI.FindPhysicalProperties("Plastic");
            electrode.GetComponent<Rigidbody2D>().AddForce((transform.right * transform.localScale.x) * 3f, ForceMode2D.Impulse);
            electrode.GetComponent<PhysicalBehaviour>().SpawnSpawnParticles = false;
            electrode.AddComponent<TaserElectrodeBehaviour>();
            electrode.transform.localScale = new Vector3(0.5f, 0.5f);
        }

        private void limbCharge()
        {
            if(!electrode)
            {
                Destroy();
                return;
            }
            if (!limbs[0])
            {
                Destroy();
                return;
            }
            if (GetComponent<TaserWireBehaviour>())
            {
                if (electrode.GetComponent<TaserElectrodeBehaviour>().connected)
                {
                    for (int i = 0; i < limbs.Length; i++)
                    {
                        limbs[i].PhysicalBehaviour.charge = 0.5f;
                        limbs[i].PhysicalBehaviour.rigidbody.AddForce(Vector3.down * 0.3f);
                        limbs[0].Person.ShockLevel += UnityEngine.Random.Range(0.0002f, 0.002f);
                        limbs[0].Person.Consciousness -= UnityEngine.Random.Range(0.0002f, 0.001f);
                        if (limbs[i].GripBehaviour)
                        {
                            limbs[i].GripBehaviour.DropObject();
                        }
                    }
                    for (int i = 1; i < limbs.Length; i++)
                    {
                        Vector3 direction;
                        direction.x = UnityEngine.Random.Range(-360, 360);
                        direction.y = UnityEngine.Random.Range(-360, 360);
                        direction.z = UnityEngine.Random.Range(-360, 360);

                        limbs[i].GetComponent<PhysicalBehaviour>().rigidbody.AddForce(direction * 0.04f);
                    }
                    PlaySound(1);
                }
            }
            else
            {
                Destroy();
            }
        }
        private IEnumerator TaserFunction()
        {
            for (int i = 0; i < 30; i++)
            {
                if (shoot)
                {
                    limbCharge();
                    yield return new WaitForSeconds(0.07f);
                }
                else
                {
                    StopCoroutine(TaserFunction());
                }
            }
            Destroy();
        }
    }

    public class TaserElectrodeBehaviour : MonoBehaviour
    {
        public bool connected = false;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!connected && collision.gameObject.GetComponent<LimbBehaviour>())
            {
                connected = true;
                FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
                joint.connectedBody = collision.gameObject.GetComponent<PhysicalBehaviour>().rigidbody;
            }
        }
    }

    public class TaserWireBehaviour : DistanceJointWireBehaviour
    {

    }
}
