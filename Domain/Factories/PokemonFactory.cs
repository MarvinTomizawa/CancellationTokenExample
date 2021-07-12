namespace Domain.Factories
{
    public static class PokemonFactory
    {
        public static Pokemon Bulbassauro()
            => new Pokemon
            {
                Id = 1,
                Name = "Bulbassauro",
                Attribute = new AttributeType
                {
                    Primary = ElementFactory.Grass() 
                }
            };
        
        public static Pokemon Charmander()
            => new Pokemon
            {
                Id = 4,
                Name = "Charmander",
                Attribute = new AttributeType
                {
                    Primary = ElementFactory.Fire() 
                }
            };
        
        public static Pokemon Squirtle()
            => new Pokemon
            {
                Id = 7,
                Name = "Squirtle",
                Attribute = new AttributeType
                {
                    Primary = ElementFactory.Water() 
                }
            };
    }
}