using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Menu.ValueObjects;

namespace BuberDinner.Domain.Menu.Entities;

public sealed class MenuSection : Entity<MenuSectionId>
{
    private readonly List<MenuItem> _menuItems = new();

    private MenuSection(MenuSectionId menuSectionId, string name, string description, List<MenuItem> items)
        : base(menuSectionId)
    {
        Name = name;
        Description = description;
        _menuItems = items;
    }

    public string Name { get; }
    public string Description { get; }
    public IReadOnlyList<MenuItem> Items => _menuItems.ToList().AsReadOnly();

    public static MenuSection Create(string name, string description, List<MenuItem> items)
    {
        return new MenuSection(MenuSectionId.CreateUnique(), name, description, items);
    }
}