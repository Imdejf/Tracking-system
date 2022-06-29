namespace TrackingSystem.Application.Common.DTOs.Enum
{
    public class EnumDTO
    {
        public string? FieldName { get; set; }
        public int FlagName { get; set; }

        private EnumDTO(string? fieldName, int flagValue)
        {
            FieldName = fieldName;
            FlagName = flagValue;
        }

        public static EnumDTO CreateForValue<TEnum>(TEnum @enum) where TEnum : System.Enum, IConvertible
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("Passed parameter must be enumrated type");
            }
            return new EnumDTO(System.Enum.GetName(typeof(TEnum), @enum), @enum.ToInt32(null));
        }

        public static ICollection<EnumDTO> CreateForType<TEnum>() where TEnum : System.Enum, IConvertible
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("Passed parameter must be enumrated type");
            }
            return System.Enum.GetValues(typeof(TEnum))
                              .Cast<TEnum>()
                              .Select(c => new EnumDTO(System.Enum.GetName(typeof(TEnum), c), c.ToInt32(null)))
                              .ToList();
        }
    }
}
