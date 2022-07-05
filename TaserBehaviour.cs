using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace KoteMod
{
    public class TaserBehaviour : MonoBehaviour
    {
      private bool shoots = false;
      private bool stunning = false;
      private int step = 0;

      private TaserWireBehaviour Wire;
      private LimbBehaviour[] Limbs;
      private LimbBehaviour Limb;
      private AudioSource AS;
      private List<AudioClip> Clips = new List<AudioClip>();
      private List<GameObject> Electrodes = new List<GameObject>();
      private Sprite ElectrodeSprite;

      Vector2 barrelPosition = new Vector2(0.2f, 0.07f);

      private void Awake()
      {
        AS = gameObject.GetOrAddComponent<AudioSource>();
        Clips = new List<AudioClip>();
        Clips.Add(ModAPI.LoadSound("sfx/shot.wav"));
        Clips.Add(ModAPI.LoadSound("sfx/stun.wav"));
        ElectrodeSprite = ModAPI.LoadSprite("ElectrodeSprite.png");
      }

      private void CreateElectrode(Vector3 pos, Transform parent)
      {
        GameObject electrode = new GameObject();
        electrode.transform.position += new Vector3(pos.x + UnityEngine.Random.Range(-0.05f, 0.05f), pos.y + UnityEngine.Random.Range(-0.05f, 0.05f), pos.z);
        electrode.AddComponent<SpriteRenderer>().sprite = ElectrodeSprite;
        electrode.transform.eulerAngles = new Vector3(0, 0, UnityEngine.Random.Range(-45, 45));
        electrode.transform.SetParent(parent);
        electrode.transform.localScale = new Vector3(0.4f, 0.4f);

        Electrodes.Add(electrode);
      }

      private void Start()
      {
        TaserReset();
      }

      private void PlaySound(int index)
      {
        AS.clip = Clips[index];
        AS.Play();
      }

      void TaserReset()
      {
        if (GetComponent<TaserWireBehaviour>())
        {
          GameObject.Destroy(GetComponent<TaserWireBehaviour>());
        }
        if (GetComponent<DistanceJoint2D>())
        {
          GameObject.Destroy(GetComponent<DistanceJoint2D>());
        }
        shoots = false;
        stunning = false;
        step = 0;
        Wire = null;
        Limbs = null;
        if (Electrodes.Count > 0)
        {
          GameObject.Destroy(Electrodes[0]);
          GameObject.Destroy(Electrodes[1]);
          Electrodes.Clear();
        }
      }
      void Use()
      {
        if (stunning) return;
        step++;
        switch (step)
        {
          case 1:
            Shoot();
            break;
          case 2:
            if (Limb != null)
            {
              StartCoroutine(Stun());
            }
            else
            {
              TaserReset();
            }
            break;
          case 3:
            step = 0;
            break;
        }
      }

      void Shoot()
      {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, 3f * transform.localScale.x, LayerMask.GetMask("Objects"));
        if (ray.collider == null) return;
        if (ray.transform.GetComponent<LimbBehaviour>())
        {
          Limbs = ray.transform.GetComponent<LimbBehaviour>().Person.Limbs;
          Limb = ray.transform.GetComponent<LimbBehaviour>();
          PlaySound(0);
          shoots = true;
          Attach(ray);
        }
      }

      void Attach(RaycastHit2D hit)
      {
        DistanceJoint2D joint = gameObject.AddComponent<DistanceJoint2D>();
        joint.maxDistanceOnly = true;
        joint.enableCollision = false;
        joint.autoConfigureDistance = false;
        joint.distance = 3f;
        joint.breakForce = Mathf.Infinity;
        joint.breakTorque = Mathf.Infinity;
        joint.anchor = barrelPosition;
        joint.connectedBody = hit.rigidbody;
        joint.connectedAnchor = hit.transform.InverseTransformPoint(hit.point);

        TaserWireBehaviour wireBehaviour = gameObject.AddComponent<TaserWireBehaviour>();
        wireBehaviour.WireColor = Color.white;
        wireBehaviour.WireMaterial = Resources.Load<Material>("Materials/Wire");
        wireBehaviour.WireWidth = 0.002f;
        wireBehaviour.typedJoint = joint;
        Wire = wireBehaviour;

        int n = 0;
        while (n < 2)
        {
            CreateElectrode(hit.point, hit.transform);
            n++;
        }
      }

      private IEnumerator Stun()
      {
        stunning = true;
        for (int i = 0; i < 30; i++)
        {
          if (shoots)
          {
            Shock();
            yield return new WaitForSeconds(0.07f);
          }
          else
          {
            StopCoroutine(Stun());
          }
        }
        TaserReset();
      }

      private void Shock()
      {
        if (Limb != null)
        {
          if (Wire)
          {
            for (int i = 0; i < Limbs.Length; i++)
            {
              if (Limbs[i] != null)
              {
                Limbs[i].PhysicalBehaviour.charge = 0.5f;
                Limbs[i].PhysicalBehaviour.rigidbody.AddForce(Vector3.down * 0.3f);
                if (Limbs[i].GripBehaviour)
                {
                  Limbs[i].GripBehaviour.DropObject();
                }
                if (i > 0)
                {
                  Vector3 direction;
                  direction.x = UnityEngine.Random.Range(-360, 360);
                  direction.y = UnityEngine.Random.Range(-360, 360);
                  direction.z = UnityEngine.Random.Range(-360, 360);
                  Limbs[i].GetComponent<Rigidbody2D>().AddForce(direction * 0.04f);
                }
              }
              Limb.Person.ShockLevel += UnityEngine.Random.Range(0.0002f, 0.002f);
              Limb.Person.Consciousness -= UnityEngine.Random.Range(0.0002f, 0.001f);
            }
            PlaySound(1);
          }
        }
        else
        {
          TaserReset();
        }
      }
    }

    public class TaserWireBehaviour : DistanceJointWireBehaviour
    {
    }
  }
