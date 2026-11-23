namespace AnimalManagement.Wpf.Models.Responses;

public record EnumTypeTranslation(int EnumType, string Translation);
public record GetEnumTypeTranslationsResponse(List<EnumTypeTranslation> EnumTypeTranslations);