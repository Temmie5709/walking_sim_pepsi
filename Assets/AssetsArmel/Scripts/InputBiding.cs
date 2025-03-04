using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBiding : MonoBehaviour
{
    [SerializeField] private InputInfos[] baseInputs;
    private Dictionary<string, char> inputsDictionnary = new Dictionary<string, char>();

    private string bindingAxis = "";

    // Start is called before the first frame update
    void Start()
    {
        foreach (var _input in baseInputs)
        {
            inputsDictionnary.Add(_input.Name, _input.Key);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(bindingAxis != "")
        {
            if(Input.anyKeyDown)
            {
                foreach (KeyCode _keycode in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if(Input.GetKey(_keycode))
                    {
                        inputsDictionnary[bindingAxis] = (char)_keycode;
                        bindingAxis = "";
                        return;
                    }
                }
            }
        }

        foreach (var _inputAxis in inputsDictionnary.Keys)
        {
            TestInput(_inputAxis);
        }
    }

    private void TestInput(string _inputAxis)
    {
        bool _input = Input.GetKey((KeyCode)inputsDictionnary[_inputAxis]);
        if (_input) Debug.Log(_inputAxis + " : " + inputsDictionnary[_inputAxis]);
    }

    public void Bind(string _axis) => bindingAxis = _axis;
}

[System.Serializable]

public struct InputInfos
{
    [SerializeField] private string inputName;
    [SerializeField] private char key;

    public string Name => inputName;
    public char Key => key;
}