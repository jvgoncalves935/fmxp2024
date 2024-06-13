using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueData
{
    private string id;
    private int camera;
    private string character;
    private string text;
    private float duration;

    public string Id {
        get {
            return id;
        }
        set {
            id = value;
        }
    }

    public int Camera {
        get {
            return camera;
        }
        set {
            camera = value;
        }
    }

    public string Character {
        get {
            return character;
        }
        set {
            character = value;
        }
    }

    public string Text {
        get {
            return text;
        }
        set {
            text = value;
        }
    }

    public float Duration {
        get {
            return duration;
        }
        set {
            duration = value;
        }
    }
    public DialogueData(string _id, int _camera, string _character, string _text, float _duration) {
        id = _id;
        camera = _camera;
        character = _character;
        text = _text;
        duration = _duration;
    }
}
