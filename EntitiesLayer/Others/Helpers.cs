namespace EntitiesLayer
{
    public static class Helpers
    {
        public static string GenerateValue()
        {
            int value = new Random().Next(2);

            return value switch
            {
                0 => "CARA",
                1 => "CRUZ",
                _ => null,
            };
        }
    }
}
