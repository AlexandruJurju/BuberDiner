using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Common.ValueObjects;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Guest.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu.ValueObjects;
using BuberDinner.Domain.MenuReview.ValueObjects;

namespace BuberDinner.Domain.MenuReview;

public sealed class MenuReview : AggregateRoot<MenuReviewId>
{
    private MenuReview(
        MenuReviewId menuReviewId,
        string comment,
        HostId hostId,
        MenuId menuId,
        GuestId guestId,
        DinnerId dinnerId,
        DateTime createdDateTime,
        DateTime updatedDateTime)
        : base(menuReviewId)
    {
        Comment = comment;
        HostId = hostId;
        MenuId = menuId;
        GuestId = guestId;
        DinnerId = dinnerId;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public Rating Rating { get; }
    public string Comment { get; }
    public HostId HostId { get; }
    public MenuId MenuId { get; }
    public GuestId GuestId { get; }
    public DinnerId DinnerId { get; }
    public DateTime CreatedDateTime { get; }
    public DateTime UpdatedDateTime { get; }

    public static MenuReview Create(
        string comment,
        HostId hostId,
        MenuId menuId,
        GuestId guestId,
        DinnerId dinnerId)
    {
        return new MenuReview(
            MenuReviewId.CreateUnique(),
            comment,
            hostId,
            menuId,
            guestId,
            dinnerId,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }
    
#pragma warning disable CS8618
    private MenuReview() { }
#pragma warning restore CS8618

}