using BladeWaltz.Character;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningTop : MonoBehaviour
{
    [SerializeField] private CharacterManager m_characterManager;

    public CharacterManager GetCharacterManager()
    {
        return m_characterManager;
    }
}
