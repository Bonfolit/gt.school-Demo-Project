using System;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Config
{

    [CreateAssetMenu(fileName = "MaterialConfig", menuName = "Config/Material Config", order = 0)]
    public class MaterialConfig : ScriptableObject
    {
        [SerializeField]
        private MasteryMaterial[] MasteryMaterials;

        public Material GetMaterial(int mastery)
        {
            foreach (var masteryMaterial in MasteryMaterials)
            {
                if (masteryMaterial.Mastery == mastery)
                {
                    return masteryMaterial.Material;
                }
            }

            throw new Exception($"Material with {mastery} mastery was not found.");
        }
    }

    [System.Serializable]
    public struct MasteryMaterial
    {
        public int Mastery;
        public Material Material;
    }

}