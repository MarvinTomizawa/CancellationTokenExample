using System.Collections.Generic;
using Domain.Constants;

namespace Domain.Factories
{
    public static class ElementFactory
    {
        public static ElementalAttribute Grass()
            => new ElementalAttribute
            {
                AttributeName = ElementName.Grass,
                StrongAgainst = new List<string>
                {
                    ElementName.Water,
                    ElementName.Rock,
                    ElementName.Ground
                },
                WeakAgainst = new List<string>
                {
                    ElementName.Fire,
                    ElementName.Bug,
                    ElementName.Flying,
                    ElementName.Ice,
                    ElementName.Poison
                }
            };
        
        public static ElementalAttribute Fire()
            => new ElementalAttribute
            {
                AttributeName = ElementName.Fire,
                StrongAgainst = new List<string>
                {
                    ElementName.Water,
                    ElementName.Ground,
                    ElementName.Rock,
                    ElementName.Ice
                },
                WeakAgainst = new List<string>
                {
                    ElementName.Water,
                    ElementName.Rock,
                    ElementName.Ground
                }
            };
        
        public static ElementalAttribute Water()
            => new ElementalAttribute
            {
                AttributeName = ElementName.Water,
                StrongAgainst = new List<string>
                {
                    ElementName.Fire,
                    ElementName.Rock,
                    ElementName.Ground
                },
                WeakAgainst = new List<string>
                {
                    ElementName.Grass,
                    ElementName.Eletric,
                    ElementName.Ground,
                    ElementName.Ice,
                    ElementName.Poison
                }
            };
    }
}