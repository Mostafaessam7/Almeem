using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace API.SignalR
{
  [Authorize]
  public class NotificationHub : Hub
  {
    // Store multiple connections per user
    private static readonly ConcurrentDictionary<string, List<string>> UserConnections = new();

    public override Task OnConnectedAsync()
    {
      var email = Context.User?.GetEmail();

      if (!string.IsNullOrEmpty(email))
      {
        UserConnections.AddOrUpdate(email, new List<string> { Context.ConnectionId }, (key, oldList) =>
        {
          oldList.Add(Context.ConnectionId);
          return oldList;
        });

        Console.WriteLine($"User {email} connected with connection ID {Context.ConnectionId}");
      }

      return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
      var email = Context.User?.GetEmail();

      if (!string.IsNullOrEmpty(email) && UserConnections.TryGetValue(email, out var connectionIds))
      {
        connectionIds.Remove(Context.ConnectionId);

        if (connectionIds.Count == 0)
        {
          UserConnections.TryRemove(email, out _);
        }

        Console.WriteLine($"User {email} disconnected with connection ID {Context.ConnectionId}");
      }

      return base.OnDisconnectedAsync(exception);
    }

    // Retrieve all connection IDs for a given email
    public static List<string>? GetConnectionIdsByEmail(string email)
    {
      UserConnections.TryGetValue(email, out var connectionIds);
      return connectionIds;
    }
  }

  public static class ClaimsPrincipalExtensions
  {
    public static string GetEmail(this ClaimsPrincipal user)
    {
      return user?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
    }
  }

}
