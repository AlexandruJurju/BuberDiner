using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Menu;

namespace BuberDinner.Infrastructure.Persistence.Repositories;

public class MenuRepository : IMenuRepository
{
    private static readonly List<Menu> _menus = new();
    
    public void Add(Menu user)
    {
        _menus.Add(user);
    }
}