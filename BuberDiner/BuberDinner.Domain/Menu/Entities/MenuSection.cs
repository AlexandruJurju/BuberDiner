using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Menu.ValueObjects;

namespace BuberDinner.Domain.Menu.Entities;

public sealed class MenuSection : Entity<MenuSectionId>
{
    private readonly List<MenuItem> _items = new();

    private MenuSection(MenuSectionId menuSectionId, string name, string description, List<MenuItem> items)
        : base(menuSectionId)
    {
        Name = name;
        Description = description;
        this._items = items;
    }

    public string Name { get; }
    public string Description { get; }
    public IReadOnlyList<MenuItem> Items => _items.ToList().AsReadOnly();

    public static MenuSection Create(string name, string description, List<MenuItem> items)
    {
        return new MenuSection(MenuSectionId.CreateUnique(), name, description, items);
    }
    
#pragma warning disable CS8618
    private MenuSection() { }
#pragma warning restore CS8618
}