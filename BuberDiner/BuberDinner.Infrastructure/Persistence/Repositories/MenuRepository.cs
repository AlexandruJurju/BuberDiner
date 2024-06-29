using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Menu;

namespace BuberDinner.Infrastructure.Persistence.Repositories;

public class MenuRepository : IMenuRepository
{
    private readonly BuberDinerDbContext _context;

    public MenuRepository(BuberDinerDbContext context)
    {
        _context = context;
    }

    public void Add(Menu user)
    {
        _context.Menus.Add(user);
    }
}