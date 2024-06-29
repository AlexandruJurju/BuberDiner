namespace BuberDinner.Contracts.Menus;

public record MenuResponse(
    string Id,
    string Name,
    string Description,
    float? AverageRating,
    List<MenuSectionReponse> Sections,
    string HostId,
    List<string> DinnerIds,
    List<string> MenuReviewIds,
    DateTime CreatedDateTime,
    DateTime UpdatedDateTime
);

public record MenuSectionReponse(
    string Id,
    string Name,
    string Description,
    List<MenuItemResponse> Items
);

public record MenuItemResponse(
    string Id,
    string Name,
    string Description
);