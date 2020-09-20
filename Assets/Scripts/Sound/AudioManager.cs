using System;
using System.Collections;
using System.Collections.Generic;
using CastleFight.Core;
using UnityEngine;

namespace CastleFight
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private Sound[] sounds;


        public void Awake()
        {
            ManagerHolder.I.AddManager(this);

            foreach(Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
            }
        }

        public void Play(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            try
            {
                s.source.Play();
            }
            catch (NullReferenceException e)
            {
                Debug.LogError("Invalid song: " + name);
            }
            Debug.Log("Playing: " + name);

        }

        public void Stop(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.source.Stop();
        }

        public float GetLength(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            return s.clip.length;
        }

    }
}